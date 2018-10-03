using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARP.Helpers
{
    public static class MacHelper
    {
        public static string AddColonsToMac(this string mac)
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 1; k < mac.Length + 1; k++)
            {
                sb.Append(mac[k - 1]);
                if (k % 2 == 0 && k != mac.Length)
                {
                    sb.Append(":");
                }
            }
            return sb.ToString();
        }
    }
}
