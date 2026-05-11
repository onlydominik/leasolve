using leasolve.API.Configuration;
using leasolve.Application;
using leasolve.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();

builder.Services.AddCustomControllers();
builder.Services.AddCustomOpenApiDocumentation();
builder.Services.AddCustomExceptionHandlers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Services.UseInfrastructure();

app.UseCustomExceptionHandlers();
app.UseCustomOpenApiDocumentation();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCustomSerilog();
app.UseAuthorization();

app.UseCustomControllers();

await app.RunAsync();