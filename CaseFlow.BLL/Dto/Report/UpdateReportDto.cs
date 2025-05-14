namespace CaseFlow.BLL.Dto.Report;

public class UpdateReportDto
{
    public int ReportId { get; init; }
    public string? Summary { get; set; }
    public string? Comments { get; set; }
}
