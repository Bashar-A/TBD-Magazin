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
        public ProductRow(Control control, List<string> products, List<ProductRow> rowList, string selectedProduct = "", int price = -1, int quantity = 1)
        {
            RowComboBoxProduct = new ComboBox();
            RowComboBoxProduct.Size = new System.Drawing.Size(310, 23);
            RowComboBoxProduct.AutoSize = false;
            RowComboBoxProduct.Parent = control;
            RowComboBoxProduct.Items.AddRange(products.ToArray());
            RowComboBoxProduct.SelectedItem = selectedProduct;

            RowTextBoxPrice = new TextBox();
            RowTextBoxPrice.Size = new System.Drawing.Size(74, 23);
            RowTextBoxPrice.AutoSize = false;
            RowTextBoxPrice.Parent = control;
            RowTextBoxPrice.TextAlign = HorizontalAlignment.Right;
            if(price != -1) RowTextBoxPrice.Text = price.ToString();

            RowTextBoxQuantity = new TextBox();
            RowTextBoxQuantity.Size = new System.Drawing.Size(74, 23);
            RowTextBoxQuantity.AutoSize = false;
            RowTextBoxQuantity.Parent = control;
            RowTextBoxQuantity.TextAlign = HorizontalAlignment.Right;
            RowTextBoxQuantity.Text = quantity.ToString();


            RowButtonDelete = new Button();
            RowButtonDelete.Size = new System.Drawing.Size(23, 23);
            RowButtonDelete.Text = "X";
            RowButtonDelete.TextAlign = ContentAlignment.MiddleCenter;
            RowButtonDelete.Parent = control;
            RowButtonDelete.Click += TaskButton_Click;
            ProductRows = rowList;

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

        public ComboBox RowComboBoxProduct { get; set; }
        public TextBox RowTextBoxPrice { get; set; }
        public TextBox RowTextBoxQuantity { get; set; }
        public Button RowButtonDelete { get; set; }
        List<ProductRow> ProductRows = new List<ProductRow>();
    }
}
