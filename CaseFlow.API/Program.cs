using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.BLL.Profiles;
using CaseFlow.BLL.Services;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DetectiveAgencyDb");

builder.Services.AddDbContext<DetectiveAgencyDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(CaseFlowMappingProfile).Assembly);

builder.Services.AddScoped<IAdminCaseService, AdminService>();
builder.Services.AddScoped<IAdminCaseTypeService, AdminService>();
builder.Services.AddScoped<IAdminClientService, AdminService>();
builder.Services.AddScoped<IAdminDetectiveService, AdminService>();
builder.Services.AddScoped<IAdminEvidenceService, AdminService>();
builder.Services.AddScoped<IAdminExpenseService, AdminService>();
builder.Services.AddScoped<IAdminReportService, AdminService>();
builder.Services.AddScoped<IAdminSuspectService, AdminService>();

builder.Services.AddScoped<IDetectiveCaseService, DetectiveService>();
builder.Services.AddScoped<IDetectiveClientService, DetectiveService>();
builder.Services.AddScoped<IDetectiveEvidenceService, DetectiveService>();
builder.Services.AddScoped<IDetectiveExpenseService, DetectiveService>();
builder.Services.AddScoped<IDetectiveReportService, DetectiveService>();
builder.Services.AddScoped<IDetectiveSuspectService, DetectiveService>();

builder.Services.AddScoped<IPostgresUserService, PostgresUserService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "CaseFlow.API", Version = "v1" });
});

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();