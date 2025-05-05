using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFlow.DAL.Models;

[Table("client")]
public class Client
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("apartment_number")]
    public int? ApartmentNumber { get; set; }

    [Column("building_number")]
    [MaxLength(30)]
    public string? BuildingNumber { get; set; }

    [Column("city")]
    [MaxLength(30)]
    public string? City { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("father_name")]
    [MaxLength(100)]
    public string? FatherName { get; set; }

    [Column("first_name")]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [MaxLength(100)]
    public string? LastName { get; set; }

    [Column("phone_number")]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("region")]
    [MaxLength(30)]
    public string? Region { get; set; }

    [Column("registration_date")] 
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    [Column("street")]
    [MaxLength(50)]
    public string? Street { get; set; }
    
    public ICollection<Case>? Cases { get; set; }
}