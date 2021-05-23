using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace TBD_Magazin
{
    class ProductRow
    {
        public ProductRow(Control control, List<string> products, List<ProductRow> rowList, string selectedProduct = "", int price = -1, int quantity = 1, bool autoPrice = false, Label label = null)
        {
            SumLabel = label;

            RowComboBoxProduct = new ComboBox();
            RowComboBoxProduct.Size = new System.Drawing.Size(310, 23);
            RowComboBoxProduct.AutoSize = false;
            RowComboBoxProduct.Parent = control;
            RowComboBoxProduct.Items.AddRange(products.ToArray());
            RowComboBoxProduct.SelectedItem = selectedProduct;
            RowComboBoxProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            if(autoPrice)RowComboBoxProduct.SelectedIndexChanged += RowComboBoxProduct_SelectedIndexChanged;


            RowTextBoxPrice = new TextBox();
            RowTextBoxPrice.Size = new System.Drawing.Size(74, 23);
            RowTextBoxPrice.AutoSize = false;
            RowTextBoxPrice.Parent = control;
            RowTextBoxPrice.TextAlign = HorizontalAlignment.Right;
            if(price != -1) RowTextBoxPrice.Text = price.ToString();
            RowTextBoxPrice.TextChanged += ValueChanged;


            RowTextBoxQuantity = new NumericUpDown();
            RowTextBoxQuantity.Size = new System.Drawing.Size(74, 23);
            RowTextBoxQuantity.AutoSize = false;
            RowTextBoxQuantity.Parent = control;
            RowTextBoxQuantity.TextAlign = HorizontalAlignment.Right;
            RowTextBoxQuantity.ValueChanged += ValueChanged;
            RowTextBoxQuantity.Value = quantity;
            RowTextBoxQuantity.Minimum = 1;
            RowTextBoxQuantity.Maximum = 1000;


            RowButtonDelete = new Button();
            RowButtonDelete.Size = new System.Drawing.Size(23, 23);
            RowButtonDelete.Text = "X";
            RowButtonDelete.TextAlign = ContentAlignment.MiddleCenter;
            RowButtonDelete.Parent = control;
            RowButtonDelete.Click += TaskButton_Click;
            ProductRows = rowList;

        }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (SumLabel == null) return;
            try
            {
                SumLabel.Text = "0";
                int sum = 0;
                foreach (var item in ProductRows)
                {
                    sum += Convert.ToInt32(item.RowTextBoxPrice.Text) * (int)item.RowTextBoxQuantity.Value;
                }
                SumLabel.Text = sum.ToString();
            }
            catch (Exception) { }
        }

        private void RowComboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DbSets.Product product = MainForm.Database.Products.Where(p => p.Name == RowComboBoxProduct.SelectedItem.ToString()).FirstOrDefault();
                RowTextBoxPrice.Text = product.Price.ToString();
            }
            catch (Exception) { }
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            ProductRows.Remove(this);
            Dispose();
        }

        ~ProductRow()
        {
            RowComboBoxProduct.Dispose();
            RowTextBoxQuantity.Dispose();
            RowTextBoxPrice.Dispose();
            RowButtonDelete.Dispose();
        }

        public void Dispose()
        {
            RowComboBoxProduct.Controls.Clear();
            RowTextBoxQuantity.Controls.Clear();
            RowTextBoxPrice.Controls.Clear();
            RowButtonDelete.Controls.Clear();

            RowComboBoxProduct.Dispose();
            RowTextBoxQuantity.Dispose();
            RowTextBoxPrice.Dispose();
            RowButtonDelete.Dispose();
        }

        public Label SumLabel { get; set; }
        public ComboBox RowComboBoxProduct { get; set; }
        public TextBox RowTextBoxPrice { get; set; }
        public NumericUpDown RowTextBoxQuantity { get; set; }
        public Button RowButtonDelete { get; set; }
        List<ProductRow> ProductRows = new List<ProductRow>();
    }
}
