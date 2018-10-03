using JARP;
using JARP.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JArpPoison.Classes
{
    public class ScanResult
    {
        public long Duration { get; set; }
        public List<Target> OnlineClients { get; set; } 
    }
}
