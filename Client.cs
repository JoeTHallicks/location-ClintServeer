using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Location
{
    public partial class Client : Form
    {

        public Client()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProtocolSelection_comboBox1.Text = "Protocol Selection";
            Message_listBox1.Items.Add("Default HTTP Protocol: WHOIS");
            Message_listBox1.Items.Add("Default Port: 43");
            Message_listBox1.Items.Add("Default Host: whois.net.dcs.hull.ac.uk");
            Message_listBox1.Items.Add("debug, details in message window");
            Message_listBox1.Items.Add("Default Timeout: 3000");
        }

        private void SendRequest_button1(object sender, EventArgs e)
        {
            List<string> details = new List<string>();
            details.Add(Username_textBox1.Text);
            if (Location_textBox2.Text != "")
            {
                details.Add(Location_textBox2.Text);
            }
            if (Host_textBox3.Text != "")
            {
                details.Add("-h");
                details.Add(Host_textBox3.Text);
            }
            if (Port_textBox4.Text != "")
            {
                details.Add("-p");
                details.Add(Port_textBox4.Text);
            }
            if (Timeout_textBox5.Text != "")
            {
                details.Add("-t"); details.Add(Timeout_textBox5.Text);
            }
            if (Debug_checkBox1.Checked)
            {
                details.Add("-d");
            }
            if (ProtocolSelection_comboBox1.SelectedText != "")
            {
                if (ProtocolSelection_comboBox1.SelectedItem.ToString() == "HTTP 0.9")
                {
                    details.Add("-h9");
                }
                else if (ProtocolSelection_comboBox1.SelectedItem.ToString() == "HTTP 1.0")
                {
                    details.Add("-h0");
                }
                else if (ProtocolSelection_comboBox1.SelectedItem.ToString() == "HTTP 1.1")
                {
                    details.Add("-h1");
                }
            }
            string[] args = new string[details.Count];
            for (int i = 0; i < details.Count; i++)
            {
                args[i] = details[i];
            }

            location loc = new location();
            location.runClient(args);
            Message_listBox1.Items.Add(location.output);
            loc = null;
            details = null;
        }
    }
}
