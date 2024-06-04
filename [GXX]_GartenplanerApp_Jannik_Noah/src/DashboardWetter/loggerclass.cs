using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;

namespace DashboardWetter
{
    public static class Loggerclass
    {
        public static Logger log = new LoggerConfiguration().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7).CreateLogger();
    }
}
