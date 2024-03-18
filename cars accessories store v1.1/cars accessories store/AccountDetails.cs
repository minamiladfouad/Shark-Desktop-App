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
    public partial class AccountDetails : Form
    {
        public int cID;
        public AccountDetails()
        {
            InitializeComponent();
        }

        connection con = new connection();

        private void AccountDetails_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = con.getClientInstallments(cID);
            textBox2.Text = "برجاء كتابة الملحوظات هنا...";
        }


        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "برجاء كتابة الملحوظات هنا...")
                textBox2.Text = "";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                textBox2.Text = "برجاء كتابة الملحوظات هنا...";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //سداد قسط
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("برجاء ادخال قيمة الدفع");
            else
            {
                int value = int.Parse(textBox1.Text);
                string note;
                if (textBox2.Text == "برجاء كتابة الملحوظات هنا...")
                    note = "";
                else note = textBox2.Text;

                con.insertInstallment(cID, dateTimePicker1.Value.Date, value, note);
                int balance = int.Parse(label7.Text) + value;
                con.updateBalance(cID, balance);
                label7.Text = balance.ToString();
                textBox1.Text = "";
                textBox2.Text = "برجاء كتابة الملحوظات هنا...";
                dataGridView1.DataSource = con.getClientInstallments(cID);
            }
        }

        //دين
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("برجاء ادخال قيمة الدفع");
            else
            {
                int value = 0 - int.Parse(textBox1.Text);
                string note;
                if (textBox2.Text == "برجاء كتابة الملحوظات هنا...")
                    note = "";
                else note = textBox2.Text;

                
                con.insertInstallment(cID, dateTimePicker1.Value.Date, value, note);
                int balance = int.Parse(label7.Text) + value;
                con.updateBalance(cID, balance);
                label7.Text = balance.ToString();
                textBox1.Text = "";
                textBox2.Text = "برجاء كتابة الملحوظات هنا...";
                dataGridView1.DataSource = con.getClientInstallments(cID);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ledger f = new Ledger();
            f.Show();
            this.Hide();
        }
    }
}
