using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }

    public class ServiceMessage<T> : ServiceMessage
    {
        public T? Data { get; set; }
    }
}
