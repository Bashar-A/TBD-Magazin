using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace TBD_Magazin
{
    public partial class AddWorker : Form
    {
        public AddWorker()
        {
            InitializeComponent();
            foreach (var item in MainForm.Database.Roles)
            {
                comboBox1.Items.Add(item.Name);
            }
            comboBox2.DataSource = Enum.GetValues(typeof(DbSets.Worker.Right));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbSets.Worker.Right right;
                Enum.TryParse<DbSets.Worker.Right>(comboBox2.SelectedValue.ToString(), out right);
                DbSets.Worker worker = new DbSets.Worker
                {
                    FullName = textBox1.Text,
                    PhoneNumber = textBox2.Text,
                    Address = textBox3.Text,
                    Passport = textBox4.Text,
                    DateOfBirth = dateTimePicker1.Value,
                    Password = textBox5.Text,
                    Rights = right
                    
                };

                worker.Role = MainForm.Database.Roles.Single(r => r.Name == comboBox1.SelectedItem.ToString());

                MainForm.Database.Workers.Add(worker);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Сотрудник добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
