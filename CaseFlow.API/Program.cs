using CaseFlow.API.Extensions;
using CaseFlow.BLL.MappingProfiles;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("CaseFlowDB");

builder.Services.AddDbContext<CaseFlowDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services
    .AddControllers();
    
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