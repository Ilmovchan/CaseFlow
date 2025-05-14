using CaseFlow.DAL.Enums;

namespace CaseFlow.DAL.Interfaces;

public interface IWorkflowEntity
{
    public ApprovalStatus ApprovalStatus { get; set; }
}