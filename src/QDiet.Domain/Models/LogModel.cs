using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDiet.Domain.Models
{
    public enum LogType
    {
        Information,
        Warning,
        Error
    }

    public class LogModel
    {
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
        public LogType Type { get; set; }
    }
}
