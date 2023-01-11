using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulers2._0.api.Models
{
    public class HangfireModel
    {
        // HangfireModel _HangfireModel= new HangfireModel();
        public void WelcomeMail(string Username)
        {
                  Console.WriteLine($"WelcomeMail:{Username}");
        }

        public void ScheduledWelcomeMail(string Username)
        {
                  Console.WriteLine($"ScheduledWelcomeMail:{Username}");
        }

         public void ScheduleFT(string Username)
        {
                  Console.WriteLine($"ScheduleFT:{Username}");
        }

    }
}