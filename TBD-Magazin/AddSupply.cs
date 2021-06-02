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
        Dictionary<string, int> productPriceQuantity = new Dictionary<string, int>();
        List<DbSets.SupplyProduct> supplyProducts = new List<DbSets.SupplyProduct>();
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
            DbSets.Supply supply = new DbSets.Supply();
            try
            {
                productPriceQuantity = new Dictionary<string, int>();
                supplyProducts = new List<DbSets.SupplyProduct>();

                supply = new DbSets.Supply
                {
                    Date = dateTimePicker1.Value
                };
                if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1) throw new Exception();
                supply.ManagerId = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox1.SelectedItem.ToString()).id;
                supply.ProviderId = MainForm.Database.Providors.FirstOrDefault(p => p.Name == comboBox2.SelectedItem.ToString()).id;

                MainForm.Database.Supplies.Add(supply);
                MainForm.Database.SaveChanges();

                foreach (var item in productRows)
                {
                    if(item.RowComboBoxProduct.SelectedIndex == -1 || Convert.ToInt32(item.RowTextBoxPrice.Text) < 0 || item.RowTextBoxQuantity.Value < 1) throw new Exception();

                    int productId = MainForm.Database.Products.AsNoTracking().FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString()).id;
                    if(!productPriceQuantity.TryAdd(
                        item.RowComboBoxProduct.SelectedItem.ToString() + "|" + item.RowTextBoxPrice.Text,
                        Convert.ToInt32(item.RowTextBoxQuantity.Text))) throw new Exception();


                    DbSets.SupplyProduct supplyProduct = new DbSets.SupplyProduct
                    {
                        SupplyId = supply.id,
                        ProductId = productId,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };
                    supplyProducts.Add(supplyProduct);
                    
                }

                foreach (var item in supplyProducts)
                {
                    DbSets.Product product = MainForm.Database.Products.Find(item.ProductId);
                    //product.Quantity += item.Quantity;
                    MainForm.Database.SupplyProducts.Add(item);
                }

                MainForm.Database.SaveChanges();
                MessageBox.Show("Поставка добавлена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                if (supply.id != 0)
                {
                    try
                    {
                        MainForm.Database.Supplies.Remove(supply);
                        MainForm.Database.SaveChanges();
                    }
                    catch (Exception) { }
                }
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, autoPrice: true);
            productRows.Add(row);
        }
    }
}
