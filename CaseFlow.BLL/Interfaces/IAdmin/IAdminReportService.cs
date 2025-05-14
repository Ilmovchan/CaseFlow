using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminReportService
{
    Task<Report?> GetReportAsync(int reportId);
    Task<List<Report>> GetReportsFromCaseAsync(int caseId);
    Task<List<Report>> GetReportsAsync();
    Task<List<Report>> GetPendingReportsAsync();
}