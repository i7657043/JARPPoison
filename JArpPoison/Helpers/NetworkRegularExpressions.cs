using JARP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JARP
{
    public static class NetworkRegularExpressions
    {
        public static Regex GatewayIp = new Regex(@"GatewayAddress:\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        public static Regex OurIp = new Regex(@"Addr:\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        public static Regex Mac = new Regex("(HWaddr:)([0-9a-fA-F]){12}");
        public static Regex OnlyMac = new Regex("([0-9a-fA-F]){12}");
        public static Regex Netmask = new Regex(@"Netmask:\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");


        public static string GetMatchString(string devDesc)
        {
            Match macResult = Mac.Match(devDesc);
            Match macOnly = OnlyMac.Match(macResult.ToString());

            return macOnly.ToString().AddColonsToMac();
        }

        public static string GetIpAddressString(string devDesc)
        {
            return OurIp.Match(devDesc).ToString();
        }

    }
}
