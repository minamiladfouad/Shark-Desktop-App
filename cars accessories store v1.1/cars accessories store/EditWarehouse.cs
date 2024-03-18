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
    public partial class EditWarehouse : Form
    {
        public EditWarehouse()
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
            comboBox1.SelectedIndex = -1;

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
            textBox9.Enabled = false;
            textBox8.Enabled = false;
            textBox7.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
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
            if (string.IsNullOrWhiteSpace(textBox7.Text))
                MessageBox.Show("برجاء ادخال الكمية");
            else
            {
                con.AddItemStock(id, int.Parse(textBox7.Text));
                MessageBox.Show("تمت اضافة الكمية بنجاح");
                textBox9.Text = (int.Parse(textBox9.Text) + int.Parse(textBox7.Text)).ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
                MessageBox.Show("برجاء ادخال الكمية");
            else
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
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox12.Enabled = true;
            textBox11.Enabled = true;
            textBox10.Enabled = true;
            textBox9.Enabled = true;
            textBox8.Enabled = true;
            textBox7.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.updateItem(id, textBox1.Text, textBox12.Text, int.Parse(textBox11.Text), int.Parse(textBox10.Text), textBox8.Text, int.Parse(textBox9.Text));
            button3.PerformClick();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.DeleteItem(id);
            this.reload();
            comboBox1.SelectedIndex = -1;
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
            if(comboBox1.SelectedIndex > -1)
                textBox13.Text = comboBox1.SelectedValue.ToString();            
        }

        private void اصدارفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBill f = new NewBill();
            f.Show();
            this.Hide();
        }

        private void تقاريرالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillReport f = new BillReport();
            f.Show();
            this.Hide();
        }

        private void تقريرالبضائعالحاليةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReport f = new WarehouseReport();
            f.Show();
            this.Hide();
        }

       

        private void تسجيلبضاعةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegItems f = new RegItems();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                MessageBox.Show("برجاء اختيار الصنف");
            else
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
                textBox9.Enabled = false;
                textBox8.Enabled = false;
                textBox7.Enabled = false;
                textBox1.Enabled = false;

                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = true;
            }
        }

        private void معاملاتالآجلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ledger f = new Ledger();
            f.Show();
            this.Hide();
        }

        private void بحثتعديلحذفToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegLedgerAcc f = new RegLedgerAcc();
            f.Show();
            this.Hide();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && comboBox1.Text != "")
                button3.PerformClick();
        }

        private void textBox11_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox10_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox9_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox12.Focus();
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox11.Focus();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox10.Focus();
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox9.Focus();
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox8.Focus();
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox1.Focus();
        }
    }
}
