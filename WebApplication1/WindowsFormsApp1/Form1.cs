using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(
              "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                label1.Text = responseFromServer;
                Console.WriteLine(responseFromServer);
            }

            // Close the response.
            response.Close();
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
            {
                { "to", textBox1.Text },
                { "subject", textBox2.Text },
                { "msg_text", richTextBox1.Text }
            };

            var test = JsonConvert.SerializeObject(values);
            var content = new StringContent(test, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://localhost:44340/api/test/send_spam", content);

            // responseString = await response.Content.ReadAsStringAsync();
        }
        private static readonly HttpClient client = new HttpClient();
    }
}
