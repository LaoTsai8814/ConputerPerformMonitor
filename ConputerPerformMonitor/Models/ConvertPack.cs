using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.Models
{
    public class ConvertPack
    {

    }
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public int ToMillis()
        {
            return (Hour * 3600 + Minute * 60 + Second) * 1000;
        }


    }
}
