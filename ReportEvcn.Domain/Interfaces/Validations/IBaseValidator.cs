﻿using ReportEvcn.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Domain.Interfaces.Validations
{
    public interface IBaseValidator<T> where T : class 
    {
        BaseResult ValidateOnNull(T model);
    }
}
