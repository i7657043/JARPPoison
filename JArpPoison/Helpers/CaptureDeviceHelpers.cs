using JARP;
using JARP.Classes;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JArpPoison.Helpers
{
    public static class CaptureDeviceHelpers
    {
        public static CaptureDeviceList FindCaptureDevices()
        {
            CaptureDeviceList devices = CaptureDeviceList.Instance;

            /*If no device exists, print error */
            if (devices.Count < 1)
            {
                Console.WriteLine("No device found on this machine");
                Console.ReadKey();
                return null;
            }

            return devices;
        }

        public static void OpenDevice(this ICaptureDevice dev)
        {
            int readTimeoutMilliseconds = 1000;
            dev.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            string filter = "arp";
            dev.Filter = filter;
            dev.StartCapture();
        }
        

        public static bool CheckIfArpReplyIsForUs(ARPPacket arpPacket, List<string> ipsInOurSubnet, List<Target> macsResponding)
        {
            if (ipsInOurSubnet != null && ipsInOurSubnet.Count > 0 && arpPacket != null)
            {
                if (ipsInOurSubnet.Any(x => arpPacket.SenderProtocolAddress.ToString().Equals(x))
                    && !macsResponding.Any(x => x.Ip.Equals(arpPacket.SenderProtocolAddress.ToString())))
                {
                    return true;                    
                }
            }

            return false;
        }
    }
}
