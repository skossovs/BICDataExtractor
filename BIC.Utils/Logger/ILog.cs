using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public interface ILog
    {
        void Info(string message, params object[] p);
        void Warning(string message, params object[] p);
        void Error(string message, params object[] p);
        void Debug(string message, params object[] p);
        void ReportException(Exception ex);
    }
}
