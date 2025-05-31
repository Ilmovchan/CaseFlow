using AutoMapper;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.BLL.Dto.Client;
using CaseFlow.BLL.Dto.Detective;
using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Services;

public class AdminService(DetectiveAgencyDbContext context, IMapper mapper, IPostgresUserService userService) :
    IAdminCaseService, IAdminClientService, IAdminDetectiveService, IAdminCaseTypeService,
    IAdminEvidenceService, IAdminExpenseService, IAdminReportService, IAdminSuspectService

{
    #region Case
    
    public async Task<Case> CreateCaseAsync(CreateCaseDto dto)
    {
        var caseEntity = mapper.Map<Case>(dto);

        context.Cases.Add(caseEntity);
        await context.SaveChangesAsync();

        return caseEntity;
    }
    
    public async Task<Case> UpdateCaseAsync(int id, UpdateCaseByAdminDto dto)
    {
        var caseEntity = await context.Cases
            .FindAsync(id) ?? throw new EntityNotFoundException("Case", id);

        mapper.Map(dto, caseEntity);
        await context.SaveChangesAsync();

        return caseEntity;
    }

    public async Task DeleteCaseAsync(int caseId)
    {
        var caseEntity = await context.Cases.FindAsync(caseId)
                         ?? throw new EntityNotFoundException("Case", caseId);

        context.CaseEvidences.RemoveRange(
            await context.CaseEvidences
                .Where(ce => ce.CaseId == caseId)
                .ToListAsync());

        context.CaseSuspects.RemoveRange(
            await context.CaseSuspects
                .Where(cs => cs.CaseId == caseId)
                .ToListAsync());

        context.Cases.Remove(caseEntity);

        await context.SaveChangesAsync();
    }

    public async Task<Case?> GetCaseAsync(int caseId)
    {
        return await context.Cases.FindAsync(caseId);
    }

    public async Task<List<Case>> GetCasesAsync()
    {
        return await context.Cases.ToListAsync();
    }

    #endregion

    #region Client

        public async Task<Client> CreateClientAsync(CreateClientDto dto)
    {
        var clientEntity = mapper.Map<Client>(dto);

        context.Clients.Add(clientEntity);
        await context.SaveChangesAsync();

        return clientEntity;
    }

    public async Task<Client> UpdateClientAsync(int id, UpdateClientDto dto)
    {
        var clientEntity = await context.Clients
            .FindAsync(id) ?? throw new EntityNotFoundException("Client", id);

        mapper.Map(dto, clientEntity);
        await context.SaveChangesAsync();
        
        return clientEntity;
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var clientEntity = await context.Clients
            .FindAsync(clientId) ?? throw new EntityNotFoundException("Client", clientId);
        
        context.Clients.Remove(clientEntity);
        await context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientAsync(int clientId)
    {
        return await context.Clients.FindAsync(clientId);
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        return await context.Clients.ToListAsync();
    }

    #endregion
    
    #region Detective
    
    public async Task<Detective> CreateDetectiveAsync(CreateDetectiveDto dto)
    {
        var detectiveEntity = mapper.Map<Detective>(dto);

        context.Detectives.Add(detectiveEntity);
        await context.SaveChangesAsync();

        var lastName = dto.LastName.ToLower();
        var username = $"detective_{detectiveEntity.Id}_{lastName}";
        var password = GeneratePassword();

        await userService.CreateUserAsync(username, password, "detective");

        return detectiveEntity;
    }

    public async Task<Detective> UpdateDetectiveAsync(int id, UpdateDetectiveDto dto)
    {
        var existingDetective = await context.Detectives
            .FindAsync(id) ?? throw new EntityNotFoundException("Detective", id);

        var oldLastName = existingDetective.LastName;
        var oldStatus = existingDetective.Status;

        mapper.Map(dto, existingDetective);
        await context.SaveChangesAsync();

        if (!string.Equals(oldLastName, existingDetective.LastName, StringComparison.OrdinalIgnoreCase))
        {
            var oldUsername = BuildUsername(oldLastName, existingDetective.Id);
            var newUsername = BuildUsername(existingDetective.LastName, existingDetective.Id);
            await userService.UpdateUsernameAsync(oldUsername, newUsername);
        }
        
        if (existingDetective.Status != oldStatus)
        {
            var username = BuildUsername(existingDetective.LastName, existingDetective.Id);

            switch (existingDetective.Status)
            {
                case DetectiveStatus.Active or DetectiveStatus.OnVacation:
                    await userService.EnableUserAsync(username);
                    break;
                case DetectiveStatus.Fired or DetectiveStatus.Retired:
                    await userService.DisableUserAsync(username);
                    break;
            }
        }

        return existingDetective;

        string BuildUsername(string lastName, int dId) => $"detective_{dId}_{lastName.ToLower()}";
    }

    public async Task DeleteDetectiveAsync(int detectiveId)
    {
        var deletedDetective = await context.Detectives
            .FindAsync(detectiveId) ?? throw new EntityNotFoundException("Detective", detectiveId);

        context.Detectives.Remove(deletedDetective);
        await context.SaveChangesAsync();

        await userService.DeleteUserAsync($"detective_{detectiveId}_{deletedDetective.LastName}");
    }

    public async Task<Detective?> GetDetectiveAsync(int detectiveId)
    {
        return await context.Detectives.FindAsync(detectiveId);
    }

    public async Task<List<Detective>> GetDetectivesAsync()
    {
        return await context.Detectives.ToListAsync();
    }

    public async Task<List<Detective>> GetUnassignedDetectivesAsync()
    {
        return await context.Detectives
            .Where(d => !context.Cases.Any(c => c.DetectiveId == d.Id))
            .ToListAsync();
    }

    public async Task<(Case, Detective)> AssignDetectiveAsync(int caseId, int detectiveId)
    {
        var caseEntity = await context.Cases
            .FindAsync(caseId) ?? throw new EntityNotFoundException("Case", caseId);
        
        var detectiveEntity = await context.Detectives
            .FindAsync(detectiveId) ?? throw new EntityNotFoundException("Detective", detectiveId);
        
        caseEntity.DetectiveId = detectiveId;
        await context.SaveChangesAsync();

        return (caseEntity, detectiveEntity);
    }

    public async Task DismissDetectiveAsync(int caseId)
    {
        var caseEntity = await context.Cases
            .FindAsync(caseId) ?? throw new EntityNotFoundException("Case", caseId);

        caseEntity.DetectiveId = null;
        await context.SaveChangesAsync();
    }

    #endregion

    #region Evidence
    
    public async Task<Evidence?> GetEvidenceAsync(int evidenceId)
    {
        return await context.Evidences.FindAsync(evidenceId);
    }

    public async Task<List<Evidence>> GetEvidencesFromCaseAsync(int caseId)
    {
        return await context.CaseEvidences
            .Where(ce => ce.CaseId == caseId)
            .Select(ce => ce.Evidence)
            .ToListAsync();
    }

    public async Task<List<Evidence>> GetEvidencesAsync()
    {
        return await context.Evidences.ToListAsync();
    }

    public async Task<List<Evidence>> GetPendingEvidencesAsync()
    {
        return await context.CaseEvidences
            .Where(ce => ce.ApprovalStatus == ApprovalStatus.Submitted)
            .Select(ce => ce.Evidence)
            .ToListAsync();
    }
    
    #endregion
    
    #region Expense
    
    public async Task<Expense?> GetExpenseAsync(int expenseId)
    {
        return await context.Expenses.FindAsync(expenseId);
    }

    public async Task<List<Expense>> GetExpensesFromCaseAsync(int caseId)
    {
        return await context.Expenses
            .Where(e => e.CaseId == caseId)
            .ToListAsync();
    }

    public async Task<List<Expense>> GetExpensesAsync()
    {
        return await context.Expenses.ToListAsync();
    }

    public async Task<List<Expense>> GetPendingExpensesAsync()
    {
        return await context.Expenses
            .Where(e => e.ApprovalStatus == ApprovalStatus.Submitted)
            .ToListAsync();
    }
    
    #endregion

    #region Report
    
    public async Task<Report?> GetReportAsync(int reportId)
    {
        return await context.Reports.FindAsync(reportId);
    }

    public async Task<List<Report>> GetReportsFromCaseAsync(int caseId)
    {
        return await context.Reports
            .Where(r => r.CaseId == caseId)
            .ToListAsync();
    }

    public async Task<List<Report>> GetReportsAsync()
    {
        return await context.Reports.ToListAsync();
    }

    public async Task<List<Report>> GetPendingReportsAsync()
    {
        return await context.Reports
            .Where(r => r.ApprovalStatus == ApprovalStatus.Submitted)
            .ToListAsync();
    }
    
    #endregion

    #region Suspect
    
    public async Task<Suspect?> GetSuspectAsync(int suspectId)
    {
        return await context.Suspects.FindAsync(suspectId);
    }

    public async Task<List<Suspect>> GetSuspectsFromCaseAsync(int caseId)
    {
        return await context.CaseSuspects
            .Where(cs => cs.CaseId == caseId)
            .Select(cs => cs.Suspect)
            .ToListAsync();
    }

    public async Task<List<Suspect>> GetSuspectsAsync()
    {
        return await context.Suspects.ToListAsync();
    }

    public async Task<List<Suspect>> GetPendingSuspectsAsync()
    {
        return await context.CaseSuspects
            .Where(cs => cs.ApprovalStatus == ApprovalStatus.Submitted)
            .Select(cs => cs.Suspect)
            .ToListAsync();
    }
    
    #endregion
    
    #region CaseType
    
    public async Task<CaseType> CreateCaseTypeAsync(CreateCaseTypeDto dto)
    {
        var caseTypeEntity = mapper.Map<CaseType>(dto);

        context.Add(caseTypeEntity);
        await context.SaveChangesAsync();
        
        return caseTypeEntity;
    }

    public async Task<CaseType> UpdateCaseTypeAsync(int id, UpdateCaseTypeDto dto)
    {
        var caseTypeEntity = await context.CaseTypes
            .FindAsync(id) ?? throw new EntityNotFoundException("CaseType", id);

        mapper.Map(dto, caseTypeEntity);
        await context.SaveChangesAsync();
        
        return caseTypeEntity;
    }

    public async Task DeleteCaseTypeAsync(int caseTypeId)
    {
        var caseTypeEntity = await context.CaseTypes
            .FindAsync(caseTypeId) ?? throw new EntityNotFoundException("CaseType", caseTypeId);
            
        context.CaseTypes.Remove(caseTypeEntity);
        await context.SaveChangesAsync();
    }

    public async Task<List<CaseType>> GetCaseTypesAsync()
    {
        return await context.CaseTypes.ToListAsync();
    }
    
    #endregion
    
    private string GeneratePassword()
    {
        var rand = new Random();
        var chars = new List<char>();
        
        for (var i = 0; i < rand.Next(1, 5); i++)
            chars.Add((char)rand.Next('0', '9' + 1));
        
        for (var i = 0; i < rand.Next(1, 5); i++)
            chars.Add((char)rand.Next('A', 'Z' + 1));
        
        for (var i = 0; i < rand.Next(1, 3); i++)
            chars.Add((char)rand.Next('a', 'z' + 1));
        
        while (chars.Count < 8)
            chars.Add((char)rand.Next('0', '9' + 1));
        
        return new string(chars.OrderBy(_ => rand.Next()).ToArray());
    }
}