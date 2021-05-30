using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TBD_Magazin
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.User = MainForm.Database.Workers.AsNoTracking().Where(w => w.PhoneNumber.Equals(textBox1.Text) && w.Password.Equals(textBox2.Text)).FirstOrDefault();
                if (MainForm.User == null) throw new Exception();
                else
                {
                    this.Hide();
                    MainForm main = new MainForm();
                    main.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Не правльный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            MainForm.Database = new DbConnection();
        }

    }
}
