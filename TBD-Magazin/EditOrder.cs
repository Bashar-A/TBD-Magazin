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
                    ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows, item.Product.Name, item.Price, item.Quantity);
                    productRows.Add(row);
                }
                


                DbSets.Order order = MainForm.Database.Orders.Include(o => o.Seller).Include(o => o.Client).First(o => o.id == orderId);
                if (order == null) throw new Exception();
                dateTimePicker1.Value = order.Date;
                label7.Text = order.OrderSum.ToString();
                foreach (var item in MainForm.Database.Workers)
                {
                    comboBox1.Items.Add(item.FullName);
                    if (item.FullName == order.Seller.FullName) comboBox1.SelectedItem = item.FullName;
                }
                foreach (var item in MainForm.Database.Clients)
                {
                    comboBox2.Items.Add(item.FullName);
                    if (item.FullName == order.Client.FullName) comboBox2.SelectedItem = item.FullName;
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
                DbSets.Order order = MainForm.Database.Orders.Include(o => o.Seller).Include(o => o.Client).First(o => o.id == orderId);
                order.Date = dateTimePicker1.Value;
                order.Seller = MainForm.Database.Workers.Single(w => w.FullName == comboBox1.SelectedItem.ToString());
                order.Client = MainForm.Database.Clients.Single(c => c.FullName == comboBox2.SelectedItem.ToString());
                MainForm.Database.Orders.Update(order);

                List<DbSets.OrderProduct> orderProducts = MainForm.Database.OrderProducts.Where(o => o.OrderId == orderId).ToList();
                MainForm.Database.RemoveRange(orderProducts);
                foreach (var item in productRows)
                {
                    DbSets.OrderProduct orderProduct = new DbSets.OrderProduct
                    {
                        OrderId = orderId,
                        ProductId = MainForm.Database.Products.FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString()).id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };
                    MainForm.Database.OrderProducts.Add(orderProduct);
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
            ProductRow row = new ProductRow(flowLayoutPanel1, products, productRows);
            productRows.Add(row);
        }
    }
}
