using CaseFlow.BLL.Dto.Report;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveReportService
{
    Task<ReportDto> CreateReportAsync(int caseId, CreateReportDto dto, int detectiveId);
    Task<ReportDto> UpdateReportAsync(int reportId, UpdateReportDto dto, int detectiveId);
    
    Task DeleteReportAsync(int reportId, int detectiveId);
    
    Task<List<ReportDto>> GetReportsAsync(int detectiveId);
    Task<List<ReportDto>> GetPendingReportsAsync(int detectiveId);
    Task<List<ReportDto>> GetDeclinedReportsAsync(int detectiveId);
    Task<List<ReportDto>> GetApprovedReportsAsync(int detectiveId);
    Task<ReportDto?> GetReportAsync(int reportId, int detectiveId);
}