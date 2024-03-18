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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        connection con = new connection();

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form6_Load(object sender, EventArgs e)
        {


            dataGridView1.DataSource = con.showItems();

            dataGridView1.Columns[0].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.Red;

            int totalPaid = 0;
            int totalWithProfit = 0;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                totalPaid += (int.Parse(r.Cells[5].Value.ToString()) * int.Parse(r.Cells[2].Value.ToString()));
                totalWithProfit += (int.Parse(r.Cells[5].Value.ToString()) * int.Parse(r.Cells[3].Value.ToString()));
            }
            textBox2.Text = totalWithProfit.ToString();
            textBox5.Text = totalPaid.ToString();
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

        private void بحثتعديلحذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
