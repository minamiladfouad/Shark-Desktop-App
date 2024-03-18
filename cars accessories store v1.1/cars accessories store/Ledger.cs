using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cars_accessories_store
{
    public partial class Ledger : Form
    {

        public Ledger()
        {
            InitializeComponent();
        }

        connection con = new connection();
        //int totalPaid = 0;

        private void تسجيلبضاعةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegItems f = new RegItems();
            f.Show();
            this.Hide();
        }

        private void المخازنبحثتعديلحذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditWarehouse f = new EditWarehouse();
            f.Show();
            this.Hide();
        }

        private void فاتورةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
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


        private void Form8_Load(object sender, EventArgs e)
        {
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = con.getClients();
            dataGridView1.DataSource = bindingSource1;

            //dataGridView1.Columns[0].Visible = false;
            
            this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
            dataGridView1.Columns[7].DefaultCellStyle.ForeColor = Color.DarkGreen;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AccountDetails f = new AccountDetails();

            f.cID = (int)this.dataGridView1.CurrentRow.Cells[0].Value;
            f.label1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f.label2.Text =this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f.label3.Text = "العنوان: " + this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f.label4.Text = "حساب بنكى رقم: " + this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            f.label5.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            f.label6.Text = "فرع: " + this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            f.label7.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();

            f.Show();
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegLedgerAcc f = new RegLedgerAcc();
            f.Show();
            this.Hide();
        }

        string old_quantity;
        int editedCell;
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                old_quantity = dataGridView1.CurrentCell.Value.ToString();
                editedCell = dataGridView1.CurrentCell.ColumnIndex;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string new_quantity = dataGridView1.CurrentCell.Value.ToString();

                if (new_quantity == old_quantity) return;
                
                else
                {
                    int itemID = (int)dataGridView1.CurrentRow.Cells[0].Value;

                    if (editedCell == 2 && !Regex.IsMatch(new_quantity, "^\\d+$")) dataGridView1.CurrentCell.Value = old_quantity;
                    else
                    {
                        con.updateClientData(itemID, new_quantity, editedCell);
                    }

                }

            }
            
        }
    }
}
