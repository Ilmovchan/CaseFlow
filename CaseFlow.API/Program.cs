using CaseFlow.API;
using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.BLL.MappingProfiles;
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

builder.Services.AddScoped<IAdminCaseService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminCaseTypeService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminClientService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminDetectiveService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminEvidenceService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminExpenseService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminReportService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IAdminSuspectService, AdminService>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IDetectiveCaseService, DetectiveService>();
builder.Services.AddScoped<IDetectiveClientService, DetectiveService>();
builder.Services.AddScoped<IDetectiveEvidenceService, DetectiveService>();
builder.Services.AddScoped<IDetectiveExpenseService, DetectiveService>();
builder.Services.AddScoped<IDetectiveReportService, DetectiveService>();
builder.Services.AddScoped<IDetectiveSuspectService, DetectiveService>();

builder.Services.AddScoped<IPostgresUserService, PostgresUserService>();

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