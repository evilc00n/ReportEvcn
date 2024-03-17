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
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
