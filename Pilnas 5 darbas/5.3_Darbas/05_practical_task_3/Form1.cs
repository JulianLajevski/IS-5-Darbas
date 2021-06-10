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

namespace _05_practical_task_3
{
    public partial class Form1 : Form
    {
        private TcpListener listener = new TcpListener(IPAddress.Any, 2024);
        private int counter;
        private byte[] signed;
        private byte[] sniped = { 3 };
        private List<string> dataList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            Thread receiverThread = new Thread(() =>
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
                                signed = ms.ToArray();
                                Invoke(new Action(() =>
                               {
                                   textBox1.Text = Convert.ToBase64String(signed);

                               }));
                            }
                            else
                            {
                                dataList.Add(Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length));
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
            Sender send = new Sender();
            try
            {
                send.Send(dataList[0], Convert.FromBase64String(textBox1.Text), dataList[1]);
                counter = 0;
                dataList.Clear();
            }
            catch
            {
                send.Send(dataList[0], sniped, dataList[1]);
                counter = 0;
                dataList.Clear();
            }
        }
    }
}
