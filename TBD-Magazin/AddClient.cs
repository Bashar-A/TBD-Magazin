using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TBD_Magazin
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0 || textBox3.Text.Length == 0) throw new Exception();
                DbSets.Client client = new DbSets.Client { FullName = textBox1.Text, Email = textBox2.Text, PhoneNumber = textBox3.Text };
                MainForm.Database.Clients.Add(client);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Клиент добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
