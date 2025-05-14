using CaseFlow.BLL.Common;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.BLL.Dto.Client;
using CaseFlow.BLL.Dto.Detective;
using CaseFlow.BLL.Interfaces.IAdmin;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Services.AdminServices;

public class AdminService : 
    IAdminCaseService, IAdminClientService, IAdminDetectiveService, IAdminCaseTypeService, 
    IAdminEvidenceService,IAdminExpenseService, IAdminReportService, IAdminSuspectService
    
{
    private readonly DetectiveAgencyDbContext _context;
    
    public AdminService(DetectiveAgencyDbContext context) 
        =>  _context = context;
    
    #region Case
    
    public async Task<Case> CreateCaseAsync(CreateCaseDto dto)
    {
        var newCase = new Case
        {
            ClientId = dto.ClientId,
            DetectiveId = dto.DetectiveId,
            CaseTypeId = dto.CaseTypeId,
            Title = dto.Title,
            Description = dto.Description,
            DeadlineDate = dto.DeadlineDate,
        };

        _context.Cases.Add(newCase);
        await _context.SaveChangesAsync();

        return newCase;
    }
    
    public async Task<Case> UpdateCaseAsync(UpdateCaseByAdminDto dto)
    {
        var existingCase = (await _context.Cases.FindAsync(dto.Id))!
            .EnsureExists("Case", dto.Id);
        
        if (dto.ClientId != null)
            existingCase.ClientId = dto.ClientId.Value;
        
        if (dto.DetectiveId != null)
            existingCase.DetectiveId = dto.DetectiveId.Value;
        
        if  (dto.CaseTypeId != null)
            existingCase.CaseTypeId = dto.CaseTypeId.Value;
        
        if (dto.Title != null)
            existingCase.Title = dto.Title;
        
        if (dto.Description != null)
            existingCase.Description = dto.Description;
        
        if  (dto.DeadlineDate != null)
            existingCase.DeadlineDate = dto.DeadlineDate.Value;
        
        if (dto.StartDate != null)
            existingCase.StartDate = dto.StartDate.Value;
        
        if (dto.CloseDate != null)
            existingCase.CloseDate = dto.CloseDate.Value;
        
        if (dto.Status != null)
            existingCase.Status = dto.Status.Value;

        await _context.SaveChangesAsync();
        return existingCase;
    }

    public async Task DeleteCaseAsync(int caseId)
    {
        var deletedCase = (await _context.Cases.FindAsync(caseId))!
            .EnsureExists("Case", caseId);
        
        _context.Cases.Remove(deletedCase);
        await _context.SaveChangesAsync();
    }

    public Task<Case?> GetCaseAsync(int caseId)
    {
        return _context.Cases.FindAsync(caseId).AsTask();
    }

    public Task<List<Case>> GetCasesAsync()
    {
        return _context.Cases.ToListAsync();
    }

    #endregion

    #region Client

        public async Task<Client> AddClientAsync(CreateClientDto dto)
    {
        var newClient = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            FatherName = dto.FatherName,
            PhoneNumber = dto.PhoneNumber, 
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            Region = dto.Region,
            City = dto.City,
            Street = dto.Street,
            BuildingNumber = dto.BuildingNumber,
            ApartmentNumber = dto.ApartmentNumber,
        };

        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        return newClient;
    }

    public async Task<Client> UpdateClientAsync(UpdateClientDto dto)
    {
        var existingClient = (await _context.Clients.FindAsync(dto.Id))!
            .EnsureExists("Client", dto.Id);
        
        if (dto.FirstName != null)
            existingClient.FirstName = dto.FirstName;
        
        if (dto.LastName != null)
            existingClient.LastName = dto.LastName;
        
        if (dto.FatherName != null)
            existingClient.FatherName = dto.FatherName;

        if (dto.PhoneNumber != null)
            existingClient.PhoneNumber = dto.PhoneNumber;
        
        if (dto.Email != null)
            existingClient.Email = dto.Email;
        
        if (dto.DateOfBirth != null)
            existingClient.DateOfBirth = dto.DateOfBirth.Value;
        
        if (dto.Region != null)
            existingClient.Region = dto.Region;
        
        if (dto.City != null)
            existingClient.City = dto.City;
        
        if (dto.Street != null)
            existingClient.Street = dto.Street;
        
        if (dto.BuildingNumber != null)
            existingClient.BuildingNumber = dto.BuildingNumber;
        
        if (dto.ApartmentNumber != null)
            existingClient.ApartmentNumber = dto.ApartmentNumber;
        
        await _context.SaveChangesAsync();
        return existingClient;
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var deletedClient = (await _context.Clients.FindAsync(clientId))!
            .EnsureExists("Client", clientId);
            
        _context.Clients.Remove(deletedClient);
        await _context.SaveChangesAsync();
    }

    public Task<Client?> GetClientAsync(int clientId)
    {
        return _context.Clients.FindAsync(clientId).AsTask();
    }

    public Task<List<Client>> GetClientsAsync()
    {
        return  _context.Clients.ToListAsync();
    }

    #endregion
    
    #region Detective
    
    public async Task<Detective> AddDetectiveAsync(CreateDetectiveDto dto)
    { 
        var newDetective = new Detective 
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            FatherName = dto.FatherName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            Region = dto.Region,
            City = dto.City,
            Street = dto.Street,
            BuildingNumber = dto.BuildingNumber,
            ApartmentNumber = dto.ApartmentNumber,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            PersonalNotes = dto.PersonalNotes,
        };

        _context.Detectives.Add(newDetective);
        await _context.SaveChangesAsync();

        return newDetective;
    }

    public async Task<Detective> UpdateDetectiveAsync(UpdateDetectiveDto dto)
    {
        var existingDetective = (await _context.Detectives.FindAsync(dto.Id))!
            .EnsureExists("Detective", dto.Id);

        if (dto.FirstName != null)
            existingDetective.FirstName = dto.FirstName;

        if (dto.LastName != null)
            existingDetective.LastName = dto.LastName;

        if (dto.FatherName != null)
            existingDetective.FatherName = dto.FatherName;

        if (dto.PhoneNumber != null)
            existingDetective.PhoneNumber = dto.PhoneNumber;

        if (dto.Email != null)
            existingDetective.Email = dto.Email;

        if (dto.DateOfBirth != null)
            existingDetective.DateOfBirth = dto.DateOfBirth.Value;

        if (dto.Region != null)
            existingDetective.Region = dto.Region;
        
        if (dto.City != null)
            existingDetective.City = dto.City;
        
        if (dto.Street != null)
            existingDetective.Street = dto.Street;
        
        if (dto.BuildingNumber != null)
            existingDetective.BuildingNumber = dto.BuildingNumber;
        
        if (dto.ApartmentNumber != null)
            existingDetective.ApartmentNumber = dto.ApartmentNumber;
        
        if (dto.HireDate != null)
            existingDetective.HireDate = dto.HireDate.Value;
        
        if (dto.Salary != null)
            existingDetective.Salary = dto.Salary.Value;
        
        if (dto.PersonalNotes != null)
            existingDetective.PersonalNotes = dto.PersonalNotes;
        
        if (dto.Status != null)
            existingDetective.Status = dto.Status.Value;

        await _context.SaveChangesAsync();
        return existingDetective;
    }

    public async Task DeleteDetectiveAsync(int detectiveId)
    {
        var deletedDetective = (await _context.Detectives.FindAsync(detectiveId))!
            .EnsureExists("Detective", detectiveId);

        _context.Detectives.Remove(deletedDetective);
        await _context.SaveChangesAsync();
    }

    public Task<Detective?> GetDetectiveAsync(int detectiveId)
    {
        return _context.Detectives.FindAsync(detectiveId).AsTask();
    }

    public Task<List<Detective>> GetDetectivesAsync()
    {
        return _context.Detectives.ToListAsync();
    }

    public Task<List<Detective>> GetUnassignedDetectivesAsync()
    {
        return _context.Detectives
            .Where(d => !_context.Cases.Any(c => c.DetectiveId == d.Id))
            .ToListAsync();
    }

    public async Task<(Case, Detective)> AssignDetectiveAsync(int caseId, int detectiveId)
    {
        var caseEntity = (await _context.Cases.FindAsync(caseId))!
            .EnsureExists("Case", caseId);
        var detectiveEntity = (await _context.Detectives.FindAsync(detectiveId))!
            .EnsureExists("Detective", detectiveId);
        
        caseEntity!.DetectiveId = detectiveId;
        await _context.SaveChangesAsync();

        return (caseEntity, detectiveEntity);
    }

    public async Task DismissDetectiveAsync(int caseId)
    {
        var updatedCase = (await _context.Cases.FindAsync(caseId))!
            .EnsureExists("Case", caseId);

        updatedCase.DetectiveId = null;
        await _context.SaveChangesAsync();
    }

    #endregion

    #region Evidence
    
    public Task<Evidence?> GetEvidenceAsync(int evidenceId)
    {
        return _context.Evidences.FindAsync(evidenceId).AsTask();
    }

    public Task<List<Evidence>> GetEvidencesFromCaseAsync(int caseId)
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.CaseId == caseId)
            .Select(ce => ce.Evidence)
            .ToListAsync();
    }

    public Task<List<Evidence>> GetEvidencesAsync()
    {
        return _context.Evidences.ToListAsync();
    }

    public Task<List<Evidence>> GetPendingEvidencesAsync()
    {
        return _context.Set<CaseEvidence>()
            .Where(ce => ce.ApprovalStatus == ApprovalStatus.Submitted)
            .Select(ce => ce.Evidence)
            .ToListAsync();
    }
    
    #endregion
    
    #region Expense
    
    public Task<Expense?> GetExpenseAsync(int expenseId)
    {
        return _context.Expenses.FindAsync(expenseId).AsTask();
    }

    public Task<List<Expense>> GetExpensesFromCaseAsync(int caseId)
    {
        return _context.Expenses
            .Where(e => e.CaseId == caseId)
            .ToListAsync();
    }

    public Task<List<Expense>> GetExpensesAsync()
    {
        return  _context.Expenses.ToListAsync();
    }

    public Task<List<Expense>> GetPendingExpensesAsync()
    {
        return _context.Expenses
            .Where(e => e.ApprovalStatus == ApprovalStatus.Submitted)
            .ToListAsync();
    }
    
    #endregion

    #region Report
    
    public Task<Report?> GetReportAsync(int reportId)
    {
        return _context.Reports.FindAsync(reportId).AsTask();
    }

    public Task<List<Report>> GetReportsFromCaseAsync(int caseId)
    {
        return _context.Reports
            .Where(r => r.CaseId == caseId)
            .ToListAsync();
    }

    public Task<List<Report>> GetReportsAsync()
    {
        return  _context.Reports.ToListAsync();
    }

    public Task<List<Report>> GetPendingReportsAsync()
    {
        return _context.Reports
            .Where(r => r.ApprovalStatus == ApprovalStatus.Submitted)
            .ToListAsync();
    }
    
    #endregion

    #region Suspect
    
    public Task<Suspect?> GetSuspectAsync(int suspectId)
    {
        return _context.Suspects.FindAsync(suspectId).AsTask();
    }

    public Task<List<Suspect>> GetSuspectsFromCaseAsync(int caseId)
    {
        return _context.Set<CaseSuspect>()
            .Where(cs => cs.CaseId == caseId)
            .Select(cs => cs.Suspect)
            .ToListAsync();
    }

    public Task<List<Suspect>> GetSuspectsAsync()
    {
        return _context.Suspects.ToListAsync();
    }

    public Task<List<Suspect>> GetPendingSuspectsAsync()
    {
        return _context.Set<CaseSuspect>()
            .Where(cs => cs.ApprovalStatus == ApprovalStatus.Submitted)
            .Select(cs => cs.Suspect)
            .ToListAsync();
    }
    
    #endregion
    
    #region CaseType
    
    public async Task<CaseType> CreateCaseTypeAsync(CreateCaseTypeDto dto)
    {
        var newCaseType = new CaseType
        {
            Name = dto.Name,
            Price = dto.Price,
        };

        _context.Add(newCaseType);
        await _context.SaveChangesAsync();
        
        return newCaseType;
    }

    public async Task<CaseType> UpdateCaseTypeAsync(UpdateCaseTypeDto dto)
    {
        var existingCaseType = (await _context.CaseTypes.FindAsync(dto.Id))!
            .EnsureExists("CaseType", dto.Id);
        
        if (dto.Name != null)
            existingCaseType.Name = dto.Name;

        if (dto.Price != null)
            existingCaseType.Price = dto.Price.Value;
        
        await _context.SaveChangesAsync();
        return existingCaseType;
    }

    public async Task DeleteCaseTypeAsync(int caseTypeId)
    {
        var deletedCaseType = (await _context.CaseTypes.FindAsync(caseTypeId))!
            .EnsureExists("CaseType", caseTypeId);
            
        _context.CaseTypes.Remove(deletedCaseType);
        await _context.SaveChangesAsync();
    }

    public Task<List<CaseType>> GetCaseTypesAsync()
    {
        return _context.CaseTypes.ToListAsync();
    }
    
    #endregion
}