using CaseFlow.API.Exceptions;
using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.BLL.Services;

namespace CaseFlow.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdminServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminCaseService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminCaseTypeService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminClientService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminDetectiveService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminEvidenceService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminExpenseService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminReportService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddScoped<IAdminSuspectService, AdminService>()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }

    public static IServiceCollection AddDetectiveServices(this IServiceCollection services)
    {
        services.AddScoped<IDetectiveCaseService, DetectiveService>();
        services.AddScoped<IDetectiveClientService, DetectiveService>();
        services.AddScoped<IDetectiveEvidenceService, DetectiveService>();
        services.AddScoped<IDetectiveExpenseService, DetectiveService>();
        services.AddScoped<IDetectiveReportService, DetectiveService>();
        services.AddScoped<IDetectiveSuspectService, DetectiveService>();
        
        return services;
    }

    public static IServiceCollection AddAdditionalServices(this IServiceCollection services)
    {
        services.AddScoped<IPostgresUserService, PostgresUserService>();

        
        return services;
    }
}