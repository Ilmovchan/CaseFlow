using System.ComponentModel.DataAnnotations;

namespace CaseFlow.API.Dtos;

public record ClientDto(
    [Required] int Id, 
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string LastName,
    [Required][StringLength(20)] string FatherName);