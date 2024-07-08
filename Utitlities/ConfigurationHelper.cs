using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.Utitlities
{
    public static class ConfigurationHelper
    {
        private static IConfiguration Configuration { get; set; }
        public static T Get<T>(string name)
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            Configuration = new ConfigurationBuilder().AddJsonFile(path + "appsettings.json", optional: false, reloadOnChange: true).Build();
            var value = Configuration["AppSettings:" + name];

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
