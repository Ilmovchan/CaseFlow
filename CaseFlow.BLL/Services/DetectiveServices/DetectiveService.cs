using CaseFlow.BLL.Common;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.BLL.Dto.Expense;
using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Services.DetectiveServices;

public class DetectiveService : 
    IDetectiveCaseService, IDetectiveClientService,
    IDetectiveEvidenceService, IDetectiveExpenseService
{
    private readonly DetectiveAgencyDbContext _context;
    
    public DetectiveService(DetectiveAgencyDbContext context) 
        => _context = context;
    
    #region Case
    
    public async Task<Case> UpdateCaseAsync(UpdateCaseByDetectiveDto dto, int  detectiveId)
    {
        var existingCase = (await _context.Cases.FindAsync(dto.Id))!
            .EnsureExists("Case", dto.Id)
            .EnsureCaseAccess(detectiveId);
        
        if (dto.Description != null)
            existingCase.Description = dto.Description;
        
        if (dto.Status != null)
            existingCase.Status = dto.Status.Value;

        await _context.SaveChangesAsync();
        return existingCase;    
    }

    public Task<List<Case>> GetCasesAsync(int detectiveId)
    {
        return _context.Cases
            .Where(c => c.DetectiveId == detectiveId)
            .ToListAsync();
    }

    public async Task<Case?> GetCaseAsync(int caseId, int detectiveId)
    {
        var existingCase = (await _context.Cases.FindAsync(caseId))!
            .EnsureExists("Case", caseId)
            .EnsureCaseAccess(detectiveId);

        return existingCase;
    }
    
    #endregion
    
    #region Client
    
    public Task<List<Client>> GetClientsAsync(int detectiveId)
    {
        return _context.Cases.Where(c => c.DetectiveId == detectiveId)
            .Select(c => c.Client!)
            .Distinct()
            .ToListAsync();
    }
    
    #endregion

    #region Evidence
    
    public async Task<Evidence> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId)
    {
        var evidenceEntity = new Evidence()
        {
            Type = dto.Type,
            Description = dto.Description,
            CollectionDate = dto.CollectionDate,
            Region = dto.Region,
            Annotation = dto.Annotation,
            Purpose = dto.Purpose,
        };

        var caseEvidenceEntity = new CaseEvidence()
        {
            CaseId = caseId,
            EvidenceId = evidenceEntity.Id,
            ApprovalStatus = ApprovalStatus.Draft,
        };
        
        _context.Evidences.Add(evidenceEntity);
        _context.Set<CaseEvidence>().Add(caseEvidenceEntity);
        
        await _context.SaveChangesAsync();

        return evidenceEntity;
    }

    public async Task<Evidence> UpdateEvidenceAsync(UpdateEvidenceDto dto, int detectiveId)
    {
        var existingEvidence = (await _context.Evidences.FindAsync(dto.Id))!
            .EnsureExists("Evidence", dto.Id);
        
        if (dto.Type != null)
            existingEvidence.Type = dto.Type.Value;
        
        if (dto.Description != null)
            existingEvidence.Description = dto.Description;
        
        if (dto.CollectionDate != null)
            existingEvidence.CollectionDate = dto.CollectionDate.Value;
        
        if (dto.Region != null)
            existingEvidence.Region = dto.Region;

        if (dto.Annotation != null)
            existingEvidence.Annotation = dto.Annotation;
        
        if (dto.Purpose != null)
            existingEvidence.Purpose = dto.Purpose;

        await _context.SaveChangesAsync();
        return existingEvidence;
    }

    public async Task DeleteEvidenceAsync(int evidenceId, int detectiveId)
    {
        var evidenceEntity = (await _context.Evidences
            .FindAsync(evidenceId))!
            .EnsureExists("Evidence", evidenceId);

        var caseEvidence = await _context.Set<CaseEvidence>()
            .Include(ce => ce.Case)
            .Where(ce => ce.EvidenceId == evidenceId)
            .ToListAsync();

        evidenceEntity.EnsureEvidenceAccess(caseEvidence, detectiveId);
        
        _context.Evidences.Remove(evidenceEntity);
    }

    public Task<List<Evidence>> GetEvidencesAsync(int detectiveId)
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.Case.DetectiveId == detectiveId)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public Task<List<Evidence>> GetRejectedEvidencesAsync(int detectiveId)
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.Case.DetectiveId == detectiveId && 
                         ce.ApprovalStatus == ApprovalStatus.Rejected)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public Task<List<Evidence>> GetApprovedEvidencesAsync(int detectiveId)
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.Case.DetectiveId == detectiveId && 
                         ce.ApprovalStatus == ApprovalStatus.Approved)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Evidence?> GetEvidenceAsync(int evidenceId, int detectiveId)
    {
        var evidence = (await _context.Evidences.FindAsync(evidenceId))!
            .EnsureExists("Evidence", evidenceId);
        
        var caseEvidences = await _context.Set<CaseEvidence>()
            .Include(ce => ce.Case)
            .Where(ce => ce.EvidenceId == evidenceId)
            .ToListAsync();
        
        evidence.EnsureEvidenceAccess(caseEvidences, detectiveId);

        return evidence;
    }

    public Task<List<Evidence>> GetSubmittedEvidencesAsync(int detectiveId)
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.Case.DetectiveId == detectiveId && 
                         ce.ApprovalStatus == ApprovalStatus.Submitted)
            .Select(ce => ce.Evidence)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task LinkEvidenceToCaseAsync(int evidenceId, int caseId, int detectiveId)
    {
        var evidenceEntity = (await _context.Evidences.FindAsync(evidenceId))
            .EnsureExists("Evidence", evidenceId);
        
        var caseEvidences = await _context.Set<CaseEvidence>()
            .Include(ce => ce.Case)
            .Where(ce => ce.EvidenceId == evidenceId)
            .ToListAsync();

        evidenceEntity!.EnsureEvidenceAccess(caseEvidences, detectiveId);
        
        var existingLink = await _context.Set<CaseEvidence>()
            .AnyAsync(ce => ce.EvidenceId == evidenceId && ce.CaseId == caseId);

        if (existingLink)
            throw new InvalidOperationException
                ("Evidence is already linked to this case.");

        var newEvidence = new CaseEvidence()
        {
            EvidenceId = evidenceId,
            CaseId = caseId,
            ApprovalStatus = ApprovalStatus.Submitted,
        };

        _context.Set<CaseEvidence>().Add(newEvidence);
        await _context.SaveChangesAsync();
    }

    public async Task UnlinkEvidenceFromCaseAsync(int evidenceId, int caseId, int detectiveId)
    {
        var evidenceEntity = (await _context.Evidences.FindAsync(evidenceId))
            .EnsureExists("Evidence", evidenceId);
        
        var caseEvidences = await _context.Set<CaseEvidence>()
            .Where(ce => ce.EvidenceId == evidenceId)
            .ToListAsync();

        evidenceEntity!.EnsureEvidenceAccess(caseEvidences, detectiveId);

        var existingLink = caseEvidences
            .FirstOrDefault(ce => ce.EvidenceId == evidenceId && ce.CaseId == caseId);

        if (existingLink == null)
            throw new InvalidOperationException("Evidence is not linked to this case.");
        
        _context.Set<CaseEvidence>().Remove(existingLink!);
        await _context.SaveChangesAsync();
    }
    
    #endregion

    public Task<Expense> CreateExpenseAsync(int caseId, CreateExpenseDto dto, int detectiveId)
    {
        var expenseEntity = new
        {
            DateTime = dto.DateTime,
            Purpose = dto.Purpose,
            Amount = dto.Amount,
            Annotation = dto.Annotation,
            Status = dto.Status,
        };
        
        
    }

    public Task<Expense> UpdateExpenseAsync(UpdateExpenseDto dto, int detectiveId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteExpenseAsync(int expenseId, int detectiveId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetExpensesAsync(int detectiveId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetDeclinedExpensesAsync(int detectiveId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Expense>> GetAcceptedExpensesAsync(int detectiveId)
    {
        throw new NotImplementedException();
    }

    public Task<Expense?> GetExpenseAsync(int expenseId, int detectiveId)
    {
        throw new NotImplementedException();
    }
}