using BnFurniture.Application.Abstractions;
using BnFurniture.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(s => s.FullName?.Replace("+", ".")); });
builder.Services.AddLogging();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IHandlerContext, HandlerContext>();
builder.Services.AddScoped<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
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