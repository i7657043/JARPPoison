using SharpPcap;
using System.Net;
using System.Net.NetworkInformation;

namespace JArpPoison.Classes
{
    public class JCaptureDevice
    {
        public PhysicalAddress OurMacAddress { get; set; }
        public PhysicalAddress TargtMacAddress { get; set; }
        public IPAddress OurIpAddress { get; set; }
        public IPAddress GatewayIpAddess { get; set; }
        public IPAddress SubnetMask { get; set; }
        public ICaptureDevice CaptureDevice { get; set; }
    }
}
