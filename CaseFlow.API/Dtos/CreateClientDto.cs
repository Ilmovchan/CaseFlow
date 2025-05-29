using System.ComponentModel.DataAnnotations;

namespace CaseFlow.API.Dtos;

public record CreateClientDto(    
    [Required][StringLength(20)] string FirstName,
    [Required][StringLength(20)] string LastName,
    [Required][StringLength(20)] string FatherName);