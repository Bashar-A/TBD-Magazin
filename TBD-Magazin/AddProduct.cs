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
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            foreach (var item in MainForm.Database.Categories)
            {
                comboBox1.Items.Add(item.Name);
            }

            foreach (var item in MainForm.Database.Manufacturers)
            {
                comboBox2.Items.Add(item.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbSets.Product product = new DbSets.Product { 
                    Name = textBox1.Text, 
                    Price = Convert.ToInt32(textBox2.Text), 
                    Power = Convert.ToInt32(textBox3.Text),
                    Garanty = Convert.ToInt32(textBox4.Text) 
                };

                product.Category = MainForm.Database.Categories.Single(c => c.Name == comboBox1.SelectedItem.ToString());
                product.Manufacturer = MainForm.Database.Manufacturers.Single(m => m.Name == comboBox2.SelectedItem.ToString());

                MainForm.Database.Products.Add(product);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Товар добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
