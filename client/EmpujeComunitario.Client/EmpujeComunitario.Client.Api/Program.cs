using EmpujeComunitario.Client.Api.Configuration;
using EmpujeComunitario.Client.Api.Infrastructure;
using EmpujeComunitario.Client.Common.Settings;
using EmpujeComunitario.Client.Services.Implementation;
using EmpujeComunitario.Client.Services.Infrastructure;
using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.WebClient.Infrastructure;
using Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Esta l�nea es �til pero no soluciona el problema actual
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.ConfigureApiBehaviorOptions(options =>
{
    // Esta l�nea es la que resuelve tu problema.
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

builder.Services.AddGrpcClient<EventService.EventServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("ServerGrpc"));
});

builder.Services.AddGrpcClient<DonationInventoryService.DonationInventoryServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("ServerGrpc"));
});

builder.Services.Configure<RabbitMq>(
    builder.Configuration.GetSection(nameof(RabbitMq)));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Client API", Version = "v1" });

    options.OperationFilter<ApplySwaggerHeaderOperationFilter>();
});
builder.Services.AddScoped<IUserManagerServices, UserManagerServices>();
builder.Services.AddScoped<IAuthManagerServices, AuthManagerServices>();
builder.Services.AddScoped<IEventManagerServices, EventManagerServices>();
builder.Services.AddScoped<IDonationManagerService, DonationManagerService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IExternalDataService,  ExternalDataService>();
builder.Services.AddConfigurationMessageFlow(builder.Configuration);

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EventProfile).Assembly);
builder.Services.AddAutoMapper(typeof(DonationProfile).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();
// Construye la app después de agregar todos los servicios
var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
app.UseMiddleware<AuthorizationMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
