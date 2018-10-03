using LukeSkywalker.IPNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JARP.Helpers
{
    public static class IPHelpers
    {
        public static List<string> GetPrivateIps()
        {
            return new List<string>
            {
                "192.168",
                "10.0",
                "172."
            };
        }

        public static IPAddress CreateIp(string[] ipArray)
        {
            return new IPAddress((long)(uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(ipArray[3] + "." + ipArray[2] + "." + ipArray[1] + "." + ipArray[0]).Address));
        }

        public static bool IpIsInOurSubnet(string IpAddress)
        {
            List<String> privateIps = GetPrivateIps();
            return !string.IsNullOrEmpty(IpAddress.ToString()) && privateIps.Any(x => IpAddress.ToString().Contains(x));
        }

        public static IPAddress GetIpAddress(string devDesc)
        {
            string[] ipArray = NetworkRegularExpressions.OurIp.Match(devDesc).ToString().Replace("Addr:", "").Split('.');
            return CreateIp(ipArray);
        }

        public static IPAddress GetGatewayIp(string devDesc)
        {
            string[] ipArray = NetworkRegularExpressions.GatewayIp.Match(devDesc).ToString().Replace("GatewayAddress:", "").Split('.');
            return CreateIp(ipArray);
        }

        public static IPAddress GetNetMask(string devDesc)
        {
            string[] ipArray = NetworkRegularExpressions.Netmask.Match(devDesc).ToString().Replace("Netmask:", "").Split('.');
            return CreateIp(ipArray);
        }

        public static List<string> GetIpRange(this IPNetworkCollection ips)
        {
            List<string> cleanIps = new List<string>();
            for (int i = 1; i < ips.Count - 1; i++)
            {
                cleanIps.Add(ips[i].ToString().Substring(0, ips[i].ToString().IndexOf("/")));
            }

            return cleanIps;
        }
    }
}
