using AutoMapper;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Entity;

namespace ReportEvcn.Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDTO>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Login", m => m.MapFrom(s => s.Login));




            CreateMap<UserDTO, User>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Login", m => m.MapFrom(s => s.Login));

        }
    }
}
