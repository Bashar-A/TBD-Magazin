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
    public partial class EditOrder : Form
    {
        int orderId;
        List<ProductRow> productRows = new List<ProductRow>();
        List<string> products = new List<string>();
        Dictionary<string, int> productQuantity = new Dictionary<string, int>();
        Dictionary<string, int> productPriceQuantity = new Dictionary<string, int>();
        List<DbSets.OrderProduct> orderProducts = new List<DbSets.OrderProduct>();
        public EditOrder()
        {
            InitializeComponent();
        }
        public EditOrder(int id)
        {
            orderId = id;
            InitializeComponent();
            try
            {
                foreach (var item in MainForm.Database.Products)
                {
                    products.Add(item.Name);
                }
                foreach (var item in MainForm.Database.OrderProducts.Include(o => o.Product).Where(o => o.OrderId == orderId))
                {
                    ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, item.Product.Name, item.Price, item.Quantity, true, label: label8);
                    productRows.Add(row);
                }
                


                DbSets.Order order = MainForm.Database.Orders.Include(o => o.Seller).Include(o => o.Client).First(o => o.id == orderId);
                if (order == null) throw new Exception();
                DbSets.Delivery delivery = MainForm.Database.Deliveries.Where(d => d.OrderId == orderId).FirstOrDefault();
                if (delivery != null) checkBox1.Checked = true;
                dateTimePicker1.Value = order.Date;
                label8.Text = order.OrderSum.ToString();
                foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Продавец"))
                {
                    comboBox1.Items.Add(item.FullName);
                    if (item.FullName == order.Seller.FullName) comboBox1.SelectedItem = item.FullName;
                }
                foreach (var item in MainForm.Database.Clients)
                {
                    comboBox2.Items.Add(item.FullName);
                    if (item.FullName == order.Client.FullName) comboBox2.SelectedItem = item.FullName;
                }

                if (delivery != null && delivery.Deliveried && MainForm.User.Rights != DbSets.Worker.Right.Admin)
                {
                    dateTimePicker1.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    foreach (var item in productRows)
                    {
                        item.RowButtonDelete.Enabled = false;
                        item.RowComboBoxProduct.Enabled = false;
                        item.RowTextBoxPrice.Enabled = false;
                        item.RowTextBoxQuantity.Enabled = false;
                    }

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
                orderProducts = new List<DbSets.OrderProduct>();

                DbSets.Order order = MainForm.Database.Orders.Include(o => o.Seller).Include(o => o.Client).First(o => o.id == orderId);
                order.Date = dateTimePicker1.Value;
                order.OrderSum = Convert.ToInt32(label8.Text);
                order.SellerId = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox1.SelectedItem.ToString()).id;
                order.ClientId = MainForm.Database.Clients.FirstOrDefault(c => c.FullName == comboBox2.SelectedItem.ToString()).id;

                List<DbSets.OrderProduct> orderProductOld = MainForm.Database.OrderProducts.Include(o => o.Product).Where(o => o.OrderId == orderId).ToList();
                foreach (var item in orderProductOld)
                {
                    TryAddOrUpdate(item.Product.Name, item.Product.Quantity, item.Quantity);
                }
                MainForm.Database.RemoveRange(orderProductOld);


                foreach (var item in productRows)
                {
                    if (item.RowComboBoxProduct.SelectedIndex == -1 || Convert.ToInt32(item.RowTextBoxPrice.Text) < 0 || item.RowTextBoxQuantity.Value < 1) throw new Exception();

                    DbSets.Product product = MainForm.Database.Products.AsNoTracking().FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString());
                    if (!productPriceQuantity.TryAdd(
                        item.RowComboBoxProduct.SelectedItem.ToString() + "|" + item.RowTextBoxPrice.Text,
                        Convert.ToInt32(item.RowTextBoxQuantity.Text))) throw new Exception();

                    DbSets.OrderProduct orderProduct = new DbSets.OrderProduct
                    {
                        OrderId = orderId,
                        ProductId = product.id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };

                    TryAddOrUpdate(product.Name, product.Quantity, orderProduct.Quantity, false);
                    orderProducts.Add(orderProduct);
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
                foreach (var item in orderProducts)
                {
                    MainForm.Database.OrderProducts.Add(item);
                }

                MainForm.Database.SaveChanges();
                MessageBox.Show("Данные о заказе обновлены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, autoPrice: true, label: label8);
            productRows.Add(row);
        }

        private void TryAddOrUpdate(string key, int value, int valueToAdd, bool plus = true)
        {
            productQuantity.TryAdd(key, value);
            productQuantity[key] += plus ? valueToAdd : -valueToAdd;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Check check = new Check(orderId);
            check.Show();
        }
    }
}
