using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Detective
{
    public int Id { get; set; }

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

    public DateOnly HireDate { get; set; }

    public decimal Salary { get; set; }

    public string? PersonalNotes { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
