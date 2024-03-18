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
    public partial class BillItems : Form
    {
        public BillItems()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = true;
        }
    }
}
