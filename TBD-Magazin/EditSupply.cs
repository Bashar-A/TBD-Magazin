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
        Dictionary<string, int> productQuantity = new Dictionary<string, int>();
        Dictionary<string, int> productPriceQuantity = new Dictionary<string, int>();
        List<DbSets.SupplyProduct> supplyProducts = new List<DbSets.SupplyProduct>();
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
                foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Менеджер"))
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
                productQuantity = new Dictionary<string, int>();
                productPriceQuantity = new Dictionary<string, int>();
                supplyProducts = new List<DbSets.SupplyProduct>();
                DbSets.Supply supply = MainForm.Database.Supplies.Include(s => s.Manager).Include(s => s.Provider).First(w => w.id == supplyId);
                supply.Date = dateTimePicker1.Value;
                supply.ManagerId = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox1.SelectedItem.ToString()).id;
                supply.ProviderId = MainForm.Database.Providors.FirstOrDefault(w => w.Name == comboBox2.SelectedItem.ToString()).id;

                List<DbSets.SupplyProduct> supplyProductOld = MainForm.Database.SupplyProducts.Include(s => s.Product).Where(s => s.SupplyId == supplyId).ToList();
                foreach (var item in supplyProductOld)
                {
                    TryAddOrUpdate(item.Product.Name, item.Product.Quantity, item.Quantity, false);
                }

                MainForm.Database.RemoveRange(supplyProductOld);

                foreach (var item in productRows)
                {
                    if (item.RowComboBoxProduct.SelectedIndex == -1 || Convert.ToInt32(item.RowTextBoxPrice.Text) < 0 || item.RowTextBoxQuantity.Value < 1) throw new Exception();

                    DbSets.Product product = MainForm.Database.Products.AsNoTracking().FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString());
                    if (!productPriceQuantity.TryAdd(
                        item.RowComboBoxProduct.SelectedItem.ToString() + "|" + item.RowTextBoxPrice.Text,
                        Convert.ToInt32(item.RowTextBoxQuantity.Text))) throw new Exception();

                    DbSets.SupplyProduct supplyProduct = new DbSets.SupplyProduct
                    {
                        SupplyId = supplyId,
                        ProductId = product.id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Value)
                    };

                    TryAddOrUpdate(product.Name, product.Quantity, supplyProduct.Quantity);
                    supplyProducts.Add(supplyProduct);
                }

                foreach (var item in productQuantity)
                {
                    if (item.Value < 0) throw new Exception();
                }

                foreach (var item in productQuantity)
                {
                    DbSets.Product product = MainForm.Database.Products.FirstOrDefault(p => p.Name == item.Key);
                    product.Quantity = item.Value;
                }
                foreach (var item in supplyProducts)
                {
                    MainForm.Database.SupplyProducts.Add(item);
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

        private void TryAddOrUpdate(string key, int value, int valueToAdd, bool plus = true)
        {
            productQuantity.TryAdd(key, value);
            productQuantity[key] += plus ? valueToAdd : -valueToAdd;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, autoPrice: true);
            productRows.Add(row);
        }
    }
}
