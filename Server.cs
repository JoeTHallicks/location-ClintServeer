using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LocationServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> details = new List<string>();                                             //list fills inputs on form
            if (Port_No_textBox2.Text != "")
            {
                details.Add("-p");
                details.Add(Port_No_textBox2.Text);
            }
            if (timeout_textBox2.Text != "")
            {
                details.Add("-t");
                details.Add(timeout_textBox2.Text);
            }
            if (ProtocolPath_textBox1.Text != "")
            {
                details.Add("-l");
                details.Add(ProtocolPath_textBox1.Text);
            }
            if (debug_checkBox1.Checked)
            {
                details.Add("-d");
            }
            Thread thr = new Thread(() => LocationServer.locationserver.runServer(details));                                   //new thread for the form to run on.
            thr.Start();
            StartServer_button1.Hide(); StartServer_button1.Show();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            ProtocolPath_textBox1.Text = "slog.txt";
        }

        private void Savedetails_button1_Click(object sender, EventArgs e)
        {
            LocationServer.locationserver.Save();
        }
    }
}
