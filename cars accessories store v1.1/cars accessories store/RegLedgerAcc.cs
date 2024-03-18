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
    public partial class RegLedgerAcc : Form
    {
        public RegLedgerAcc()
        {
            InitializeComponent();
        }

        private void RegLedgerAcc_Load(object sender, EventArgs e)
        {

        }

        private void تقريرالبضائعالحاليةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReport f = new WarehouseReport();
            f.Show();
            this.Hide();
        }

        private void تقاريرالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillReport f = new BillReport();
            f.Show();
            this.Hide();
        }

        private void اصدارفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBill f = new NewBill();
            f.Show();
            this.Hide();
        }

        private void بحثتعديلحذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditWarehouse f = new EditWarehouse();
            f.Show();
            this.Hide();
        }

        private void تسجيلبضاعةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegItems f = new RegItems();
            f.Show();
            this.Hide();
        }

        private void معاملاتالآجلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ledger f = new Ledger();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                MessageBox.Show("برجاء ادخال البيانات الأساسية لعميل جديد: اسم العميل ورقم تليفونه");
            else
            {
                int iBalance = 0;
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                    iBalance = int.Parse(textBox7.Text);
                connection con = new connection();
                con.insertLedgerAcc(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, iBalance);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox2.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox3.Focus();
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox4.Focus();
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox5.Focus();
        }
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox6.Focus();
        }
        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox7.Focus();
        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                textBox1.Focus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
