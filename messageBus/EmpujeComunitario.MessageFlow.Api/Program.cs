using EmpujeComunitario.MessageFlow.Common.Settings;
using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Implementation;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Implementation;
using EmpujeComunitario.MessageFlow.Service.Infrastructure;
using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection(nameof(RabbitMqSettings)));
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddHostedService<MessagesConsumerService>();
builder.Services.AddScoped<IDonationRequestRepository, DonationRequestRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddDbContext<MessageFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(RabbitMqMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MessageFlowDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
