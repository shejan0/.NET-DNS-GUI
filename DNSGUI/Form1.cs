using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace DNSGUI
{
    public partial class Form1 : Form
    {
        ListBox.ObjectCollection output;
        IPAddress selectedIP;
        bool currentlypinging = false;
        Ping pingObject = new Ping();
        PingReply reply;
        WebClient client = new WebClient();
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            output = IPAddressOutput.Items;
            DNSSearch();
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1;
           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pingDestination();
        }

        private void AddressInput_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            {
                output.Clear();
                DNSSearch();
            }
        }
        void DNSSearch()
        {
            try
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses(AddressInput.Text);
                foreach (IPAddress i in ipAddresses)
                {
                    output.Add(i);
                }
            }catch(Exception e)
            {
                output.Add(e.Message);
            }
        }
        void pingDestination()
        {
            try
            {
                if (!currentlypinging)
                {
                    currentlypinging = true;
                    reply = pingObject.Send(selectedIP);
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            PingLabel.Text = reply.RoundtripTime + "ms to " + selectedIP;
                            break;
                        case IPStatus.TimedOut:
                            PingLabel.Text = reply.Status.ToString();
                            timer.Enabled = false;
                            break;
                        case IPStatus.DestinationNetworkUnreachable:
                            PingLabel.Text = reply.Status.ToString();
                            timer.Enabled = false;
                            break;
                    }
                    if (reply.Status == IPStatus.Success)
                    {
                        
                    }
                    else
                    {
                        PingLabel.Text = reply.Status.ToString();
                    }
                    currentlypinging = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
            
        }
        void updateInfoforIP()
        {
            try
            {
                
                String response = client.DownloadString("http://ip-api.com/json/" + selectedIP.ToString());
                IP_API_JSON jsonResponse=JsonConvert.DeserializeObject<IP_API_JSON>(response);
                InformationLabel.Text = jsonResponse.ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        private void IPAddressOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (IPAddressOutput.SelectedItem is System.Net.IPAddress)
            {
                selectedIP = (IPAddress)IPAddressOutput.SelectedItem;
                updateInfoforIP();
                timer.Enabled = true;

            }else if (IPAddressOutput.SelectedItem == null)
            {
                Console.WriteLine("A Non-existent object was selected");
                timer.Enabled = false;
                PingLabel.Text = "";
            }
            else
            {
                Console.WriteLine("Selected object is not IPAddress, was type: "+IPAddressOutput.SelectedItem.GetType());
                timer.Enabled = false;
                PingLabel.Text = "";
            }
        }

    }
    public class IP_API_JSON
    {
        //https://ip-api.com/docs/api:json
        [JsonProperty("query")]
        public string query { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("countrycode")]
        public string countryCode { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }
        [JsonProperty("regionName")]
        public string regionName { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("zip")]
        public string zip { get; set; }

        [JsonProperty("lat")]
        public string lat { get; set; }
        [JsonProperty("lon")]
        public string lon { get; set; }
        [JsonProperty("timezone")]
        public string timeZone { get; set; }
        [JsonProperty("isp")]
        public string ISP { get; set; }
        [JsonProperty("org")]
        public string Org { get; set; }
        [JsonProperty("as")]
        public string AS { get; set; }

        public override String ToString()
        {
            return "Country: " + country +" ("+countryCode+"), Region: " + regionName + ", City: " + city + ", ZipCode: " + zip + ", Lat,Lon: (" + lat + "," + lon + "), ISP: " + ISP;
        }
    }
  
}
