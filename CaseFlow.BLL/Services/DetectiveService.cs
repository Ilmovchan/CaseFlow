using AutoMapper;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.BLL.Dto.Expense;
using CaseFlow.BLL.Dto.Report;
using CaseFlow.BLL.Dto.Suspect;
using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Services;

public class DetectiveService(DetectiveAgencyDbContext context, IMapper mapper) :
    IDetectiveCaseService, IDetectiveClientService,
    IDetectiveEvidenceService, IDetectiveSuspectService, IDetectiveExpenseService, IDetectiveReportService
{
    #region Case
    public async Task<Case> UpdateCaseAsync(int caseId, UpdateCaseByDetectiveDto dto, int detectiveId)
    {
        var caseEntity = await context.Cases
            .FirstOrDefaultAsync(c => c.Id == caseId && c.DetectiveId == detectiveId) 
                         ?? throw new EntityNotFoundException("Case", caseId);
        
        mapper.Map(dto, caseEntity);
        
        await context.SaveChangesAsync();
        return caseEntity;
    }

    public async Task<List<Case>> GetCasesAsync(int detectiveId)
    {
        return await context.Cases
            .Where(c => c.DetectiveId == detectiveId)
            .ToListAsync();
    }

    public async Task<Case?> GetCaseAsync(int caseId, int detectiveId)
    {
        return await context.Cases
            .FirstOrDefaultAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);
    }
    #endregion

    #region Client
    public async Task<List<Client>> GetClientsAsync(int detectiveId)
    {
        return await context.Clients
            .Where(cl => cl.Cases!.Any(c => c.DetectiveId == detectiveId))
            .ToListAsync();
    }
    #endregion
    
    #region Evidence
    public async Task<Evidence> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId)
    {
        var hasAccess = await context.CaseEvidences
            .AnyAsync(ce => ce.CaseId == caseId && ce.Case.DetectiveId == detectiveId);

        if (!hasAccess) 
            throw new AccessDeniedException("Evidence", detectiveId);
        
        var evidenceEntity = mapper.Map<Evidence>(dto);
        context.Evidences.Add(evidenceEntity);
        
        var caseEvidenceEntity = new CaseEvidence()
        {
            CaseId = caseId,
            Evidence = evidenceEntity,
            ApprovalStatus = ApprovalStatus.Draft,
        };

        context.CaseEvidences.Add(caseEvidenceEntity);
        await context.SaveChangesAsync();

        return evidenceEntity;
    }

    public async Task<Evidence> UpdateEvidenceAsync(int evidenceId, UpdateEvidenceDto dto, int detectiveId)
    {
        var hasAccess = await context.CaseEvidences
            .AnyAsync(ce => ce.EvidenceId == evidenceId && ce.Case.DetectiveId == detectiveId);
        
        if (!hasAccess) 
            throw new AccessDeniedException("Evidence", detectiveId);
        
        var evidenceEntity = await context.Evidences
            .FindAsync(evidenceId) ?? throw new EntityNotFoundException("Evidence",  evidenceId);

        mapper.Map(dto, evidenceEntity);
        
        await context.SaveChangesAsync();
        return evidenceEntity;
    }

    public async Task DeleteEvidenceAsync(int evidenceId, int detectiveId)
    {
        var hasAccess = await context.CaseEvidences
            .AnyAsync(ce => ce.EvidenceId == evidenceId && ce.Case.DetectiveId == detectiveId);
        
        if (!hasAccess) 
            throw new AccessDeniedException("Evidence", detectiveId);
        
        var evidenceEntity = await context.Evidences
            .FindAsync(evidenceId) ?? throw new EntityNotFoundException("Evidence", evidenceId);
        
        context.Evidences.Remove(evidenceEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Evidence>> GetAssignedEvidencesAsync(int detectiveId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Evidence>> GetUnassignedEvidencesAsync(int detectiveId)
    {
        return await context.Evidences
            .Where(e => ! context.CaseEvidences.Any(ce => ce.EvidenceId == e.Id))
            .ToListAsync();
    }

    public async Task<List<Evidence>> GetAllEvidencesAsync(int detectiveId)
    {
        var assignedEvidences = context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId)
            .Select(ce => ce.Evidence);

        var unassignedEvidences = context.Evidences
            .Where(e => !context.CaseEvidences.Any(ce => ce.EvidenceId == e.Id));
        
        return await assignedEvidences
            .Union(unassignedEvidences)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Evidence>> GetRejectedEvidencesAsync(int detectiveId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId && ce.ApprovalStatus == ApprovalStatus.Declined)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Evidence>> GetApprovedEvidencesAsync(int detectiveId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId && ce.ApprovalStatus == ApprovalStatus.Approved)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Evidence?> GetEvidenceAsync(int evidenceId, int detectiveId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.EvidenceId == evidenceId && ce.Case.DetectiveId == detectiveId)
            .Select(ce => ce.Evidence)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Evidence>> GetSubmittedEvidencesAsync(int detectiveId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId && ce.ApprovalStatus == ApprovalStatus.Pending)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public async Task LinkEvidenceToCaseAsync(int evidenceId, int caseId, int detectiveId)
    {
        var hasAccessToEvidence = await context.CaseEvidences
                                      .Where(ce => ce.EvidenceId == evidenceId)
                                      .AnyAsync(ce => ce.Case.DetectiveId == detectiveId) 
                                  || !await context.CaseEvidences
                                      .AnyAsync(ce => ce.EvidenceId == evidenceId);

        
        var hasAccessToCase = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);
        
        if (!hasAccessToEvidence) 
            throw new AccessDeniedException("Evidence", detectiveId);
        if (!hasAccessToCase) 
            throw new AccessDeniedException("Case", detectiveId);
        
        var isCreatedAsync = await context.CaseEvidences
            .AnyAsync(ce => ce.EvidenceId == evidenceId && ce.CaseId == caseId);

        if (isCreatedAsync)
            throw new InvalidOperationException($"Evidence with Id {evidenceId} and case with Id {caseId} are already linked");

        var caseEvidenceEntity = new CaseEvidence()
        {
            CaseId = caseId,
            EvidenceId = evidenceId,
            ApprovalStatus = ApprovalStatus.Draft,
        };
        
        context.CaseEvidences.Add(caseEvidenceEntity);
        await context.SaveChangesAsync();
    }

    public async Task UnlinkEvidenceFromCaseAsync(int evidenceId, int caseId, int detectiveId)
    {
        var hasAccessToEvidence = await context.CaseEvidences
            .Where(ce => ce.EvidenceId == evidenceId)
            .AnyAsync(ce => ce.Case.DetectiveId == detectiveId);
        
        var hasAccessToCase = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);
        
        if (!hasAccessToEvidence) 
            throw new AccessDeniedException("Evidence", detectiveId);
        if (!hasAccessToCase) 
            throw new AccessDeniedException("Case", detectiveId);
        
        var caseEvidenceEntity = await context.CaseEvidences
            .FirstOrDefaultAsync(ce => ce.EvidenceId == evidenceId && ce.CaseId == caseId);
        
        if (caseEvidenceEntity == null)
            throw new InvalidOperationException($"Evidence with Id {evidenceId} and case with Id {caseId} are not linked");
        
        context.CaseEvidences.Remove(caseEvidenceEntity);
        
        await context.SaveChangesAsync();
    }
    #endregion
    
    #region Suspect
    public async Task<Suspect> CreateSuspectAsync(int caseId, CreateSuspectDto dto, int detectiveId)
    {
        var hasAccess = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccess) 
            throw new AccessDeniedException("Case", detectiveId);
        
        var suspectEntity = mapper.Map<Suspect>(dto);
        context.Suspects.Add(suspectEntity);

        var caseSuspectEntity = new CaseSuspect
        {
            CaseId = caseId,
            Suspect = suspectEntity,
            ApprovalStatus = ApprovalStatus.Draft,
        };

        context.CaseSuspects.Add(caseSuspectEntity);
        await context.SaveChangesAsync();

        return suspectEntity;
    }

    public async Task<Suspect> UpdateSuspectAsync(int suspectId, UpdateSuspectDto dto, int detectiveId)
    {
        var hasAccess = await context.CaseSuspects
            .AnyAsync(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Suspect", detectiveId);
        
        var suspectEntity = await context.Suspects
            .FindAsync(suspectId) ?? throw new EntityNotFoundException("Suspect", suspectId);

        mapper.Map(dto, suspectEntity);

        await context.SaveChangesAsync();
        return suspectEntity;
    }

    public async Task DeleteSuspectAsync(int suspectId, int detectiveId)
    {
        var hasAccess = await context.CaseSuspects
            .AnyAsync(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Suspect", detectiveId);
        
        var suspectEntity = await context.Suspects
            .FindAsync(suspectId) ?? throw new EntityNotFoundException("Suspect", suspectId);

        context.Suspects.Remove(suspectEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Suspect>> GetAssignedSuspectsAsync(int detectiveId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId)
            .Select(cs => cs.Suspect)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Suspect>> GetUnassignedSuspectsAsync(int detectiveId)
    {
        return await context.Suspects
            .Where(s => !context.CaseSuspects.Any(cs => cs.SuspectId == s.Id))
            .ToListAsync();
    }

    public async Task<List<Suspect>> GetAllSuspectsAsync(int detectiveId)
    {
        var assignedSuspects = context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId)
            .Select(cs => cs.Suspect);

        var unassignedSuspects = context.Suspects
            .Where(s => !context.CaseSuspects.Any(cs => cs.SuspectId == s.Id));

        return await assignedSuspects
            .Union(unassignedSuspects)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Suspect>> GetRejectedSuspectsAsync(int detectiveId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Declined)
            .Select(cs => cs.Suspect)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Suspect>> GetApprovedSuspectsAsync(int detectiveId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Approved)
            .Select(cs => cs.Suspect)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Suspect?> GetSuspectAsync(int suspectId, int detectiveId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId)
            .Select(cs => cs.Suspect)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Suspect>> GetSubmittedSuspectsAsync(int detectiveId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Pending)
            .Select(cs => cs.Suspect)
            .Distinct()
            .ToListAsync();
    }

    public async Task LinkSuspectToCaseAsync(int suspectId, int caseId, int detectiveId)
    {
        var hasAccessToSuspect = await context.CaseSuspects
                                        .Where(cs => cs.SuspectId == suspectId)
                                        .AnyAsync(cs => cs.Case.DetectiveId == detectiveId) 
                                    || !await context.CaseSuspects
                                        .AnyAsync(cs => cs.SuspectId == suspectId);

        var hasAccessToCase = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccessToSuspect)
            throw new AccessDeniedException("Suspect", detectiveId);
        if (!hasAccessToCase)
            throw new AccessDeniedException("Case", detectiveId);

        var isAlreadyLinked = await context.CaseSuspects
            .AnyAsync(cs => cs.SuspectId == suspectId && cs.CaseId == caseId);

        if (isAlreadyLinked)
            throw new InvalidOperationException($"Suspect with Id {suspectId} and Case with Id {caseId} are already linked.");

        var caseSuspectEntity = new CaseSuspect
        {
            CaseId = caseId,
            SuspectId = suspectId,
            ApprovalStatus = ApprovalStatus.Draft
        };

        context.CaseSuspects.Add(caseSuspectEntity);
        await context.SaveChangesAsync();
    }

    public async Task UnlinkSuspectFromCaseAsync(int suspectId, int caseId, int detectiveId)
    {
        var hasAccessToSuspect = await context.CaseSuspects
            .Where(cs => cs.SuspectId == suspectId)
            .AnyAsync(cs => cs.Case.DetectiveId == detectiveId);

        var hasAccessToCase = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccessToSuspect)
            throw new AccessDeniedException("Suspect", detectiveId);
        if (!hasAccessToCase)
            throw new AccessDeniedException("Case", detectiveId);

        var caseSuspectEntity = await context.CaseSuspects
            .FirstOrDefaultAsync(cs => cs.SuspectId == suspectId && cs.CaseId == caseId);

        if (caseSuspectEntity == null)
            throw new InvalidOperationException($"Suspect with Id {suspectId} and Case with Id {caseId} are not linked.");

        context.CaseSuspects.Remove(caseSuspectEntity);
        await context.SaveChangesAsync();
    }
    #endregion

    #region Expense
    public async Task<Expense> CreateExpenseAsync(int caseId, CreateExpenseDto dto, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.CaseId == caseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new AccessDeniedException("Expense",  detectiveId);
        
        var expenseEntity = mapper.Map<Expense>(dto);
        context.Expenses.Add(expenseEntity);
        await context.SaveChangesAsync();
        
        return expenseEntity;
    }

    public async Task<Expense> UpdateExpenseAsync(int expenseId, UpdateExpenseDto dto, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new AccessDeniedException("Expense",  detectiveId);

        var expenseEntity = await context.Expenses
            .FindAsync(expenseId) ?? throw new EntityNotFoundException("Expense",  expenseId);

        mapper.Map(dto, expenseEntity);
        await context.SaveChangesAsync();
        
        return expenseEntity;
    }

    public async Task DeleteExpenseAsync(int expenseId, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new AccessDeniedException("Expense",  detectiveId);

        var expenseEntity = await context.Expenses
            .FindAsync(expenseId) ?? throw new EntityNotFoundException("Expense", detectiveId);
        
        context.Expenses.Remove(expenseEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Expense>> GetExpensesAsync(int detectiveId)
    {
        return await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId)
            .ToListAsync();
    }

    public async Task<List<Expense>> GetRejectedExpensesAsync(int detectiveId)
    {
        return await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId && e.ApprovalStatus == ApprovalStatus.Declined)
            .ToListAsync();
    }

    public async Task<List<Expense>> GetApprovedExpensesAsync(int detectiveId)
    {
        return await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId && e.ApprovalStatus == ApprovalStatus.Approved)
            .ToListAsync();
    }

    public async Task<Expense?> GetExpenseAsync(int expenseId, int detectiveId)
    {
        return await context.Expenses
            .FirstOrDefaultAsync(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId);
    }
    #endregion

    #region Report
    public async Task<Report> CreateReportAsync(int caseId, CreateReportDto dto, int detectiveId)
    {
        var hasAccess = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Report", detectiveId);

        var reportEntity = mapper.Map<Report>(dto);
        reportEntity.CaseId = caseId;

        context.Reports.Add(reportEntity);
        await context.SaveChangesAsync();

        return reportEntity;
    }

    public async Task<Report> UpdateReportAsync(int reportId, UpdateReportDto dto, int detectiveId)
    {
        var hasAccess = await context.Reports
            .AnyAsync(r => r.Id == reportId && r.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Report", detectiveId);

        var reportEntity = await context.Reports
            .FindAsync(reportId) ?? throw new EntityNotFoundException("Report", reportId);

        mapper.Map(dto, reportEntity);
        await context.SaveChangesAsync();

        return reportEntity;
    }

    public async Task DeleteReportAsync(int reportId, int detectiveId)
    {
        var hasAccess = await context.Reports
            .AnyAsync(r => r.Id == reportId && r.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new AccessDeniedException("Report", detectiveId);

        var reportEntity = await context.Reports
            .FindAsync(reportId) ?? throw new EntityNotFoundException("Report", reportId);

        context.Reports.Remove(reportEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Report>> GetReportsAsync(int detectiveId)
    {
        return await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId)
            .ToListAsync();
    }

    public async Task<List<Report>> GetSubmittedReportsAsync(int detectiveId)
    {
        return await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Pending)
            .ToListAsync();
    }

    public async Task<List<Report>> GetRejectedReportsAsync(int detectiveId)
    {
        return await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Declined)
            .ToListAsync();
    }

    public async Task<List<Report>> GetApprovedReportsAsync(int detectiveId)
    {
        return await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Approved)
            .ToListAsync();
    }

    public async Task<Report?> GetReportAsync(int reportId, int detectiveId)
    {
        return await context.Reports
            .FirstOrDefaultAsync(r => r.Id == reportId && r.Case.DetectiveId == detectiveId);
    }
    #endregion
}