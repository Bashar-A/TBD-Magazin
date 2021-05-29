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
    public partial class ProvidersAndSupplies : Form
    {
        public ProvidersAndSupplies()
        {
            InitializeComponent();
        }

        private void ProvidersAndSupplies_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Manager", "Менеджер");
            dataGridView1.Columns.Add("Provider", "Поставщик");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject1();

            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("Name", "Наименование");
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            RefreshObject2();
        }

        public void RefreshObject1()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in MainForm.Database.Supplies.Include(s => s.Manager).Include(s => s.Provider))
            {
                dataGridView1.Rows.Add
                    (
                    item.id,
                    item.Date.ToShortDateString() + " " + item.Date.ToShortTimeString(),
                    item.Manager.FullName, 
                    item.Provider.Name
                    );
            }
        }

        public void RefreshObject2()
        {
            dataGridView2.Rows.Clear();
            foreach (var item in MainForm.Database.Providors)
            {
                dataGridView2.Rows.Add(item.id, item.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSupply addSupply = new AddSupply();
            addSupply.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string input = "";
            if(ShowInputDialog(ref input) == DialogResult.Cancel || input == "") return;
            try
            {
                DbSets.Providor providor = new DbSets.Providor { Name = input };
                MainForm.Database.Providors.Add(providor);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Поставщик добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshObject2();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RefreshObject1();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            RefreshObject2();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (id == null) return;
            EditSupply editSupply = new EditSupply(Convert.ToInt32(id.ToString()));
        }



        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object oid = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
            if (oid == null) return;
            int id = Convert.ToInt32(oid.ToString());
            

            try
            {
                DbSets.Providor providor = MainForm.Database.Providors.Find(id);
                string input = providor.Name;
                if (ShowInputDialog(ref input) == DialogResult.Cancel || input == "" || input == providor.Name) return;
                providor.Name = input;
                MainForm.Database.Providors.Update(providor);
                MainForm.Database.SaveChanges();
                MessageBox.Show("Поставщик обновлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshObject2();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(240, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Наименование";
            inputBox.StartPosition = FormStartPosition.CenterScreen;

            TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&Принять";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Отмена";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == -1) return;
            object id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;
            if (id == null) return;
            try
            {
                DbSets.Supply supply = MainForm.Database.Supplies.Find(id);
                MainForm.Database.Supplies.Remove(supply);
                MainForm.Database.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Cуществуют зависимые записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell.RowIndex == -1) return;
            object id = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value;
            if (id == null) return;
            try
            {
                DbSets.Providor providor = MainForm.Database.Providors.Find(id);
                MainForm.Database.Providors.Remove(providor);
                MainForm.Database.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Cуществуют зависимые записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
