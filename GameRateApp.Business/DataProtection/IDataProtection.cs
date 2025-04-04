using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.DataProtection
{
    public interface IDataProtection
    {
        string Protect(string password);
        string UnProtect(string protectedPassword);
    }
}
