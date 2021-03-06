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
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject();

            switch (MainForm.User.Rights)
            {
                case DbSets.Worker.Right.NoRights:
                    button3.Visible = false;
                    break;
                case DbSets.Worker.Right.Admin:
                    break;
                case DbSets.Worker.Right.Manager:
                    button3.Visible = false;
                    break;
                case DbSets.Worker.Right.Seller:
                    button3.Visible = false;
                    break;
                case DbSets.Worker.Right.Courier:
                    button3.Visible = false;
                    break;
                default:
                    button3.Visible = false;
                    break;
            }
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (id == null) return;
            EditClient editClient = new EditClient(Convert.ToInt32(id.ToString()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == -1) return;
            object id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;
            if (id == null) return;
            try
            {
                DbSets.Client client = MainForm.Database.Clients.Find(id);
                MainForm.Database.Clients.Remove(client);
                MainForm.Database.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Cуществуют зависимые записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
