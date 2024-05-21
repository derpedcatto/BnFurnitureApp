using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductCategoryController.Commands;
using BnFurniture.Application.Controllers.ProductCategoryController.DTO;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Application.Controllers.ProductTypeController.Commands;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Shared.Utilities.Email;
using BnFurniture.Shared.Utilities.Hash;
using BnFurnitureApp.Middleware;
using BnFurnitureApp.Server.Middleware;
using FluentValidation;
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


// Suppress default error response model
builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => {
    apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
});


// Session settings
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Logging Service registration
Console.OutputEncoding = System.Text.Encoding.UTF8;
builder.Services.AddLogging();
builder.Services.AddHttpLogging(logging => 
{
    logging.LoggingFields =
        // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath |
        // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestMethod |
        // Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery |
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody |
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode |
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;

    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.MediaTypeOptions.AddText("application/x-www-form-urlencoded");
});  
    

// Db Service registration
var connectionString = builder.Configuration.GetConnectionString("DerpeLocalConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("BnFurniture.Infrastructure"));
});


// FluentValidation Services registration
builder.Services.AddValidatorsFromAssemblyContaining<BnFurniture.Application.AssemblyClass>();
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

builder.Services.AddScoped<IHandlerContext, HandlerContext>();


//builder.Services.AddScoped<IValidator<ProductCategoryDTO>, ProductCategoryDTOValidator>();
//builder.Services.AddScoped<AddProductCategoryHandler>();
//builder.Services.AddScoped<GetProductCategoriesHandler>();
//builder.Services.AddScoped<DeleteProductCategoryHandler>();
//builder.Services.AddScoped<UpdateProductCategoryHandler>();
//builder.Services.AddScoped<IValidator<ProductTypeDTO>, ProductTypeDTOValidator>();
//builder.Services.AddScoped<CreateProductTypeHandler>();
////builder.Services.AddScoped<GetAllProductTypesHandler>();
//builder.Services.AddScoped<UpdateProductTypeHandler>();
//builder.Services.AddScoped<DeleteProductTypeHandler>();
////  builder.Services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();




// Other Services registration
builder.Services.AddSingleton<IHashService, Sha1HashService>();
builder.Services.AddSingleton<IEmailService, EmailService>();



var app = builder.Build();

// Checking DB connection
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (dbContext.Database.CanConnect())
    {
        var databaseName = dbContext.Database.GetDbConnection().Database;
        Console.WriteLine($"Connected to the database: {databaseName}");
    }
    else
    {
        Console.WriteLine("Failed to connect to the database.");
        // Здесь можно предпринять дополнительные действия в случае неудачного подключения
    }
}

// Middleware registration
app.UseHttpLogging();
app.UseMiddleware<LogAndExceptionHandlerMiddleware>();


// Checking DB connection
var logger = app.Services.GetRequiredService<ILogger<Program>>();
await CheckDatabaseConnectionAsync(app, logger);


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// app.UseAuthentication();

app.UseSession();
app.UseMiddleware<SessionAuthMiddleware>();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();


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