using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        var caseEntity = await context.Cases.
            FirstOrDefaultAsync(c => c.Id == caseId && c.DetectiveId == detectiveId)
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
        
        // ReSharper disable once InconsistentNaming
        var _case = await context.Cases
            .FirstOrDefaultAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);
        
        if (_case == null)
            throw new EntityNotFoundException("Case", caseId);
        
        return _case;
    }
    #endregion

    #region Client
    public async Task<List<Client>> GetClientsAsync(int detectiveId)
    {
        return await context.Clients
            .Where(cl => cl.Cases!.Any(c => c.DetectiveId == detectiveId))
            .ToListAsync();
    }

    public async Task<Client?> GetClientAsync(int clientId, int detectiveId)
    {
        var client = await context.Clients
            .FirstOrDefaultAsync(cl => cl.Cases!.Any(c => c.DetectiveId == detectiveId && c.ClientId == clientId));
        
        if (client == null)
            throw new EntityNotFoundException("Client", clientId);
        
        return client;
    }
    #endregion
    
    #region Evidence
        public async Task<EvidenceCaseDto> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId)
    {
        //Проверяю доступ детектива к делу
        var hasAccess = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccess) 
            throw new EntityNotFoundException("Case", caseId);
        
        //Добавляю в БД сущности evidence и caseEvidence
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
        
        //Возвращаю модель в виде дто(чтобы добавить удобный вывод в респонсе) 
        var dtoResult = mapper.Map<EvidenceCaseDto>(evidenceEntity);
        dtoResult.EvidenceId = evidenceEntity.Id;
        dtoResult.CaseId = caseId;
        dtoResult.ApprovalStatus = ApprovalStatus.Draft;
        
        return dtoResult;
    }
    
    public async Task<EvidenceCaseDto> UpdateEvidenceAsync(int evidenceId, UpdateEvidenceDto dto, int detectiveId, bool forceClone = true)
    {
        var caseEvidences = await context.CaseEvidences
            .Include(ec => ec.Case)
            .Where(e => e.EvidenceId == evidenceId)
            .ToListAsync();
        
        var hasAccess = caseEvidences.Any(ce => ce.Case.DetectiveId == detectiveId) || !caseEvidences.Any();

        if (!hasAccess)
            throw new EntityNotFoundException("Evidence", evidenceId);

        var isUsedInOtherCases = caseEvidences.Any(ce => ce.Case.DetectiveId != detectiveId);

        if (isUsedInOtherCases)
        {
            if (!forceClone)
                throw new EntityUpdateConflictException("Evidence", evidenceId);
                
            var original = await context.Evidences.FindAsync(evidenceId);
            if (original == null)
                throw new EntityNotFoundException("Evidence", evidenceId);

            var copy = mapper.Map<Evidence>(original);
            mapper.Map(dto, copy);
            context.Evidences.Add(copy);

            await context.SaveChangesAsync();

            var currentLink = caseEvidences.FirstOrDefault(ce => ce.Case.DetectiveId == detectiveId)
                ?? throw new InvalidOperationException("Cannot find CaseEvidence for current detective.");

            // Удаляем только связь текущего детектива
            context.CaseEvidences.Remove(currentLink);

            context.CaseEvidences.Add(new CaseEvidence
            {
                CaseId = currentLink.CaseId,
                EvidenceId = copy.Id,
                ApprovalStatus = ApprovalStatus.Draft,
            });

            await context.SaveChangesAsync();

            var resultDto = mapper.Map<EvidenceCaseDto>(copy);
            resultDto.CaseId = currentLink.CaseId;
            resultDto.ApprovalStatus = ApprovalStatus.Draft;

            return resultDto;
        }

        var evidence = await context.Evidences.FindAsync(evidenceId);
        if (evidence == null)
            throw new EntityNotFoundException("Evidence", evidenceId);

        mapper.Map(dto, evidence);
        await context.SaveChangesAsync();

        var finalDto = mapper.Map<EvidenceCaseDto>(evidence);
        finalDto.CaseId = null;
        finalDto.ApprovalStatus = ApprovalStatus.Draft;

        return finalDto;
    }


    public async Task DeleteEvidenceAsync(int evidenceId, int detectiveId)
    {
        var caseEvidences = await context.CaseEvidences
            .Include(ec => ec.Case)
            .Where(e => e.EvidenceId == evidenceId)
            .ToListAsync();
        
        var isLinkedToAnyCase = caseEvidences.Any();
        if (!isLinkedToAnyCase)
            throw new EntityDeleteConflictException("Evidence is not linked to any case");
        
        var hasAccess = caseEvidences.Any(ce => ce.Case.DetectiveId == detectiveId);
        if (!hasAccess)
            throw new EntityNotFoundException("Evidence", evidenceId);

        var isUsedInOtherCases = caseEvidences.Any(ce => ce.Case.DetectiveId != detectiveId);
        if (isUsedInOtherCases)
            throw new EntityDeleteConflictException("Evidence is linked to other detectives case");

        context.CaseEvidences.RemoveRange(caseEvidences);
        
        var evidence = await context.Evidences.FindAsync(evidenceId) ?? throw new EntityNotFoundException("Evidence", evidenceId);
        context.Evidences.Remove(evidence);
        
        await context.SaveChangesAsync();
    }
    
    public async Task<List<EvidenceCaseDto>> GetAssignedEvidencesAsync(int detectiveId) =>
        await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId)
            .Select(ce => new EvidenceCaseDto
            {
                EvidenceId = ce.EvidenceId,
                CaseId = ce.CaseId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus,
            })
            .Distinct()
            .ToListAsync();

    public async Task<List<EvidenceCaseDto>> GetUnassignedEvidencesAsync(int detectiveId) =>
        await context.Evidences
            .Where(e => ! context.CaseEvidences.Any(ce => ce.EvidenceId == e.Id))
            .Select(e => new EvidenceCaseDto
            {
                EvidenceId = e.Id,
                CaseId = null,
                Type = e.Type,
                Description = e.Description,
                CollectionDate = e.CollectionDate,
                Region = e.Region,
                Annotation = e.Annotation,
                Purpose = e.Purpose,
                ApprovalStatus = null,
            })
            .ToListAsync();

    public async Task<List<EvidenceCaseDto>> GetEvidencesAsync(int detectiveId)
    {
        var assignedEvidences = await GetAssignedEvidencesAsync(detectiveId);
        var unassignedEvidences = await GetUnassignedEvidencesAsync(detectiveId);

        return assignedEvidences
            .Union(unassignedEvidences) 
            .ToList();
    }

    public async Task<List<EvidenceDto>> GetAllEvidencesAsync(int detectiveId) =>
        await context.CaseEvidences
            .Select(ce => new EvidenceDto
            {
                Id = ce.EvidenceId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus
            })
            .ToListAsync();
    
    public async Task<List<EvidenceCaseDto>> GetDeclinedEvidencesAsync(int detectiveId) =>
        await context.CaseEvidences
                .Where(ce => ce.Case.DetectiveId == detectiveId 
                             && ce.ApprovalStatus == ApprovalStatus.Declined)
                .Select(ce => new EvidenceCaseDto
                {
                    EvidenceId = ce.EvidenceId,
                    CaseId = ce.CaseId,
                    Type = ce.Evidence.Type,
                    Description = ce.Evidence.Description,
                    CollectionDate = ce.Evidence.CollectionDate,
                    Region = ce.Evidence.Region,
                    Annotation = ce.Evidence.Annotation,
                    Purpose = ce.Evidence.Purpose,
                    ApprovalStatus = ce.ApprovalStatus,
                })
                .Distinct()
                .ToListAsync();

    public async Task<List<EvidenceCaseDto>> GetApprovedEvidencesAsync(int detectiveId) =>
        await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId 
                         && ce.ApprovalStatus == ApprovalStatus.Approved)
            .Select(ce => new EvidenceCaseDto
            {
                EvidenceId = ce.EvidenceId,
                CaseId = ce.CaseId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus,
            })
            .Distinct()
            .ToListAsync();

    public async Task<EvidenceCaseDto?> GetEvidenceAsync(int evidenceId, int detectiveId) => 
        await context.CaseEvidences
            .Where(ce => ce.EvidenceId == evidenceId && ce.Case.DetectiveId == detectiveId)
            .Select(ce => new EvidenceCaseDto
            {
                EvidenceId = ce.EvidenceId,
                CaseId = ce.CaseId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus,
            })
            .FirstOrDefaultAsync();
    
    public async Task<List<EvidenceCaseDto>> GetEvidencesFromCase(int caseId, int detectiveId)
    {
        var caseExists = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!caseExists)
            throw new EntityNotFoundException("Case", caseId);

        return await context.CaseEvidences
            .Where(ce => ce.CaseId == caseId)
            .Select(ce => new EvidenceCaseDto
            {
                EvidenceId = ce.EvidenceId,
                CaseId = ce.CaseId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus,
            })
            .ToListAsync();
    }

    public async Task<List<EvidenceCaseDto>> GetPendingEvidencesAsync(int detectiveId) =>
        await context.CaseEvidences
            .Where(ce => ce.Case.DetectiveId == detectiveId && ce.ApprovalStatus == ApprovalStatus.Pending)
            .Select(ce => new EvidenceCaseDto
            {
                EvidenceId = ce.EvidenceId,
                CaseId = ce.CaseId,
                Type = ce.Evidence.Type,
                Description = ce.Evidence.Description,
                CollectionDate = ce.Evidence.CollectionDate,
                Region = ce.Evidence.Region,
                Annotation = ce.Evidence.Annotation,
                Purpose = ce.Evidence.Purpose,
                ApprovalStatus = ce.ApprovalStatus,
            })
            .Distinct()
            .ToListAsync();
    
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
            throw new EntityNotFoundException("Evidence", evidenceId);
        if (!hasAccessToCase) 
            throw new EntityNotFoundException("Case", caseId);
        
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
            throw new EntityNotFoundException("Evidence", detectiveId);
        if (!hasAccessToCase) 
            throw new EntityNotFoundException("Case", detectiveId);
        
        var caseEvidenceEntity = await context.CaseEvidences
            .FirstOrDefaultAsync(ce => ce.EvidenceId == evidenceId && ce.CaseId == caseId);
        
        if (caseEvidenceEntity == null)
            throw new InvalidOperationException($"Evidence with Id {evidenceId} and case with Id {caseId} are not linked");
        
        context.CaseEvidences.Remove(caseEvidenceEntity);
        
        await context.SaveChangesAsync();
    }
    #endregion
    
    #region Suspect
    public async Task<SuspectDto> CreateSuspectAsync(int caseId, CreateSuspectDto dto, int detectiveId)
    {
        var hasAccess = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccess) 
            throw new EntityNotFoundException("Case", detectiveId);
        
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
        
        var dtoResult = mapper.Map<SuspectDto>(suspectEntity);
        dtoResult.CaseId = caseId;
        dtoResult.ApprovalStatus = ApprovalStatus.Draft;

        return dtoResult;
    }

    public async Task<SuspectDto> UpdateSuspectAsync(int suspectId, UpdateSuspectDto dto, int detectiveId)
    {
        var caseSuspect = await context.CaseSuspects
            .Include(ce => ce.Case)
            .FirstOrDefaultAsync(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId);

        if (caseSuspect == null)
            throw new EntityNotFoundException("Suspect", detectiveId);
        
        var suspectEntity = await context.Suspects
            .FindAsync(suspectId) ?? throw new EntityNotFoundException("Suspect", suspectId);

        mapper.Map(dto, suspectEntity);

        await context.SaveChangesAsync();
        
        var dtoResult = mapper.Map<SuspectDto>(suspectEntity);
        dtoResult.CaseId = caseSuspect.CaseId;
        dtoResult.ApprovalStatus = caseSuspect.ApprovalStatus;
        
        return dtoResult;
    }

    public async Task DeleteSuspectAsync(int suspectId, int detectiveId)
    {
        var hasAccess = await context.CaseSuspects
            .AnyAsync(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new EntityNotFoundException("Suspect", detectiveId);
        
        var suspectEntity = await context.Suspects
            .FindAsync(suspectId) ?? throw new EntityNotFoundException("Suspect", suspectId);

        context.Suspects.Remove(suspectEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<SuspectDto>> GetAssignedSuspectsAsync(int detectiveId) =>
        await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId)
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync();

    public async Task<List<SuspectDto>> GetUnassignedSuspectsAsync(int detectiveId) =>
        await context.Suspects
            .Where(s => !context.CaseSuspects.Any(cs => cs.SuspectId == s.Id))
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<SuspectDto>> GetSuspectsAsync(int detectiveId)
    {
        var assignedSuspects = await GetAssignedSuspectsAsync(detectiveId);
        var unassignedSuspects = await GetUnassignedSuspectsAsync(detectiveId);

        return assignedSuspects
            .Union(unassignedSuspects)
            .ToList();
    }

    public async Task<List<SuspectDto>> GetDeclinedSuspectsAsync(int detectiveId) =>
        await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Declined)
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync();

    public async Task<List<SuspectDto>> GetApprovedSuspectsAsync(int detectiveId) =>
        await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Approved)
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync();

    public async Task<SuspectDto?> GetSuspectAsync(int suspectId, int detectiveId) =>
        await context.CaseSuspects
            .Where(cs => cs.SuspectId == suspectId && cs.Case.DetectiveId == detectiveId)
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

    public async Task<List<SuspectDto>> GetPendingSuspectsAsync(int detectiveId) =>
        await context.CaseSuspects
            .Where(cs => cs.Case.DetectiveId == detectiveId && cs.ApprovalStatus == ApprovalStatus.Pending)
            .ProjectTo<SuspectDto>(mapper.ConfigurationProvider)
            .Distinct()
            .ToListAsync();

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
            throw new EntityNotFoundException("Suspect", detectiveId);
        if (!hasAccessToCase)
            throw new EntityNotFoundException("Case", detectiveId);

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
            throw new EntityNotFoundException("Suspect", suspectId);
        if (!hasAccessToCase)
            throw new EntityNotFoundException("Case", caseId);

        var caseSuspectEntity = await context.CaseSuspects
            .FirstOrDefaultAsync(cs => cs.SuspectId == suspectId && cs.CaseId == caseId);

        if (caseSuspectEntity == null)
            throw new InvalidOperationException($"Suspect with Id {suspectId} and Case with Id {caseId} are not linked.");

        context.CaseSuspects.Remove(caseSuspectEntity);
        await context.SaveChangesAsync();
    }
    #endregion

    #region Expense
    public async Task<ExpenseDto> CreateExpenseAsync(int caseId, CreateExpenseDto dto, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.CaseId == caseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new EntityNotFoundException("Expense",  detectiveId);
        
        var expenseEntity = mapper.Map<Expense>(dto);
        context.Expenses.Add(expenseEntity);
        await context.SaveChangesAsync();
        
        return mapper.Map<ExpenseDto>(expenseEntity);
    }

    public async Task<ExpenseDto> UpdateExpenseAsync(int expenseId, UpdateExpenseDto dto, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new EntityNotFoundException("Expense",  detectiveId);

        var expenseEntity = await context.Expenses
            .FindAsync(expenseId) ?? throw new EntityNotFoundException("Expense",  expenseId);

        mapper.Map(dto, expenseEntity);
        await context.SaveChangesAsync();
        
        return mapper.Map<ExpenseDto>(expenseEntity);
    }

    public async Task DeleteExpenseAsync(int expenseId, int detectiveId)
    {
        var hasAccess = await context.Expenses
            .AnyAsync(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId);
        
        if (!hasAccess)
            throw new EntityNotFoundException("Expense",  detectiveId);

        var expenseEntity = await context.Expenses
            .FindAsync(expenseId) ?? throw new EntityNotFoundException("Expense", detectiveId);
        
        context.Expenses.Remove(expenseEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(int detectiveId) => 
        await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId)
            .ProjectTo<ExpenseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    
    public async Task<List<ExpenseDto>> GetDeclinedExpensesAsync(int detectiveId) =>
        await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId && e.ApprovalStatus == ApprovalStatus.Declined)
            .ProjectTo<ExpenseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<ExpenseDto>> GetApprovedExpensesAsync(int detectiveId) => 
        await context.Expenses
            .Where(e => e.Case.DetectiveId == detectiveId && e.ApprovalStatus == ApprovalStatus.Approved)
            .ProjectTo<ExpenseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<ExpenseDto?> GetExpenseAsync(int expenseId, int detectiveId) =>
        await context.Expenses
            .Where(e => e.Id == expenseId && e.Case.DetectiveId == detectiveId)
            .ProjectTo<ExpenseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    
    #endregion

    #region Report
    public async Task<ReportDto> CreateReportAsync(int caseId, CreateReportDto dto, int detectiveId)
    {
        var hasAccess = await context.Cases
            .AnyAsync(c => c.Id == caseId && c.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new EntityNotFoundException("Report", detectiveId);

        var reportEntity = mapper.Map<Report>(dto);
        reportEntity.CaseId = caseId;

        context.Reports.Add(reportEntity);
        await context.SaveChangesAsync();

        return mapper.Map<ReportDto>(reportEntity);
    }

    public async Task<ReportDto> UpdateReportAsync(int reportId, UpdateReportDto dto, int detectiveId)
    {
        var hasAccess = await context.Reports
            .AnyAsync(r => r.Id == reportId && r.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new EntityNotFoundException("Report", detectiveId);

        var reportEntity = await context.Reports
            .FindAsync(reportId) ?? throw new EntityNotFoundException("Report", reportId);

        mapper.Map(dto, reportEntity);
        await context.SaveChangesAsync();

        return mapper.Map<ReportDto>(reportEntity);
    }

    public async Task DeleteReportAsync(int reportId, int detectiveId)
    {
        var hasAccess = await context.Reports
            .AnyAsync(r => r.Id == reportId && r.Case.DetectiveId == detectiveId);

        if (!hasAccess)
            throw new EntityNotFoundException("Report", detectiveId);

        var reportEntity = await context.Reports
            .FindAsync(reportId) ?? throw new EntityNotFoundException("Report", reportId);

        context.Reports.Remove(reportEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<ReportDto>> GetReportsAsync(int detectiveId) => 
        await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId)
            .ProjectTo<ReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<ReportDto>> GetPendingReportsAsync(int detectiveId) =>
        await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Pending)
            .ProjectTo<ReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<ReportDto>> GetDeclinedReportsAsync(int detectiveId) =>
        await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Declined)
            .ProjectTo<ReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<ReportDto>> GetApprovedReportsAsync(int detectiveId) =>
        await context.Reports
            .Where(r => r.Case.DetectiveId == detectiveId && r.ApprovalStatus == ApprovalStatus.Approved)
            .ProjectTo<ReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<ReportDto?> GetReportAsync(int reportId, int detectiveId) =>
        await context.Reports
            .Where(r => r.Id == reportId && r.Case.DetectiveId == detectiveId)
            .ProjectTo<ReportDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    #endregion
}