using leasolve.API.Configuration;
using leasolve.Application;
using leasolve.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();

builder.Services.AddCustomControllers();
builder.Services.AddCustomOpenApiDocumentation();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseCustomOpenApiDocumentation();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.UseCustomControllers();

await app.RunAsync();