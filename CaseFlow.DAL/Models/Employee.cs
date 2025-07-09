using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? FatherName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int AddressId { get; set; }

    public DateTime HireDate { get; set; }

    public DateTime? TerminationDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User? User { get; set; }
}
