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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Seller", "Продавец");
            dataGridView1.Columns.Add("Client", "Клиент");
            dataGridView1.Columns.Add("Sum", "Стоимость заказа");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject1();
        }

        public void RefreshObject1()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Orders.Include(o => o.Seller).Include(s => s.Client))
            {
                dataGridView1.Rows.Add
                    (
                    item.id,
                    item.Date.ToShortDateString() + " " + item.Date.ToShortTimeString(),
                    item.Seller.FullName, 
                    item.Client.FullName,
                    item.OrderSum
                    );
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            addOrder.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RefreshObject1();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (id == null) return;
            EditOrder editOrder = new EditOrder(Convert.ToInt32(id.ToString()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == -1) return;
            object id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;
            if (id == null) return;
            try
            {
                DbSets.Order order = MainForm.Database.Orders.Find(id);
                MainForm.Database.Orders.Remove(order);
                MainForm.Database.SaveChanges();
            }
            catch (Exception) {
                MessageBox.Show("Ошибка! Cуществуют зависимые записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
