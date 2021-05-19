using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TBD_Magazin
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Наименование");
            dataGridView1.Columns.Add("Price", "Цена");
            dataGridView1.Columns.Add("Quantity", "Количество на складе");
            dataGridView1.Columns.Add("Category", "Категория");
            dataGridView1.Columns.Add("Manufacturer", "Производитель");
            dataGridView1.Columns.Add("Power", "Мощность");
            dataGridView1.Columns.Add("Garanty", "Срок гарантии");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject();
        }

        public void RefreshObject()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Products.Include(p => p.Category).Include(p => p.Manufacturer))
            {
                dataGridView1.Rows.Add(item.id, item.Name, item.Price, item.Quantity, item.Category.Name, item.Manufacturer.Name, item.Power, item.Garanty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshObject();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (id == null) return;
            EditProduct editProduct = new EditProduct(Convert.ToInt32(id.ToString()));
        }
    }
}
