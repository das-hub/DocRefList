using AutoMapper;
using DocRefList.Models.AdminViewModels;
using DocRefList.Models.Entities;

namespace DocRefList.Models.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<RegisterViewModel, Employee>()
                .ForMember(e => e.UserName, opt => opt.MapFrom(r => r.Email));
            CreateMap<UpdateViewModel, Employee>()
                .ForMember(e => e.UserName, opt => opt.MapFrom(r => r.Email))
                .ReverseMap();
        }
    }
}
