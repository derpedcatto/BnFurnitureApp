using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Behaviors;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Shared.Utilities.Hash;
using Mediator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(s => s.FullName?.Replace("+", ".")); });
builder.Services.AddLogging();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30)));
});

// TODO: перенести это в отдельный метод и использовать ILogger
using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try 
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Connected to the database.");
    }
    catch
    {
        Console.WriteLine("Failed to connect to the database.");
    }
}

builder.Services.AddScoped<IHandlerContext, HandlerContext>();

builder.Services.AddSingleton<IHashServices, Sha1HashService>();

builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var app = builder.Build();

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

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();



/* MediatR
builder.Services.AddMediatR(cfg => {
    var executingAssembly = Assembly.GetExecutingAssembly();
    var referencedAssemblies = executingAssembly.GetReferencedAssemblies();
    foreach (var assemblyName in referencedAssemblies)
    {
        var assembly = Assembly.Load(assemblyName);
        cfg.RegisterServicesFromAssembly(assembly);
    }
    cfg.RegisterServicesFromAssembly(executingAssembly);
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
*/