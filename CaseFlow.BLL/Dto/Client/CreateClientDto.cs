namespace CaseFlow.BLL.Dto.Client;

public class CreateClientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? FatherName { get; set; }

    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Region { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
    public int? ApartmentNumber { get; set; }
}