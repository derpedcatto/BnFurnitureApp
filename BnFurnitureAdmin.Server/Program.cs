using Azure.Storage.Blobs;
using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Shared.Utilities.AzureBlob;
using BnFurniture.Shared.Utilities.Email;
using BnFurniture.Shared.Utilities.Hash;
using BnFurnitureAdmin.Middleware;
using BnFurnitureAdmin.Server.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.WriteIndented = true;
    o.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(s => s.FullName?.Replace("+", ".")); });
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5027")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5027, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
    });

    options.ListenLocalhost(7247, listenOptions =>
    {
        listenOptions.UseHttps();
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});


// Suppress default error response model
builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => 
{
    apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
});


// Session settings
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});


// Logging Service registration
Console.OutputEncoding = System.Text.Encoding.UTF8;
builder.Services.AddLogging();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath |
                            HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestQuery |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.ResponseBody;

    logging.MediaTypeOptions.AddText("application/json");
    logging.MediaTypeOptions.AddText("application/xml");
    logging.MediaTypeOptions.AddText("text/plain");

    // logging.RequestBodyLogLimit = 1024;
    // logging.ResponseBodyLogLimit = 1024;
});


// Db Service registration
// var dbConnectionString = builder.Configuration["ProdDbConnection"];
var dbConnectionString = builder.Configuration.GetConnectionString("ProdDbConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString));
});


// FluentValidation Services registration
builder.Services.AddValidatorsFromAssemblyContaining<BnFurniture.Application.AssemblyClass>();
FluentValidation.ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) =>
    ToCamelCase(member?.Name);
foreach (var type in typeof(BnFurniture.Application.AssemblyClass).Assembly.GetTypes()
    .Where(x => x.Name.EndsWith("DTOValidator") && !x.IsAbstract && !x.IsInterface))
{
    builder.Services.AddScoped(type);
    Console.WriteLine($"Added validator - {type.Name}");
}


// Handlers registration
foreach (var type in typeof(BnFurniture.Application.AssemblyClass).Assembly.GetTypes()
    .Where(x => x.Name.EndsWith("Handler") && !x.IsAbstract && !x.IsInterface))
{
    builder.Services.AddTransient(type);
    Console.WriteLine($"Added handler - {type.Name}");
}

// Shared Service registration
foreach (var type in typeof(BnFurniture.Application.AssemblyClass).Assembly.GetTypes()
    .Where(x => x.Name.EndsWith("SharedLogic") && !x.IsAbstract && !x.IsInterface))
{
    builder.Services.AddScoped(type);
    Console.WriteLine($"Added shared logic class - {type.Name}");
}

builder.Services.AddScoped<IHandlerContext, HandlerContext>();


// Other Services registration
// builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration["AzureBlobStorageConnection"]));
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorageConnection")));
builder.Services.AddSingleton<IAzureImageBlobService, AzureImageBlobService>();
builder.Services.AddSingleton<IHashService, Sha256HashService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IAppImageService, AzureAppImageService>();


var app = builder.Build();



// Middleware registration
app.UseHttpLogging();
app.UseMiddleware<LogAndExceptionHandlerMiddleware>();


// Checking DB connection
var logger = app.Services.GetRequiredService<ILogger<Program>>();
await CheckDatabaseConnectionAsync(app, logger);


/*
var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}
*/

app.UseDefaultFiles();
app.UseStaticFiles(); // for wwwroot
/*
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "Images"
});
*/


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();
// app.UseAuthentication();

app.UseSession();
app.UseMiddleware<SessionAuthMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.MapControllers();

app.Run();



static string? ToCamelCase(string? str)
{
    if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
        return str;

    char[] chars = str.ToCharArray();

    chars[0] = char.ToLowerInvariant(chars[0]);
    return new string(chars);
}

static async Task CheckDatabaseConnectionAsync(WebApplication app, ILogger logger)
{
    using var scope = app.Services.CreateScope();

    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.CanConnectAsync();
        logger.LogInformation("Database connection success");
    }
    catch (Exception ex)
    {
        logger.LogError($"Database connection failed - {ex.Message}");
    }
}

/*
logging.LoggingFields =
    // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath |
    // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestMethod |
    // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery |
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody |
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode |
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;

logging.MediaTypeOptions.AddText("multipart/form-data");
logging.MediaTypeOptions.AddText("application/x-www-form-urlencoded");
*/

// Azure config
/*
var keyVaultName = builder.Configuration["bnfurnitureappsecrets"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(
        keyVaultUri,
        new DefaultAzureCredential(),
        new AzureKeyVaultConfigurationOptions
        {
            Manager = new KeyVaultSecretManager(),
            ReloadInterval = TimeSpan.FromSeconds(120)
        });
}
*/
