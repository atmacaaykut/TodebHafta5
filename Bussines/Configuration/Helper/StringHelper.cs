using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Configuration.Helper
{
    public static class StringHelper
    {
        public static string CreateCacheKey(string userName, int userId)
        {
           return string.Concat(userName.Substring(0, 3), "_", userId);
        }
    }
}
