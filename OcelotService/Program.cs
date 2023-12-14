using MicroServices.Shared.JWT;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("apiGateway.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddOcelot();


var app = builder.Build();


app.UseHttpsRedirection();
app.MapControllers();
app.UseOcelot().Wait();
app.Run();
