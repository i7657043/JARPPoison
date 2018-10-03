using JARP.Classes;
using LukeSkywalker.IPNetwork;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JARP.Helpers
{
    public static class TextBoxWriterExtensionss
    {
        public static void PrintArpScanDetails(this RichTextBox richTxtBox, long duration, List<Target> macsResponding, IPAddress gatewayIpAddress, IPAddress ourIpAddress)
        {
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "\nTook " + (duration / 1000 < 1 ? " < 1 second " : duration / 1000 + " seconds ") + "to scan all addresses in our subnet\n"; });
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "Waiting 2 seconds to recieve ARP replys\n"; });
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.SelectionStart = richTxtBox.Text.Length; });
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.ScrollToCaret(); });
            Thread.Sleep(2000);
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "\n" + macsResponding.Count + " Devices online\n"; });

            macsResponding = RemoveDuplicates(macsResponding);

            ListRespondents(richTxtBox, macsResponding, gatewayIpAddress, ourIpAddress);

            richTxtBox.Invoke((MethodInvoker)delegate
            {
                richTxtBox.Text += "\n1) Paste the target IP and MAC addressses into the respective text boxes" +
                "\n2) Press Send JARP to modify the Target's and Gateway's ARP Caches\n";
            });

            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.SelectionStart = richTxtBox.Text.Length; });
            richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.ScrollToCaret(); });
        }

        private static List<Target> RemoveDuplicates(List<Target> macsResponding)
        {
            macsResponding = macsResponding.OrderBy(x => x.Ip.Split('.')[3]).ToList();
            List<int> lastOctets = macsResponding.Select(x => int.Parse(x.Ip.Split('.')[3])).ToList();
            lastOctets.OrderBy(x => x);
            lastOctets.Sort();
            List<Target> dupe = new List<Target>();
            foreach (int i in lastOctets)
            {
                dupe.Add(macsResponding.FirstOrDefault(x => x.Ip.Contains(i.ToString())));
            }
            if (dupe.Count > 0)
            {
                macsResponding = dupe;
            }

            return macsResponding;
        }

        private static void ListRespondents(RichTextBox richTxtBox, List<Target> macsResponding, IPAddress gatewayIpAddress, IPAddress ourIpAddress)
        {
            foreach (Target s in macsResponding)
            {
                if (s.Ip.Equals(gatewayIpAddress.ToString()))
                {
                    richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "Ip: " + s.Ip + " - MAC: " + s.Mac + " -- Router\n"; });
                }
                else if (s.Ip.Equals(ourIpAddress.ToString()))
                {
                    richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "Ip: " + s.Ip + " - MAC: " + s.Mac + " -- That's us\n"; });
                }
                else
                {
                    richTxtBox.Invoke((MethodInvoker)delegate { richTxtBox.Text += "Ip: " + s.Ip + " - MAC: " + s.Mac + "\n"; });
                }
            }
        }

        public static void PreFillTextBoxes(TextBox macTxtBox, TextBox ipTxtBox, string ourMac, string ourIp)
        {
            macTxtBox.Text = ourMac;
            ipTxtBox.Text = ourIp;
        }

        public static void PrintWelcomeMsg(this RichTextBox richTxtBox)
        {
            richTxtBox.Text += "Welcome to the ARP poisoning program\n\nModify the Target's and Router's ARP Cache, to replace the correct MAC - IP mapping, with our own, directing traffic through us\n\n";
            richTxtBox.Text += "Selected the following Network Interface Card to work with:\n";
        }

        public static void PrintNetworkInfo(this RichTextBox richTxtBox, ICaptureDevice dev, List<string> netInfo)
        {
            richTxtBox.Text += dev.Description + "\n\nNIC Details as of "+DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()+ ":\n";
            richTxtBox.Text += "Our IP: " + netInfo[0].ToString() + "\n"
                + "Gateway IP: " + netInfo[1].ToString() + "\n"
                + "Subnet Mask: " + netInfo[2].ToString() + "\n"
                + "Our MAC: " + netInfo[3] + "\n";
        }

        public static void printIpRangeInfo(this RichTextBox richTxtBox, IPNetworkCollection ips, string ipWithSubnet)
        {
            richTxtBox.Text += "\nScan coming from " + ipWithSubnet + "\n";
            richTxtBox.Text += ips.Count - 2 + " IP addresses available\n";
            richTxtBox.Text += "First: " + ips[1].ToString().Substring(0, ips[1].ToString().IndexOf("/")) + "\n";
            richTxtBox.Text += "Last: " + ips[ips.Count - 2].ToString().Substring(0, ips[ips.Count - 2].ToString().IndexOf("/")) + "\n";
            richTxtBox.Text += "\nScanning all addresses available...\n";

        }

        public static void printArpingProgress(this RichTextBox richTextBox, IPAddress targetIp, IPAddress gatewayIp, PhysicalAddress ourMac, int count)
        {
            richTextBox.Invoke((MethodInvoker)delegate {richTextBox.Text += "\n\nTell " + targetIp.ToString() + " that " + gatewayIp.ToString() + " is at " + ourMac.ToString() + "(Our MAC)"; });
            richTextBox.Invoke((MethodInvoker)delegate {richTextBox.Text += "\nAnd\nTell " + gatewayIp.ToString() + " that " + targetIp.ToString() + " is at " + ourMac.ToString() + "(Our MAC)"; });
            richTextBox.Invoke((MethodInvoker)delegate {richTextBox.Text += "\n\n" + count + " JARP Packets sent successfully"; });
            richTextBox.Invoke((MethodInvoker)delegate {richTextBox.SelectionStart = richTextBox.Text.Length; });
            richTextBox.Invoke((MethodInvoker)delegate { richTextBox.ScrollToCaret(); });
        }
    }
}
