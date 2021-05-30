using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TBD_Magazin.Reports
{
    public partial class ProductsInStock : Form
    {
        public ProductsInStock()
        {
            InitializeComponent();
        }

        private void ProductsInStock_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Наименование");
            dataGridView1.Columns.Add("Stock", "Количество на складе");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;

            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Products)
            {
                if (item.Quantity <= 0) continue;
                dataGridView1.Rows.Add(item.id, item.Name, item.Quantity);
            }
        }
    }
}
