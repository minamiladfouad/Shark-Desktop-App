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
    public partial class BillReport : Form
    {
        public BillReport()
        {
            InitializeComponent();
        }

        connection con = new connection();
        int totalPaid;
        int totalProfit;
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        

        private void Form5_Load(object sender, EventArgs e)
        {

            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            radioButton1.BackColor = System.Drawing.Color.Transparent;
            radioButton2.BackColor = System.Drawing.Color.Transparent;

            this.dataGridView1.DataSource = con.showBills();
            this.dataGridView1.Columns[7].DefaultCellStyle.ForeColor = Color.DarkGreen;


            totalPaid = 0;
            totalProfit = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                totalPaid += int.Parse(r.Cells[7].Value.ToString());
                totalProfit += int.Parse(r.Cells[8].Value.ToString());
            }

            textBox2.Text = totalProfit.ToString();
            textBox5.Text = totalPaid.ToString();

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

        private void بحثتعديلحذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditWarehouse f = new EditWarehouse();
            f.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView1.DataSource = con.showBills();
                totalPaid = 0;
                totalProfit = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    totalPaid += (int)r.Cells[7].Value;
                    totalProfit += (int)r.Cells[8].Value;
                }

                textBox2.Text = totalProfit.ToString();
                textBox5.Text = totalPaid.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                dataGridView1.DataSource = con.showBillsByDate(dateTimePicker1.Value.Date,dateTimePicker2.Value.Date);
                totalPaid = 0;
                totalProfit = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    totalPaid += (int)r.Cells[7].Value;
                    totalProfit += (int)r.Cells[8].Value;
                }

                textBox2.Text = totalProfit.ToString();
                textBox5.Text = totalPaid.ToString();
            }
            if (radioButton3.Checked)
            {
                dataGridView1.DataSource = con.showBillsByID(int.Parse(textBox1.Text));
                totalPaid = 0;
                totalProfit = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    totalPaid += (int)r.Cells[7].Value;
                    totalProfit += (int)r.Cells[8].Value;
                }

                textBox2.Text = totalProfit.ToString();
                textBox5.Text = totalPaid.ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox1.ForeColor = Color.Black;
                textBox1.ReadOnly = false;
                textBox1.Focus();
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BillItems f = new BillItems();

            f.dataGridView1.DataSource = con.showBillItems((int)this.dataGridView1.CurrentRow.Cells[0].Value);
            f.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                button1.PerformClick();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل بالفعل تريد حذف الفواتير التى تم تحديدها؟", "رسالة تأكيد", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

                if (selectedRowCount <= 0)
                    MessageBox.Show("لم يتم تحديد الفواتير المراد حذفها");
                else
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        int id = (int)dataGridView1.SelectedRows[i].Cells[0].Value;

                        DataTable x = con.getItem_quantity_Sold(id);

                        foreach (DataRow row in x.Rows)
                        {
                            con.AddItemStock(int.Parse(row["itemID"].ToString()), int.Parse(row["quantity"].ToString()));

                            con.DeleteBillItem(id);
                        }
                        con.DeleteBill(id);

                        MessageBox.Show("تم حذف الفاتورة رقم ’" + id + "’ بنجاح");
                    }

                    dataGridView1.DataSource = con.showBills();
                    totalPaid = 0;
                    totalProfit = 0;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        totalPaid += (int)r.Cells[7].Value;
                        totalProfit += (int)r.Cells[8].Value;
                    }

                    textBox2.Text = totalProfit.ToString();
                    textBox5.Text = totalPaid.ToString();
                }

                //foreach (DataGridViewRow r in dataGridView1.Rows)
                //{
                //    if (dataGridView1.SelectedRows.Count == 1)
                //    {

                //int id = (int)r.Cells[0].Value;

                //DataTable x = con.getItem_quantity_Sold(id);

                //foreach (DataRow row in x.Rows)
                //{
                //    con.AddItemStock(int.Parse(row["itemID"].ToString()), int.Parse(row["quantity"].ToString()));

                //    con.DeleteBillItem(id);
                //}
                //con.DeleteBill(id);

                //MessageBox.Show("تم حذف الفاتورة رقم ’" + id + "’ بنجاح");
                //    }
                //}

                
            }
            
        }

        private void معاملاتالآجلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ledger f = new Ledger();
            f.Show();
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegLedgerAcc f = new RegLedgerAcc();
            f.Show();
            this.Hide();
        }
    }
}
