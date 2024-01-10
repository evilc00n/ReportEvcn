using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Domain.Result
{
    public class BaseResult
    {
        public string ErrorMesage { get; set; }
        public bool IsSuccess => ErrorMesage == null;
        
        public int ErrorCode { get; set; }

    }

    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }


        public BaseResult(string errorMessage, int errorCode, T data)
        {
            ErrorMesage = errorMessage;
            ErrorCode = errorCode;
            Data = data;    
        }

        public BaseResult() { }
    }
}
