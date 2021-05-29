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
    public partial class Deliveries : Form
    {
        public Deliveries()
        {
            InitializeComponent();
        }

        private void Deliveries_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Address", "Адрес");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Order", "Номер заказа");
            dataGridView1.Columns.Add("Courier", "Курьер");
            dataGridView1.Columns.Add("Manager", "Менеджер");
            dataGridView1.Columns.Add("Deliveried", "Доставлено");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject();
        }

        public void RefreshObject()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Deliveries.AsNoTracking().Include(d => d.Courier).Include(d => d.Manager))
            {
                dataGridView1.Rows.Add(item.id, item.Address, item.Date, item.OrderId, item.Courier?.FullName, item.Manager?.FullName, item.Deliveried ? "Да" : "Нет");
                if (item.Deliveried) dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
                else dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
            }
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
            EditDelivery editDelivery = new EditDelivery(Convert.ToInt32(id.ToString()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == -1) return;
            object id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;
            if (id == null) return;
            try
            {
                DbSets.Delivery delivery = MainForm.Database.Deliveries.Find(id);
                MainForm.Database.Deliveries.Remove(delivery);
                MainForm.Database.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Cуществуют зависимые записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
