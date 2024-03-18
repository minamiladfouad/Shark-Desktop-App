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
    public partial class WarehouseReport : Form
    {
        public WarehouseReport()
        {
            InitializeComponent();
        }

        connection con = new connection();
        int totalPaid = 0;
        int totalWithProfit = 0;

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = con.showItems();
            dataGridView1.DataSource = bindingSource1;

            //dataGridView1.DataSource = con.showItems();

            dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Ascending);
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.DarkGreen;
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[6].Visible = false;

            
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
            ///*
            if (dataGridView1.CurrentRow != null)
            {
                
                string new_quantity = dataGridView1.CurrentCell.Value.ToString();

                //if (editedCell != 4 && !Regex.IsMatch(new_quantity, "^\\d+$"))
                //{
                //    dataGridView1.CurrentCell.Value = old_quantity;
                //}

                if (new_quantity == old_quantity) return;

                else
                {
                    //dataGridView1.CurrentRow.Cells[3].Value = new_quantit;
                    int itemID = (int)dataGridView1.CurrentRow.Cells[6].Value;

                    if (editedCell != 4 && !Regex.IsMatch(new_quantity, "^\\d+$")) dataGridView1.CurrentCell.Value = old_quantity;
                    else if (editedCell == 2)
                    {
                        con.updateCost(itemID, int.Parse(new_quantity));

                        int diff = int.Parse(new_quantity) - int.Parse(old_quantity);
                        totalPaid += (int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) * diff);
                        textBox5.Text = totalPaid.ToString();
                    }

                    else if (editedCell == 3)
                    {
                        con.updatePrice(itemID, int.Parse(new_quantity));

                        int diff = int.Parse(new_quantity) - int.Parse(old_quantity);
                        totalWithProfit += (int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) * diff);
                        textBox2.Text = totalWithProfit.ToString();
                    }

                    if (editedCell == 4)
                    {
                        con.updateNote(itemID, new_quantity);
                    }

                    else if (editedCell == 5)
                    {
                        con.updateStock(itemID, int.Parse(new_quantity));
                        
                        int diff = int.Parse(new_quantity) - int.Parse(old_quantity);
                        totalPaid += (int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()) * diff);
                        totalWithProfit += (int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString()) * diff);
                        textBox2.Text = totalWithProfit.ToString();
                        textBox5.Text = totalPaid.ToString();
                    }

                }
            }
            //*/
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
