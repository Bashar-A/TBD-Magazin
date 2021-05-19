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
    public partial class EditSupply : Form
    {
        int supplyId;
        List<ProductRow> productRows = new List<ProductRow>();
        List<string> products = new List<string>();
        public EditSupply()
        {
            InitializeComponent();
        }
        public EditSupply(int id)
        {
            supplyId = id;
            InitializeComponent();
            try
            {
                foreach (var item in MainForm.Database.Products)
                {
                    products.Add(item.Name);
                }
                foreach (var item in MainForm.Database.SupplyProducts.Include(s => s.Product).Where(s => s.SupplyId == supplyId))
                {
                    ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, item.Product.Name, item.Price, item.Quantity);
                    productRows.Add(row);
                }
                


                DbSets.Supply supply = MainForm.Database.Supplies.Include(s => s.Manager).Include(s => s.Provider).First(s => s.id == supplyId);
                if (supply == null) throw new Exception();
                dateTimePicker1.Value = supply.Date;
                foreach (var item in MainForm.Database.Workers)
                {
                    comboBox1.Items.Add(item.FullName);
                    if (item.FullName == supply.Manager.FullName) comboBox1.SelectedItem = item.FullName;
                }
                foreach (var item in MainForm.Database.Providors)
                {
                    comboBox2.Items.Add(item.Name);
                    if (item.Name == supply.Provider.Name) comboBox2.SelectedItem = item.Name;
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
                DbSets.Supply supply = MainForm.Database.Supplies.Include(s => s.Manager).Include(s => s.Provider).First(w => w.id == supplyId);
                supply.Date = dateTimePicker1.Value;
                supply.Manager = MainForm.Database.Workers.Single(w => w.FullName == comboBox1.SelectedItem.ToString());
                supply.Provider = MainForm.Database.Providors.Single(w => w.Name == comboBox2.SelectedItem.ToString());
                MainForm.Database.Supplies.Update(supply);

                List<DbSets.SupplyProduct> supplyProducts = MainForm.Database.SupplyProducts.Where(s => s.SupplyId == supplyId).ToList();
                MainForm.Database.RemoveRange(supplyProducts);
                foreach (var item in productRows)
                {
                    DbSets.SupplyProduct supplyProduct = new DbSets.SupplyProduct
                    {
                        SupplyId = supplyId,
                        ProductId = MainForm.Database.Products.FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString()).id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };
                    MainForm.Database.SupplyProducts.Add(supplyProduct);
                }

                MainForm.Database.SaveChanges();
                MessageBox.Show("Данные о поставки обновлены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
