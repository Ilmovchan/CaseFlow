using CaseFlow.API.Extensions;
using CaseFlow.BLL.MappingProfiles;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DetectiveAgencyDb");

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
dataSourceBuilder.MapEnum<CaseStatus>("case_status");
dataSourceBuilder.MapEnum<DetectiveStatus>("detective_status");
dataSourceBuilder.MapEnum<EvidenceType>("evidence_type");
dataSourceBuilder.MapEnum<ApprovalStatus>("approval_status");
dataSourceBuilder.EnableUnmappedTypes();

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DetectiveAgencyDbContext>(options => options.UseNpgsql(
    dataSource,
    o =>
    {
        o.MapEnum<CaseStatus>("case_status");
        o.MapEnum<DetectiveStatus>("detective_status");
        o.MapEnum<EvidenceType>("evidence_type");
        o.MapEnum<ApprovalStatus>("approval_status");
    }));

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(CaseFlowMappingProfile).Assembly);

builder.Services.AddAdminServices();
builder.Services.AddDetectiveServices();
builder.Services.AddAdditionalServices();

builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums(); 
    options.SwaggerDoc("v1", new OpenApiInfo {Title = "CaseFlow.API", Version = "v1" });
});

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();