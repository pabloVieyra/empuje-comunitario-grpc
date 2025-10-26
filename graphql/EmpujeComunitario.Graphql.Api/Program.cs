using EmpujeComunitario.Graphql.Api.Configuration;
using EmpujeComunitario.Graphql.Api.GraphqlQuery;
using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Implementation;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using EmpujeComunitario.Graphql.Service.Implementation;
using EmpujeComunitario.Graphql.Service.Infrastructure;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Filter API", Version = "v1" });

    options.OperationFilter<AddRequiredHeaderParameter>(
        "UserId", // El nombre del header
        "ID del usuario para identificar la sesi�n.", 
        true 
    );
});

builder.Services.AddDbContext<MessageFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IGraphqlReportService, GraphqlReportService>();
builder.Services.AddScoped<ISoapClientService, SoapClientService>();
builder.Services.AddScoped<IFilterService, FilterService>();

builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<IUserSavedFilterRepository, UserSavedFilterRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.Configure<SoapWebServiceSetting>(builder.Configuration.GetSection(nameof(SoapWebServiceSetting)));
builder.Services
    .AddGraphQLServer()
    .AddQueryType<DonationQuery>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

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
app.MapGraphQL("/graphql");
app.MapNitroApp("/bcp");


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
