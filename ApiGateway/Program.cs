using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration).AddConsul();

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapGet("/", async context => await context.Response.WriteAsync("API Gateway is up and running !")));
app.UseHttpsRedirection();
await app.UseOcelot();

app.Run();
