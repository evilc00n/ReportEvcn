using AutoMapper;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
