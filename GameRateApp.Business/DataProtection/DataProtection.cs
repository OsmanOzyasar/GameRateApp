using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protection;
        public DataProtection(IDataProtectionProvider provider)
        {
            _protection = provider.CreateProtector("GameRateApp-security-v1");
        }
        public string Protect(string password)
        {
            return _protection.Protect(password);
        }

        public string UnProtect(string protectedPassword)
        {
            return _protection.Unprotect(protectedPassword);
        }
    }
}
