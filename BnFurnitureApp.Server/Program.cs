using ASP_Work.Data;
using BnFurniture.Application.Abstractions;
using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(s => s.FullName?.Replace("+", ".")); });
builder.Services.AddLogging();

builder.Services.AddScoped<IHandlerContext, HandlerContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30)));
});

using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

    if (dbContext.Database.CanConnect())
    {
        Console.WriteLine("Connected to the database.");
    }
    else
    {
        Console.WriteLine("Failed to connect to the database.");
        // Здесь можно предпринять дополнительные действия в случае неудачного подключения
    }
}

builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

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