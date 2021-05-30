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
    public partial class OrdersInPeriod : Form
    {
        public OrdersInPeriod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<DbSets.Order> orders = MainForm.Database.Orders.AsNoTracking().Where(o => o.Date >= dateTimePicker1.Value && o.Date <= dateTimePicker2.Value).ToList();
            foreach (var item in orders)
            {
                if (item.OrderSum <= 0) continue;
                int count = 0;
                foreach (var p in MainForm.Database.OrderProducts.Where(o => o.OrderId == item.id))
                {
                    count += p.Quantity;
                }
                dataGridView1.Rows.Add(item.id, item.Date, count, item.OrderSum);
            }
            label3.Text = "Количество продаж: " + dataGridView1.Rows.Count;
        }

        private void OrdersInPeriod_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Count", "Количество товаров");
            dataGridView1.Columns.Add("Sum", "Стоимость заказа");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
        }
    }
}
