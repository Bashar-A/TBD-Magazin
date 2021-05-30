using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBD_Magazin
{
    public partial class MainForm : Form
    {
        public static DbConnection Database;
        public static DbSets.Worker User;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Database = new DbConnection();
            switch (User.Rights)
            {
                case DbSets.Worker.Right.NoRights:
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    break;
                case DbSets.Worker.Right.Admin:
                    break;
                case DbSets.Worker.Right.Manager:
                    button4.Visible = false;
                    break;
                case DbSets.Worker.Right.Seller:
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    break;
                case DbSets.Worker.Right.Courier:
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button8.Visible = false;
                    break;
                default:
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoriesAndManufacturers categoriesAndManufacturers = new CategoriesAndManufacturers();
            categoriesAndManufacturers.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WorkersAndRoles workersAndRoles = new WorkersAndRoles();
            workersAndRoles.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProvidersAndSupplies providersAndSupplies = new ProvidersAndSupplies();
            providersAndSupplies.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Deliveries deliveries = new Deliveries();
            deliveries.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
