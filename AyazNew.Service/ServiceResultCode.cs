using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Service
{
    public enum ServiceResultCode : int
    {
        Success = 0,
        Generic = 1,
        RecordNotFound = 2,
        UnAuthorizedAccess = 400,
        SP_Error = 3
    }
}
