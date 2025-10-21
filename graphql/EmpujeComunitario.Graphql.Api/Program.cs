using EmpujeComunitario.Graphql.Api.GraphqlQuery;
using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Implementation;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using EmpujeComunitario.Graphql.Service.Implementation;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MessageFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IGraphqlReportService, GraphqlReportService>();
builder.Services.AddScoped<ISoapClientService, SoapClientService>();

builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<DonationQuery>();

var app = builder.Build();

app.MapGraphQL("/graphql");
app.MapNitroApp("/bcp");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
