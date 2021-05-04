using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Service
{
    public class ServiceResult
    {
        public ServiceResult()
        {

        }

        public ServiceResult(ServiceResultCode resultCode)
        {
            ResultCode = resultCode;
        }

        public ServiceResult(ServiceResultCode resultCode, string resultMessage)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }


        public bool HasError => ResultCode != ServiceResultCode.Success;
        public string ResultMessage { get; set; } = string.Empty;
        public ServiceResultCode ResultCode { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(ServiceResultCode resultCode) : base(resultCode)
        {
        }

        public ServiceResult(ServiceResultCode resultCode, string resultMessage) : base(resultCode, resultMessage)
        {
        }


        public ServiceResult(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

    }
}
