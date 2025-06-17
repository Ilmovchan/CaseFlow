using AutoMapper;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.BLL.Dto.Client;
using CaseFlow.BLL.Dto.Detective;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.MappingProfiles;

public class CaseFlowMappingProfile : Profile
{
    public CaseFlowMappingProfile()
    {
        CreateMap<CreateCaseDto, Case>();
        
        CreateMap<UpdateCaseByAdminDto, Case>()
            .ForMember(dest => dest.CaseTypeId, opt => opt.Condition(src => src.CaseTypeId.HasValue))
            .ForMember(dest => dest.ClientId, opt => opt.Condition(src => src.ClientId.HasValue))
            .ForMember(dest => dest.DetectiveId, opt => opt.Condition(src => src.DetectiveId.HasValue))
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.DeadlineDate, opt => opt.Condition(src => src.DeadlineDate.HasValue))
            .ForMember(dest => dest.CloseDate, opt => opt.Condition(src => src.CloseDate.HasValue))
            .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status.HasValue));

        CreateMap<CreateCaseTypeDto, CaseType>();

        CreateMap<UpdateCaseTypeDto, CaseType>()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
            .ForMember(price => price.Price, opt => opt.Condition(src => src.Price != null));
        
        CreateMap<CreateClientDto, Client>();
        
        CreateMap<UpdateClientDto, Client>()
            .ForMember(dest => dest.ApartmentNumber, opt => opt.Condition(src => src.ApartmentNumber != null))
            .ForMember(dest => dest.BuildingNumber, opt => opt.Condition(src => src.BuildingNumber != null))
            .ForMember(dest => dest.City, opt => opt.Condition(src => src.City != null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.Condition(src => src.DateOfBirth.HasValue))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
            .ForMember(dest => dest.FatherName, opt => opt.Condition(src => src.FatherName != null))
            .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != null))
            .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != null))
            .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
            .ForMember(dest => dest.Region, opt => opt.Condition(src => src.Region != null))
            .ForMember(dest => dest.Street, opt => opt.Condition(src => src.Street != null));

        CreateMap<CreateDetectiveDto, Detective>();

        CreateMap<UpdateDetectiveDto, Detective>()
            .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != null))
            .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != null))
            .ForMember(dest => dest.FatherName, opt => opt.Condition(src => src.FatherName != null))
            .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.Condition(src => src.DateOfBirth.HasValue))
            .ForMember(dest => dest.Region, opt => opt.Condition(src => src.Region != null))
            .ForMember(dest => dest.City, opt => opt.Condition(src => src.City != null))
            .ForMember(dest => dest.Street, opt => opt.Condition(src => src.Street != null))
            .ForMember(dest => dest.BuildingNumber, opt => opt.Condition(src => src.BuildingNumber != null))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.Condition(src => src.ApartmentNumber.HasValue))
            .ForMember(dest => dest.Salary, opt => opt.Condition(src => src.Salary.HasValue))
            .ForMember(dest => dest.PersonalNotes, opt => opt.Condition(src => src.PersonalNotes != null));
    }
}