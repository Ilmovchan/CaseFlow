using System;
using System.Collections.Generic;

namespace CaseFlow.DAL.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string Region { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int BuildingNumber { get; set; }

    public int? ApartmentNumber { get; set; }

    public string? PostalCode { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Evidence> Evidences { get; set; } = new List<Evidence>();

    public virtual ICollection<Suspect> Suspects { get; set; } = new List<Suspect>();

    public virtual ICollection<Witness> Witnesses { get; set; } = new List<Witness>();
}
