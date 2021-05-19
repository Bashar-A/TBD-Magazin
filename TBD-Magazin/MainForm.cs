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
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database = new DbConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkersAndRoles workersAndRoles = new WorkersAndRoles();
            workersAndRoles.Show();
            //Products products = new Products();
            //products.Show();
            //CategoriesAndManufacturers categoriesAndManufacturers = new CategoriesAndManufacturers();
            //categoriesAndManufacturers.Show();
            //Clients clients = new Clients();
            //clients.Show();
        }
    }
}
