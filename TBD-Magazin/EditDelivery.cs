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
    public partial class EditDelivery : Form
    {
        int devliveryId;
        public EditDelivery()
        {
            InitializeComponent();
        }
        public EditDelivery(int id)
        {
            devliveryId = id;
            InitializeComponent();
            try
            {
                DbSets.Delivery delivery = MainForm.Database.Deliveries.Include(d => d.Manager).Include(d => d.Courier).First(d => d.id == devliveryId);
                if (delivery == null) throw new Exception();
                textBox1.Text = delivery.Address;
                textBox2.Text = delivery.OrderId.ToString();
                checkBox1.Checked = delivery.Deliveried;
                dateTimePicker1.Value = delivery.Date ?? DateTime.Now;

                foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Курьер"))
                {
                    comboBox1.Items.Add(item.FullName);
                    if (item.FullName == delivery.Courier?.FullName) comboBox1.SelectedItem = item.FullName;
                }
                foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Менеджер"))
                {
                    comboBox2.Items.Add(item.FullName);
                    if (item.FullName == delivery.Manager?.FullName) comboBox2.SelectedItem = item.FullName;
                }

                if (delivery.Deliveried)
                {
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    checkBox1.Enabled = false;
                    button1.Enabled = false;
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
                if (checkBox1.Checked && MessageBox.Show("Вы действительно хотите отметить заказ как доставлен?", "Подтверждение", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                DbSets.Delivery delivery = MainForm.Database.Deliveries.Include(d => d.Manager).Include(d => d.Courier).First(d => d.id == devliveryId);
                delivery.Address = textBox1.Text;
                delivery.Date = dateTimePicker1.Value;
                delivery.Deliveried = checkBox1.Checked;
                delivery.Courier = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox1.SelectedItem.ToString());
                delivery.Manager = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox2.SelectedItem.ToString());
                MainForm.Database.SaveChanges();
                MessageBox.Show("Данные о доставки обновлены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
