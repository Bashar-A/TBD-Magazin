﻿using System;
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
        Dictionary<string, int> productQuantity = new Dictionary<string, int>();
        Dictionary<string, int> productPriceQuantity = new Dictionary<string, int>();
        List<DbSets.OrderProduct> orderProducts = new List<DbSets.OrderProduct>();
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
            DbSets.Order order = new DbSets.Order();
            try
            {
                productQuantity = new Dictionary<string, int>();
                productPriceQuantity = new Dictionary<string, int>();
                orderProducts = new List<DbSets.OrderProduct>();

                order = new DbSets.Order
                {
                    Date = dateTimePicker1.Value
                };
                if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1) throw new Exception();
                order.SellerId = MainForm.Database.Workers.FirstOrDefault(w => w.FullName == comboBox1.SelectedItem.ToString()).id;
                order.ClientId = MainForm.Database.Clients.FirstOrDefault(p => p.FullName == comboBox2.SelectedItem.ToString()).id;

                MainForm.Database.Orders.Add(order);
                MainForm.Database.SaveChanges();

                foreach (var item in productRows)
                {
                    if (item.RowComboBoxProduct.SelectedIndex == -1 || Convert.ToInt32(item.RowTextBoxPrice.Text) < 0 || item.RowTextBoxQuantity.Value < 1) throw new Exception();

                    DbSets.Product product = MainForm.Database.Products.AsNoTracking().FirstOrDefault(p => p.Name == item.RowComboBoxProduct.SelectedItem.ToString());
                    if (!productPriceQuantity.TryAdd(
                        item.RowComboBoxProduct.SelectedItem.ToString() + "|" + item.RowTextBoxPrice.Text,
                        Convert.ToInt32(item.RowTextBoxQuantity.Text))) throw new Exception();

                    DbSets.OrderProduct orderProduct = new DbSets.OrderProduct
                    {
                        OrderId = order.id,
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
                MessageBox.Show("Заказ добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                if (order.id != 0)
                {
                    MainForm.Database.Orders.Remove(order);
                    MainForm.Database.SaveChanges();
                }
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
