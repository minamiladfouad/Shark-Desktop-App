using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cars_accessories_store
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        connection con = new connection();
        int id;

        

        private void Form3_Load(object sender, EventArgs e)
        {

            Dictionary<int, string> items = new Dictionary<int, string>();
            items = con.getItemsName_ID();
            comboBox1.DataSource = new BindingSource(items, null);
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "key";

            textBox13.Text = comboBox1.SelectedValue.ToString();
            //id = int.Parse(textBox13.Text);
            //List<string> item = new List<string>();
            //item = con.getItemInfo(id);
            //textBox12.Text = item[0];
            //textBox11.Text = item[1];
            //textBox10.Text = item[2];
            //textBox8.Text = item[3];
            //textBox9.Text = item[4];
        }

        private void reload()
        {
            textBox1.Clear();
            textBox12.Clear();
            textBox11.Clear();
            textBox10.Clear();
            textBox8.Clear();
            textBox9.Clear();

            textBox1.Enabled = false;
            textBox12.Enabled = false;
            textBox11.Enabled = false;
            textBox10.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            Dictionary<int, string> items = new Dictionary<int, string>();
            items = con.getItemsName_ID();
            comboBox1.DataSource = new BindingSource(items, null);
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "key";

            textBox13.Text = comboBox1.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           con.AddItemStock(id, int.Parse(textBox7.Text));

           MessageBox.Show("تمت اضافة الكمية بنجاح");

           textBox9.Text = (int.Parse(textBox9.Text) + int.Parse(textBox7.Text)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int stock = con.getItemStock(id);
            int quantity = int.Parse(textBox7.Text);
            if (quantity > stock)
            {
                MessageBox.Show("هذه الكمية غير متاحة بالمخزن .... اقصى كميه متاحة :  " + stock);
            }
            else
            {
                if (quantity == stock)
                {
                    MessageBox.Show("ملحوظة : هذه اخر كمية موجوده بالمخزن  ");
                }
                con.subtractItemStock(id, int.Parse(textBox7.Text));

                MessageBox.Show("تم حذف الكمية بنجاح");
                textBox9.Text = (int.Parse(textBox9.Text) - int.Parse(textBox7.Text)).ToString();
            }

            
           
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox12.Enabled = true;
            textBox11.Enabled = true;
            textBox10.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.updateItem(id, textBox1.Text, textBox12.Text, int.Parse(textBox11.Text), int.Parse(textBox10.Text), textBox8.Text, int.Parse(textBox9.Text));
            this.reload();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.DeleteItem(id);

            this.reload();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        

        
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            textBox13.Text = comboBox1.SelectedValue.ToString();
            
        }

        private void اصدارفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }

        private void تقاريرالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
            this.Hide();
        }

        private void تقريرالبضائعالحاليةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
            this.Hide();
        }

       

        private void تسجيلبضاعةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id = int.Parse(textBox13.Text);

            List<string> item = new List<string>();
            item = con.getItemInfo(id);
            textBox12.Text = item[0];
            textBox11.Text = item[1];
            textBox10.Text = item[2];
            textBox8.Text = item[3];
            textBox9.Text = item[4];
            textBox1.Text = item[5];
            
            textBox12.Enabled = false;
            textBox11.Enabled = false;
            textBox10.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox1.Enabled = false;
            
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;
        }

       

       
    }
}
