using AutoMapper;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Entity;

namespace ReportEvcn.Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
