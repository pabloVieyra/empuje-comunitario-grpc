using EmpujeComunitario.Client.Services.Implementation;
using EmpujeComunitario.Client.Services.Infrastructure;
using EmpujeComunitario.Client.Services.Interface;
using Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Esta línea es útil pero no soluciona el problema actual
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.ConfigureApiBehaviorOptions(options =>
{
    // Esta línea es la que resuelve tu problema.
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddGrpcClient<UserService.UserServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("ServerGrpc"));
});
builder.Services.AddGrpcClient<AuthService.AuthServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("ServerGrpc"));
});
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserManagerServices, UserManagerServices>();
builder.Services.AddScoped<IAuthManagerServices, AuthManagerServices>();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
