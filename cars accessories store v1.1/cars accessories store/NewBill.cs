using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace cars_accessories_store
{
    public partial class NewBill : Form
    {
        public NewBill()
        {
            InitializeComponent();
        }

        connection con = new connection();
        
        Dictionary<int, string> items = new Dictionary<int, string>();
        


        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //int billID = con.getMaxBillID() + 1;
            textBox10.Text = con.getIDENT_CURRENT().ToString();

            textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");

            items = con.getItemsName_ID();

            comboBox2.DataSource = new BindingSource(items, null);
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "key";
            comboBox2.SelectedIndex = -1;

            //textBox3.Text = con.getItemPrice(int.Parse(comboBox2.SelectedValue.ToString())).ToString();


            foreach (DataGridViewColumn x in dataGridView1.Columns)
            {
                dataGridView1.DefaultCellStyle.ForeColor = Color.Navy;
            }

            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.Red;

            textBox2.Focus();
            textBox2.Select();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                button4.PerformClick();
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.SelectedIndex <= -1 && !items.ContainsValue(comboBox2.Text)) return;

            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[0].Value != null && r.Cells[0].Value.ToString() == comboBox2.Text)
                {
                    MessageBox.Show("هذا الصنف موجود بالفعل .. يمكنك تغيير الكمية المطلوبة");
                    comboBox2.Text="";
                    comboBox2.SelectedIndex = -1;
                    comboBox2.Focus();
                    textBox4.Text = "1";
                    textBox3.Text = "0";
                    return;
                }
            }
            int id = items.FirstOrDefault(x => x.Value == comboBox2.Text).Key;

            int stock = con.getItemStock(id);
            int quantity=int.Parse(textBox4.Text);
            int price=int.Parse(textBox3.Text);
            int cost = con.getItemCost(id);


            if (quantity > stock)
            {
                MessageBox.Show("هذه الكمية غير متاحة بالمخزن .... اقصى كميه متاحة :  " + stock);
                textBox4.Focus();
            }
            else
            {
                
                if (quantity == stock)
                {
                    MessageBox.Show("ملحوظة : هذه اخر كمية موجوده بالمخزن  ");
                }

                int sub_price = quantity * price;




                object[] row = { comboBox2.Text, price, cost, quantity, sub_price };

                dataGridView1.Rows.Add(row);

                textBox5.Text=(int.Parse(textBox5.Text)+sub_price).ToString();
                textBox7.Text = (int.Parse(textBox5.Text) - int.Parse(textBox6.Text)).ToString();

                comboBox2.Text="";
                comboBox2.SelectedIndex = -1;
                comboBox2.Focus();
                textBox4.Text = "1";
                textBox3.Text = "0";

            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox7.Text = (int.Parse(textBox5.Text) - int.Parse(textBox6.Text)).ToString();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
                MessageBox.Show("لم يتم اضافة بضاعة للفاتورة");
            else
            {
                string type;
                if(radioButton1.Checked==true)
                    type = radioButton1.Text;
                else
                    type = radioButton2.Text;

                DateTime date = Convert.ToDateTime(textBox1.Text);

                int totalProfit = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    totalProfit += ((int)r.Cells[1].Value - (int)r.Cells[2].Value) * (int)r.Cells[3].Value;
                }
                
                totalProfit -= int.Parse(textBox6.Text);

                con.insertBill(date,textBox2.Text, textBox8.Text, type, int.Parse(textBox6.Text), int.Parse(textBox5.Text), int.Parse(textBox7.Text), totalProfit);



                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string itemName = dataGridView1.Rows[i].Cells[0].Value.ToString();

                    int id = items.FirstOrDefault(x => x.Value == itemName).Key;

                    int quantity = (int)dataGridView1.Rows[i].Cells[3].Value;

                    con.insertBillItems(int.Parse(textBox10.Text), id, quantity);

                    con.subtractItemStock(id,quantity);
                }

                textBox10.Text = con.getIDENT_CURRENT().ToString();
                textBox2.Clear();
                textBox8.Clear();
                radioButton1.Checked = true;
                textBox5.Text = "0";
                textBox6.Text = "0";
                textBox7.Text = "0";

                dataGridView1.Rows.Clear();

                comboBox2.Text="";
                comboBox2.SelectedIndex = -1;
                textBox4.Text = "1";
                textBox3.Text = "0";

            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
            {
                textBox3.Text = con.getItemPrice(int.Parse(comboBox2.SelectedValue.ToString())).ToString();
                textBox4.Focus();
            }

        }


        int old_quantit;
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                old_quantit = (int)dataGridView1.CurrentRow.Cells[3].Value;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string new_quantit = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                if (new_quantit == old_quantit.ToString()) return;

                if (!Regex.IsMatch(new_quantit, "^\\d+$"))
                {
                    dataGridView1.CurrentRow.Cells[3].Value = old_quantit;
                }
                else
                {
                    //dataGridView1.CurrentRow.Cells[3].Value = new_quantit;
                    
                    if (int.Parse(new_quantit) > old_quantit)
                    {
                        string itemName = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                        int id = items.FirstOrDefault(x => x.Value == itemName).Key;

                        int stock = con.getItemStock(id);
                        int quantity = int.Parse(new_quantit);
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
                        }
                    }
                    /////////////


                    int price = (int)dataGridView1.CurrentRow.Cells[1].Value;
                    dataGridView1.CurrentRow.Cells[3].Value = int.Parse(new_quantit);
                    dataGridView1.CurrentRow.Cells[4].Value = (price * int.Parse(new_quantit));

                    int new_total = 0;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        new_total += (int)r.Cells[4].Value;
                    }

                    textBox5.Text = new_total.ToString();
                    textBox7.Text = (int.Parse(textBox5.Text) - int.Parse(textBox6.Text)).ToString();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f=new Font("Times New Roman",16,FontStyle.Bold);
            Font f2 = new Font("Times New Roman", 22, FontStyle.Bold);
            Font f3 = new Font("Times New Roman", 20, FontStyle.Bold);
            Font f4 = new Font("Times New Roman", 24, FontStyle.Bold);


            string strNO = "#NO  000" + textBox10.Text;

            string date =  textBox1.Text;
            string name = textBox2.Text;

            SizeF fontSizeName = e.Graphics.MeasureString(name, f2);

            e.Graphics.DrawImage(Properties.Resources.fatora,0,0,e.PageBounds.Width,e.PageBounds.Height);
            e.Graphics.DrawString(strNO, f, Brushes.Black, 70, 140);

            e.Graphics.DrawString(date, f2, Brushes.Black, 500, 140);
            e.Graphics.DrawString(name, f2, Brushes.Black, e.PageBounds.Width - fontSizeName.Width-200, 180);


            //////////// items ////////////////////
            int startHeight = 300;
            int rowsHeight = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string itemName = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string price = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string quantity = dataGridView1.Rows[i].Cells[3].Value.ToString();
                string subPrice = dataGridView1.Rows[i].Cells[4].Value.ToString();


                SizeF Size1 = e.Graphics.MeasureString(itemName, f3);
                SizeF Size2 = e.Graphics.MeasureString(price, f3);
                SizeF Size3 = e.Graphics.MeasureString(quantity, f3);
                SizeF Size4 = e.Graphics.MeasureString(subPrice, f3);

                e.Graphics.DrawString(itemName, f3, Brushes.Black, e.PageBounds.Width - Size1.Width - 70, startHeight+rowsHeight);
                e.Graphics.DrawString(price, f3, Brushes.Black, e.PageBounds.Width - Size2.Width - 450, startHeight + rowsHeight);
                e.Graphics.DrawString(quantity, f3, Brushes.Black, e.PageBounds.Width - Size3.Width - 580, startHeight + rowsHeight);
                e.Graphics.DrawString(subPrice, f3, Brushes.Black, e.PageBounds.Width - Size4.Width - 690, startHeight + rowsHeight);


                rowsHeight += 35;
            }


            ////////////////////////////////

            string allPrice = textBox5.Text; ;
            string discound = textBox6.Text; ;
            string total_Price = textBox7.Text; ;

            SizeF allPriceSize = e.Graphics.MeasureString(allPrice, f2);
            SizeF discoundSize = e.Graphics.MeasureString(discound, f2);
            SizeF total_PriceSize = e.Graphics.MeasureString(total_Price, f4);

            e.Graphics.DrawString(allPrice, f2, Brushes.Black, e.PageBounds.Width - allPriceSize.Width - 220, 905);
            e.Graphics.DrawString(discound, f2, Brushes.Black, e.PageBounds.Width - discoundSize.Width - 650, 905);
            e.Graphics.DrawString(total_Price, f4, Brushes.Black, e.PageBounds.Width - total_PriceSize.Width - 400, 945);
        
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

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int new_total = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                new_total += (int)r.Cells[4].Value;
            }

            textBox5.Text = new_total.ToString();
            textBox7.Text = (int.Parse(textBox5.Text) - int.Parse(textBox6.Text)).ToString();
        }


        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && comboBox2.Text!="")
            {

                int id = items.FirstOrDefault(x => x.Value == comboBox2.Text).Key;

                textBox3.Text = con.getItemPrice(id).ToString();
               
                textBox4.Focus();
                
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
