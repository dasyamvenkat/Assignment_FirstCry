using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.Utitlities
{
    public class Logs
    {
        public  void Info(string message)
        {
            NLog.LogManager.Setup().LoadConfiguration(builder => {
                builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "file.txt");
            });
            var log = LogManager.GetCurrentClassLogger();
            log.Info(message);
        }
    }
}
