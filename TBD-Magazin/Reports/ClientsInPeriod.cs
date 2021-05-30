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
    public partial class ClientsInPeriod : Form
    {
        public ClientsInPeriod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Orders.Include(o => o.Client).Where(o => o.Date >= dateTimePicker1.Value && o.Date <= dateTimePicker2.Value).Select(o => o.Client).Distinct())
            {
                dataGridView1.Rows.Add(item.id, item.FullName, item.PhoneNumber);
            }
        }

        private void ClientsInPeriod_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("FullName", "ФИО");
            dataGridView1.Columns.Add("PhoneNumber", "Телефон");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
        }
    }
}
