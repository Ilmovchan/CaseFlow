using CaseFlow.API.Endpoints;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DetectiveAgencyDb");
builder.Services.AddDbContext<DetectiveAgencyDbContext>(options =>
{
    options.UseNpgsql(connString);
});

var app = builder.Build();

app.MapClientsEndpoints();

app.Run();