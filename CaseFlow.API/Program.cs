using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.BLL.Services;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DetectiveAgencyDb");

builder.Services.AddDbContext<DetectiveAgencyDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AdminService));
builder.Services.AddAutoMapper(typeof(DetectiveService));

builder.Services.AddScoped<IAdminCaseService, AdminService>();
builder.Services.AddScoped<IAdminCaseTypeService, AdminService>();
builder.Services.AddScoped<IAdminClientService, AdminService>();
builder.Services.AddScoped<IAdminDetectiveService, AdminService>();
builder.Services.AddScoped<IAdminEvidenceService, AdminService>();
builder.Services.AddScoped<IAdminExpenseService, AdminService>();
builder.Services.AddScoped<IAdminReportService, AdminService>();
builder.Services.AddScoped<IAdminSuspectService, AdminService>();

builder.Services.AddScoped<DetectiveService>();
builder.Services.AddScoped<IPostgresUserService, PostgresUserService>();

var app = builder.Build();

app.MapControllers();

app.Run();