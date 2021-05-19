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
    public partial class AddSupply : Form
    {
        List<ProductRow> productRows = new List<ProductRow>();
        List<string> products = new List<string>();
        public AddSupply()
        {
            InitializeComponent();
            foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Менеджер"))
            {
                comboBox1.Items.Add(item.FullName);
            }
            foreach (var item in MainForm.Database.Providors)
            {
                comboBox2.Items.Add(item.Name);
            }
            foreach (var item in MainForm.Database.Products)
            {
                products.Add(item.Name);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbSets.Supply supply = new DbSets.Supply
                {
                    Date = dateTimePicker1.Value
                };

                supply.Manager = MainForm.Database.Workers.Single(w => w.FullName == comboBox1.SelectedItem.ToString());
                supply.Provider = MainForm.Database.Providors.Single(p => p.Name == comboBox2.SelectedItem.ToString());

                MainForm.Database.Supplies.Add(supply);
                MainForm.Database.SaveChanges();

                foreach (var item in productRows)
                {
                    DbSets.SupplyProduct supplyProduct = new DbSets.SupplyProduct
                    {
                        SupplyId = supply.id,
                        ProductId = MainForm.Database.Products.FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString()).id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };
                    MainForm.Database.SupplyProducts.Add(supplyProduct);
                }

                MainForm.Database.SaveChanges();
                MessageBox.Show("Поставка добавлена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows);
            productRows.Add(row);
        }
    }
}
