using Microsoft.AspNetCore.Builder;
using Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Domain;
using Consul;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}DocumentationFile.xml");
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Service API",
    });
});
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.UseExceptionHandler(builder.Environment.IsDevelopment() ? "/development" : "/live");
app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Title"));
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

