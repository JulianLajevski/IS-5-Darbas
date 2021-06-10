using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_practical_task
{
    public partial class Form1 : Form
    {
          private RSACSPSample rsa = new RSACSPSample();
        private Sender sndr = new  Sender();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sndr.Send(textBox1.Text, rsa.HashAndSignBytes(textBox1.Text), rsa.test);

        }
    }
}
