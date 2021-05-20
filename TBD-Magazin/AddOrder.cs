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
    public partial class AddOrder : Form
    {
        List<ProductRow> productRows = new List<ProductRow>();
        List<string> products = new List<string>();
        public AddOrder()
        {
            InitializeComponent();
            foreach (var item in MainForm.Database.Workers.Where(w => w.Role.Name == "Продавец"))
            {
                comboBox1.Items.Add(item.FullName);
            }
            foreach (var item in MainForm.Database.Clients)
            {
                comboBox2.Items.Add(item.FullName);
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
                DbSets.Order order = new DbSets.Order
                {
                    Date = dateTimePicker1.Value
                };

                order.Seller = MainForm.Database.Workers.Single(w => w.FullName == comboBox1.SelectedItem.ToString());
                order.Client = MainForm.Database.Clients.Single(p => p.FullName == comboBox2.SelectedItem.ToString());

                MainForm.Database.Orders.Add(order);
                MainForm.Database.SaveChanges();

                foreach (var item in productRows)
                {
                    DbSets.OrderProduct orderProduct = new DbSets.OrderProduct
                    {
                        OrderId = order.id,
                        ProductId = MainForm.Database.Products.FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString()).id,
                        Price = Convert.ToInt32(item.RowTextBoxPrice.Text),
                        Quantity = Convert.ToInt32(item.RowTextBoxQuantity.Text)
                    };
                    MainForm.Database.OrderProducts.Add(orderProduct);
                }

                MainForm.Database.SaveChanges();
                MessageBox.Show("Заказ добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
