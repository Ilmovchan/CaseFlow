using CaseFlow.BLL.Dto.Report;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveReportService
{
    Task<Report> CreateReportAsync(int caseId, CreateReportDto dto, int detectiveId);
    Task<Report> UpdateReportAsync(int reportId, UpdateReportDto dto, int detectiveId);
    
    Task<List<Report>> GetReportsAsync(int detectiveId);
    Task<List<Report>> GetSubmittedReportsAsync(int detectiveId);
    Task<List<Report>> GetRejectedReportsAsync(int detectiveId);
    Task<List<Report>> GetApprovedReportsAsync(int detectiveId);
    Task<Report?> GetReportAsync(int reportId, int detectiveId);
}