using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TBD_Magazin
{
    public partial class EditClient : Form
    {
        int clientID;
        public EditClient()
        {
            InitializeComponent();
        }
        public EditClient(int id)
        {
            clientID = id;
            InitializeComponent();
            try
            {
                DbSets.Client client = MainForm.Database.Clients.Find(clientID);
                if (client == null) throw new Exception();
                textBox1.Text = client.FullName;
                textBox2.Text = client.Email;
                textBox3.Text = client.PhoneNumber;
                this.Show();
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbSets.Client client = MainForm.Database.Clients.Find(clientID);
                client.FullName = textBox1.Text;
                client.Email = textBox2.Text;
                client.PhoneNumber = textBox3.Text;
                MainForm.Database.Clients.Update(client);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Клиент обновлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
