using EmpujeComunitario.MessageFlow.Api.Configuration;
using EmpujeComunitario.MessageFlow.Common.Settings;
using EmpujeComunitario.MessageFlow.DataAccess.Context;
using EmpujeComunitario.MessageFlow.DataAccess.Implementation;
using EmpujeComunitario.MessageFlow.DataAccess.Interface;
using EmpujeComunitario.MessageFlow.Service.Implementation;
using EmpujeComunitario.MessageFlow.Service.Infrastructure;
using EmpujeComunitario.MessageFlow.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageFlow API", Version = "v1" });

    options.OperationFilter<ApplySwaggerHeaderOperationFilter>();
});

builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection(nameof(RabbitMqSettings)));

//context
builder.Services.AddDbContext<MessageFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<EmpujeComunitarioContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//repositories
builder.Services.AddScoped<ICancelledDonationRepository , CancelledDonationRepository>();
builder.Services.AddScoped<IDonationRequestRepository, DonationRequestRepository>();
builder.Services.AddScoped<IDonationsRepository , DonationsRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();

//service
builder.Services.AddScoped<ICancellDonationService, CancellDonationService>();
builder.Services.AddHostedService<MessagesConsumerService>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddScoped<IRequestDonationService, RequestDonationService>();
builder.Services.AddScoped<ITransferDonationService, TransferDonationService>();
builder.Services.AddScoped<IOfferDonationService, OfferDonationService>();
builder.Services.AddScoped<IEventSolidaryService , EventSolidaryService>();
builder.Services.AddScoped<IExternalDataService, ExternalDataService>();
builder.Services.AddScoped<IVolunteerAdhesionService, VolunteerAdhesionService>();


builder.Services.AddAutoMapper(typeof(RabbitMqMappingProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MessageFlowDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
