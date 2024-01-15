using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Domain.Dto.Report
{
    public record CreateReportDTO(string Name, string Description, long UserId);
}
