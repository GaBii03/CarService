using Microsoft.OpenApi.Models;
using CarService.BL;
using CarService.DL;

var builder = WebApplication.CreateBuilder(args);

// ако проектът ти има контролери
builder.Services.AddControllers();
builder.Services.AddBusinessLogic();
builder.Services.AddConfigurations(builder.Configuration);


// активиране на Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Примерна Swagger интеграция в .NET 9"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.MapControllers();

app.Run();
