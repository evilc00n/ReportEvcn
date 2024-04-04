using AutoMapper;
using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Entity;

namespace ReportEvcn.Application.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {

            CreateMap<Role, RoleDTO>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name));


            CreateMap<RoleDTO, Role>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name));
        }
    }
}
