using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaysGoneModManager.CustomClasses
{
    public class SteamAction
    {
        public SteamAction(string name, Action action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; set; }
        public Action Action { get; set; }
    }
}
