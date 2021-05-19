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
    public partial class EditWorker : Form
    {
        int workerId;
        public EditWorker()
        {
            InitializeComponent();
        }
        public EditWorker(int id)
        {
            workerId = id;
            InitializeComponent();
            try
            {
                DbSets.Worker worker = MainForm.Database.Workers.Include(w => w.Role).First(w => w.id == workerId);
                if (worker == null) throw new Exception();
                textBox1.Text = worker.FullName;
                textBox2.Text = worker.PhoneNumber;
                textBox3.Text = worker.Address;
                textBox4.Text = worker.Passport;
                dateTimePicker1.Value = worker.DateOfBirth;

                foreach (var item in MainForm.Database.Roles)
                {
                    comboBox1.Items.Add(item.Name);
                    if (item.Name == worker.Role.Name) comboBox1.SelectedItem = item.Name;
                }
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
                DbSets.Worker worker = MainForm.Database.Workers.Include(w => w.Role).First(w => w.id == workerId);
                worker.FullName = textBox1.Text;
                worker.PhoneNumber = textBox2.Text;
                worker.Address = textBox3.Text;
                worker.Passport = textBox4.Text;
                worker.DateOfBirth = dateTimePicker1.Value;
                worker.Role = MainForm.Database.Roles.Single(r => r.Name == comboBox1.SelectedItem.ToString());
                MainForm.Database.Workers.Update(worker);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Данные о сотруднике обновлены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
