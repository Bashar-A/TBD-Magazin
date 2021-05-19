using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TBD_Magazin
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("FullName", "ФИО");
            dataGridView1.Columns.Add("Email", "Электронная почта");
            dataGridView1.Columns.Add("PhoneNumber", "Телефон");
            RefreshObject();
        }

        public void RefreshObject()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Clients)
            {
                dataGridView1.Rows.Add(item.id, item.FullName, item.Email, item.PhoneNumber);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshObject();
        }
    }
}
