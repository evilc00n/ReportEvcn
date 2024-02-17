﻿using FluentValidation;
using ReportEvcn.Domain.Dto.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Application.Validations.FluentFalidations.Report
{
    public class UpdateReportValidator : AbstractValidator<UpdateReportDTO>
    {
        public UpdateReportValidator()
        {
            RuleFor(x=> x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        }
    }
}