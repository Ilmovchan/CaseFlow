using AutoMapper;
using CaseFlow.BLL.Dto.Case;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Profiles;

public class CaseFlowMappingProfile : Profile
{
    public CaseFlowMappingProfile()
    {
        CreateMap<CreateCaseDto, Case>();
        CreateMap<UpdateCaseByAdminDto, Case>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}