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
using System.IO;
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
        KEY_JSON keys; 
        public Form1(String[] args)
        {
            InitializeComponent();
            output = IPAddressOutput.Items;
            DNSSearch();
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1;
            String lines="";
            if (args.Length > 1)
            {
                lines = File.ReadAllText(args[1]);
               
            }
            else
            {
                try
                {
                    lines = File.ReadAllText("keys.json");
                }catch(Exception e)
                {
                    output.Add(e.Message);

                }
                

            }
            keys = JsonConvert.DeserializeObject<KEY_JSON>(lines);
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
                String link = "http://ip-api.com/json/" + selectedIP.ToString();
                String response = client.DownloadString(link);
                Console.WriteLine(response);
                IP_API_JSON jsonResponse=JsonConvert.DeserializeObject<IP_API_JSON>(response);
                InformationLabel.Text = jsonResponse.ToString();
                updateImage(jsonResponse);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        void updateImage(IP_API_JSON json)
        {
            System.Drawing.Image imagetest = tryImageSource(json.lon, json.lat, resultImage.Width, resultImage.Height);
            if (imagetest == null) {
                imagetest = new System.Drawing.Bitmap(resultImage.Width, resultImage.Height);
                System.Drawing.Font font = new System.Drawing.Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
                System.Drawing.Graphics graphics = Graphics.FromImage(imagetest);
                graphics.DrawString("UNABLE TO GET IMAGE, MAYBE MISSING KEY", font, Brushes.Red, new Point(0, 0));
            }
            resultImage.Image = imagetest;
        }
        System.Drawing.Image tryImageSource(String lon,String lat,int width,int height)
        {
            //the API Key for the various maps is hosted by maps4html.org which is part of the W3C and is open to everyone.
            //https://maps4html.org/HTML-Map-Element-UseCases-Requirements/examples/static-map.html
            String url ="";
            for(int n = 0; n < 3; n++)
            {
                try
                {
                    switch (n)
                    {
                        case 0:
                            //MapBox API
                            if (keys.mapBoxApiKey != null)
                            {
                                url = "https://api.mapbox.com/styles/v1/mapbox/light-v9/static/pin-s+f00(" + lon + "," + lat + ")/" + lon + "," + lat + ",11,0,0/" + width + "x" + height + "?access_token="+keys.mapBoxApiKey;
                            }
                            break;

                        case 1:
                            //TomTom API
                            if (keys.tomTomApiKey != null)
                            {
                                url = "https://api.tomtom.com/map/1/staticimage?center=" + lon + "," + lat + "&zoom=11&width=" + width + "&height=" + height + "&layer=basic&style=main&format=png&view=Unified&key="+keys.tomTomApiKey;

                            }
                            break;
                        case 2:
                            //Bing Maps, Virtual Earth, doesn't seem to work, keep getting forbidden even though link is ok
                            if (keys.bingMapsApiKey != null)
                            {
                                url = "https://dev.virtualearth.net/REST/v1/Imagery/Map/Road/" + lat + "," + lon + "/16?mapSize=" + width + "," + height + "&pushpin=" + lat + "," + lon + ";0;&format=png&key="+keys.bingMapsApiKey;

                            }
                            break;
                    }
                    if (url!=null)
                    {
                        Console.WriteLine(url);
                        return Image.FromStream(client.OpenRead(url));
                    }
                    else
                    {
                        Console.WriteLine("NO URL was selected for image");
                        return null;
                    }
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
            }
            return null;


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
    public class KEY_JSON
    {
        [JsonProperty("mapBoxApiKey")]
        public string mapBoxApiKey { get; set; }
        [JsonProperty("tomTomApiKey")]
        public string tomTomApiKey { get; set; }
        [JsonProperty("bingMapsApiKey")]
        public string bingMapsApiKey { get; set; }

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
