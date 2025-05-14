using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Detective;

public class UpdateDetectiveDto
{
    public int Id { get; init; } 
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? BuildingNumber { get; set; }
    public int? ApartmentNumber { get; set; }

    public DateOnly? HireDate { get; set; }

    public decimal? Salary { get; set; }

    public string? PersonalNotes { get; set; }
    
    public DetectiveStatus? Status { get; set; }
}