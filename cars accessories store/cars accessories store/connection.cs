using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace cars_accessories_store
{
    class connection
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\database\\Elkersh.mdf;Integrated Security=True;User Instance=True");

        SqlCommand cmd;

        SqlDataAdapter da;
        DataTable dt;


        public DataTable showItems()
        {
            try
            {

                da = new SqlDataAdapter("select  itemName as [اسم الصنف] , itemManufact as [بلد الصناعة] , itemCost as [سعر الجملة], itemPrice as [سعر البيع], itemNote as ملحوظات, itemStock as [العدد بالمخزن] from Items", con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void insertItem( string name, string manufact, int cost,int price, string note,int stock)
        {
            try
            {
                string sql = "insert into Items ( itemName , itemManufact , itemCost , itemPrice , itemNote , itemStock ) values (N'" + name + "',N'" + manufact + "'," + cost + "," + price + ",N'" + note + "'," + stock + ")";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("تم الحفظ بنجاح");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        } 

        public void updateItem(int ID, string name, string manufact, int cost, int price, string note, int stock)
        {
            try
            {
                string sql = "UPDATE Items SET itemName = N'" + name + "' , itemManufact=N'" + manufact + "' , itemCost= " + cost + " , itemPrice= " + price + ", itemNote=N'" + note + "' , itemStock=" + stock + "   WHERE itemID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("تم التعديل بنجاح");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void subtractItemStock(int ID,int stock)
        {
            try
            {
                string sql = "UPDATE Items SET itemStock = itemStock - " + stock + "  WHERE itemID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        } 

        public void AddItemStock(int ID, int stock)
        {
            try
            {

                string sql = "UPDATE Items SET itemStock = itemStock + " + stock + "  WHERE itemID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

          
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        } 

        public void DeleteItem(int ID)
        {
            try
            {
                string sql = "DELETE FROM Items WHERE itemID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("تم حذف الصنف بنجاح");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void DeleteBill(int ID)
        {
            try
            {
                string sql = "DELETE FROM Bills WHERE billID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();



                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void DeleteBillItem(int ID)
        {
            try
            {
                string sql = "DELETE FROM Sold WHERE billID =" + ID;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();



                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public DataTable getItem_quantity_Sold(int ID)
        {

            try
            {

                da = new SqlDataAdapter("select itemID,quantity from Sold WHERE billID =" + ID, con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        

        public string getPassword()
        {
            string Password = "";

            try
            {
                string sql = "select RTRIM(sellerPass) from Seller where sellerID=1";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Password = reader[0].ToString();

                }


                reader.Close();
                con.Close();
                return Password;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Password;
        } //Done

        public List<string> getItemsName()
        {
            List<string> items = new List<string>();

            try
            {
                string sql = "select itemName from Items";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                        items.Add(reader[0].ToString());

                }



                con.Close();
                return items;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return items;
        }

        public Dictionary<int, string> getItemsName_ID()
        {
            Dictionary<int, string> items = new Dictionary<int, string>();

            try
            {
                string sql = "select itemID,itemName from Items";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int key=int.Parse(reader[0].ToString());
                        string value=reader[1].ToString();
                       items.Add(key, value);
                    }

                }



                con.Close();
                return items;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return items;
        }

        public List<string> getItemInfo(int id)
        {
            List<string> item = new List<string>();

            try
            {
                string sql = "select * from Items where itemID="+id;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item.Add(reader[2].ToString());
                        item.Add(reader[3].ToString());
                        item.Add(reader[4].ToString());
                        item.Add(reader[5].ToString());
                        item.Add(reader[6].ToString());
                        item.Add(reader[1].ToString());
                    }

                }



                con.Close();
                return item;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return item;
        }

        public int getItemPrice(int id)
        {
            try
            {
                string sql = "select itemPrice from Items where itemID="+id;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int i = int.Parse(reader[0].ToString());
                    con.Close();
                    return i;

                }
                else
                {
                    MessageBox.Show("");
                }


                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public int getIDENT_CURRENT()
        {
            try
            {
                string sql = " SELECT IDENT_CURRENT('Bills') +1";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int i = int.Parse(reader[0].ToString());
                    con.Close();
                    return i;

                }
                else
                {
                    MessageBox.Show("");
                }


                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public int getItemCost(int id)
        {
            try
            {
                string sql = "select itemCost from Items where itemID=" + id;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int i = int.Parse(reader[0].ToString());
                    con.Close();
                    return i;

                }
                else
                {
                    MessageBox.Show("");
                }


                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public int getItemStock(int id)
        {
            try
            {
                string sql = "select itemStock from Items where itemID=" + id;

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    int i = int.Parse(reader[0].ToString());
                    con.Close();
                    return i;

                }
                else
                {
                    MessageBox.Show("");
                }


                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        
        public int getMaxBillID()
        {
            try
            {
                string sql = "select Max(billID) from Bills ";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                        int i;
                        var isNumeric = int.TryParse(reader[0].ToString(), out i);
                        // i = int.Parse(reader[0].ToString());

                        if (isNumeric)
                        {

                            con.Close();
                            return i;
                        }
                        con.Close();
                        return 1;
                 

                }


                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public void insertBill(DateTime date, string name, string mobile, string type, int discount, int total, int paid,int profit)
        {
            try
            {
                string sql = "insert into Bills ( billDate , clientName , clientMobile , billType , billDiscount , billTotal , billPaid , billprofit ) values ('" + date + "',N'" + name + "','" + mobile + "',N'" + type + "'," + discount + "," + total + "," + paid + "," + profit + ")";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("تمت اضافة الفاتورة بنجاح");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public DataTable showBills()
        {
            try
            {

                da = new SqlDataAdapter("select  billID as [رقم الفاتورة] , billDate as [التاريخ] , clientName as [اسم العميل] , clientMobile as [رقم الموبايل], billType as [نوع الفاتورة], billDiscount as الخصم, billTotal as [الاجمالى ] , billPaid as [الاجمالى بعد الخصم] , billprofit as [الربح] from Bills", con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public DataTable showBillsByDate(DateTime d1,DateTime d2)
        {
            try
            {

                da = new SqlDataAdapter("select  billID as [رقم الفاتورة] , billDate as [التاريخ] , clientName as [اسم العميل] , clientMobile as [رقم الموبايل], billType as [نوع الفاتورة], billDiscount as الخصم, billTotal as [الاجمالى ] , billPaid as [الاجمالى بعد الخصم] , billprofit as [الربح] from Bills where billDate between '"+d1+"' and '"+d2+"'", con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public DataTable showBillsByID(int id)
        {
            try
            {

                da = new SqlDataAdapter("select  billID as [رقم الفاتورة] , billDate as [التاريخ] , clientName as [اسم العميل] , clientMobile as [رقم الموبايل], billType as [نوع الفاتورة], billDiscount as الخصم, billTotal as [الاجمالى ] , billPaid as [الاجمالى بعد الخصم] , billprofit as [الربح] from Bills where billID =" + id, con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public DataTable showBillItems(int id)
        {
            try
            {

                da = new SqlDataAdapter("select  Items.itemName as [الصنـــــف] ,Items.itemPrice as [سعر الوحده] , Sold.quantity as [العدد] from Sold INNER JOIN Items ON Sold.itemID = Items.itemID  where Sold.BillID =" + id, con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void insertBillItems(int billID, int itemID, int quantity)
        {
            try
            {
                string sql = "insert into Sold ( billID,itemID ,quantity ) values (" + billID + "," + itemID + "," + quantity + ")";

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();
              ///  MessageBox.Show("تمت اضافة الاصناف للفاتورة بنجاح");
            }

            catch (Exception ex)
            {

                MessageBox.Show(" برجاء عدم تكرار العنصر داخل الفاتورة \n"+ex.Message);

            }
        }
    }


}
