using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Socket_Client {
    public partial class Form1 : Form {
        System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (client.Connected) client.Close();
            client = new System.Net.Sockets.TcpClient();
            client.Connect(textBox1.Text.Split(':')[0], int.Parse(textBox1.Text.Split(':')[1]));
            if (client.Connected) richTextBox1.Text = "Connected!";
            else richTextBox1.Text = "Failed!";
        }

        private void button2_Click(object sender, EventArgs e) {
            try {
                NetworkStream serverStream = client.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "\n");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                byte[] inStream = new byte[client.ReceiveBufferSize];
                serverStream.Read(inStream, 0, (int)client.ReceiveBufferSize);
                string data = System.Text.Encoding.ASCII.GetString(inStream);
                richTextBox1.Text = data;
            } catch (Exception ex) {
                richTextBox1.Text = ex.ToString();
            }
        }
    }
}
