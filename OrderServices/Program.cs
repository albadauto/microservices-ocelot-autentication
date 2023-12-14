using AutoMapper;
using MicroServices.Shared.DTO;
using MicroServices.Shared.JWT;
using MicroServices.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.Shared.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
string connection = builder.Configuration.GetConnectionString("Database");
var authenticationProviderKey = "IdentityApiKey";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<SqlServerContext>(o => o.UseSqlServer(connection));
builder.Services.AddAuthorization();
builder.Services.AddJwtCustomAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orders/insertorder", async (SqlServerContext context, OrderModel dto) =>
{
    context.Order.Add(dto);
    context.SaveChanges();
    return Results.Ok();
}).RequireAuthorization();
app.UseAuthentication();
app.UseAuthorization();

app.Run();


