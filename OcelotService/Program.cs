using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var authenticationProviderKey = "Bearer";
builder.Configuration.AddJsonFile("apiGateway.json", optional: false, reloadOnChange: true);
builder.Services.AddAuthentication(s => 
{
    s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(authenticationProviderKey, x =>
{
    x.SaveToken = true;
    x.RequireHttpsMetadata = false;
    x.Authority = "https://localhost:7112";
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        ValidateAudience = false,
        ValidateIssuer = false, 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IdentityApiKey"))

    };
});
builder.Services.AddControllers();
builder.Services.AddOcelot();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseOcelot().Wait();
app.Run();
