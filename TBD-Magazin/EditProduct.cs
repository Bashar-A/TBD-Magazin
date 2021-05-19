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
    public partial class EditProduct : Form
    {
        int productId;
        public EditProduct()
        {
            InitializeComponent();
        }
        public EditProduct(int id)
        {
            productId = id;
            InitializeComponent();
            try
            {
                DbSets.Product product = MainForm.Database.Products.Include(p => p.Category).Include(p => p.Manufacturer).First(p => p.id == productId);
                if (product == null) throw new Exception();
                textBox1.Text = product.Name;
                textBox2.Text = product.Price.ToString();
                textBox3.Text = product.Power.ToString();
                textBox4.Text = product.Garanty.ToString();

                foreach (var item in MainForm.Database.Categories)
                {
                    comboBox1.Items.Add(item.Name);
                    if (item.Name == product.Category.Name) comboBox1.SelectedItem = item.Name;
                }

                foreach (var item in MainForm.Database.Manufacturers)
                {
                    comboBox2.Items.Add(item.Name);
                    if (item.Name == product.Manufacturer.Name) comboBox2.SelectedItem = item.Name;
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
                DbSets.Product product = MainForm.Database.Products.Include(p => p.Category).Include(p => p.Manufacturer).First(p => p.id == productId);
                product.Name = textBox1.Text;
                product.Price = Convert.ToInt32(textBox2.Text);
                product.Power = Convert.ToInt32(textBox3.Text);
                product.Garanty = Convert.ToInt32(textBox4.Text);
                product.Category = MainForm.Database.Categories.Single(c => c.Name == comboBox1.SelectedItem.ToString());
                product.Manufacturer = MainForm.Database.Manufacturers.Single(m => m.Name == comboBox2.SelectedItem.ToString());
                MainForm.Database.Products.Update(product);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Товар обновлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
