using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Monads;

namespace BIC.Utils
{
    public static class StatusCheck
    {
        public static bool IsNullOrEmtpy(this string s)
        {
            return s.Return(s1 => s1.Trim() == "", true);
        }
    }
}
