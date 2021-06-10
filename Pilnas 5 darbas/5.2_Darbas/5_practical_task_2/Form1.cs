using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_practical_task_2
{
    public partial class Form1 : Form
    {
       private TcpListener listener = new TcpListener(IPAddress.Any, 2023);
        public string text;
        public string key;
        public string signature;
        public byte[] signedData;
        int counter;
        public List<string> gautText = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { Thread receiverThread = new Thread(() =>
        {
            start();

        });
            receiverThread.Start();
        }
        public void start()
        {

            try
            {
                while (true)
                {
                    listener.Start();
                    using var client = listener.AcceptTcpClient();
                    using var stream = client.GetStream();
                    {

                        {
                            using MemoryStream ms = new MemoryStream();

                            var buffer = new byte[1024];
                            int bufferSize;
                            while ((bufferSize = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bufferSize);

                            }
                            if (counter == 1)
                            {
                                signedData = ms.ToArray();
                            }
                            else
                            {
                                gautText.Add(Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length));
                            }
                            counter++;
                            }
                        }
                    }
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (SignatureVerify.VerifySignedHash(gautText[0], signedData, gautText[1]))
            {
                label1.Text = "Text successfully verified!";
                gautText.Clear();
                counter = 0;
            }
            else
                label1.Text = "It's did not match";
            {
                gautText.Clear();
                counter = 0;
            }
        }
    }
}
