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
    public partial class Check : Form
    {
        int orderId;
        public Check()
        {
            InitializeComponent();
        }

        public Check(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
        }

        private void Check1_Load(object sender, EventArgs e)
        {
            try
            {
                DbSets.Check check = MainForm.Database.Checks.AsNoTracking().Include(c => c.Order.Client).FirstOrDefault(c => c.OrderId == orderId);
                textBox1.Text = check.id.ToString();
                textBox2.Text = check.Date.ToShortDateString() + " " + check.Date.ToShortTimeString();
                textBox3.Text = check.Sum.ToString();
                textBox4.Text = check.Order.Client.FullName;
                this.Text = "Чек по заказу №" + orderId;
            }
            catch (Exception) { this.Close(); }
        }
    }
}
