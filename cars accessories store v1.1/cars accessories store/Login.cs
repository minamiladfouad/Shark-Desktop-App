using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace cars_accessories_store
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        connection con = new connection();

        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string macAddress = GetMacAddress();
            if (macAddress == "70F1A150AE3D")
            {
                label1.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                MessageBox.Show(macAddress);
                MessageBox.Show("هذا الجهاز غير مرخص لاستخدام البرنامج برجاء الاتصال بالمبيعات 01274583552 ..!!");
                Application.Exit();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = con.getPassword();
            if (textBox1.Text == pass)
            {
                WarehouseReport f = new WarehouseReport();
                f.Show();
                this.Hide();
            }
            else
             {
                    MessageBox.Show("هذا الرقم غير صحيح!");
                    textBox1.SelectAll();
                
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                button1.PerformClick();
        }
    }
}
