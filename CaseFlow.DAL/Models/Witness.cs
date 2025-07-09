using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Witness
{
    public int WitnessId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? FatherName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int AddressId { get; set; }

    public string? Notes { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual CaseWitness? CaseWitness { get; set; }
}
