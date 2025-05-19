using CaseFlow.BLL.Services.AdminServices;
using CaseFlow.BLL.Services.DetectiveServices;
using Microsoft.Extensions.DependencyInjection;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath("/Users/user/RiderProjects/CaseFlow/CaseFlow.UI")
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<DetectiveAgencyDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DetectiveAgencyDb")));

services.AddScoped<AdminService>();
services.AddTransient<DetectiveService>();

services.AddAutoMapper(typeof(DetectiveService).Assembly);

var serviceProvider = services.BuildServiceProvider();

var adminService = serviceProvider.GetService<AdminService>() ?? throw new Exception("admin service == null");
var detectiveService = serviceProvider.GetService<DetectiveService>();



