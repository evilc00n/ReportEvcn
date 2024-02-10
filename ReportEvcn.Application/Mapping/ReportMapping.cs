using AutoMapper;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Entity;


namespace ReportEvcn.Application.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDTO>().ReverseMap();
        }
    }
}
