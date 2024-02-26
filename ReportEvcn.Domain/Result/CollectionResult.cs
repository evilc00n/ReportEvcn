﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Domain.Result
{
    public class CollectionResult<T> : BaseResult<IEnumerable<T>>
    {
        public int Count { get; set; }
    }
}
