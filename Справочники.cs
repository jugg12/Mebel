using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Mebel
{
    public partial class Справочники : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        
        public Справочники(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
           
        }

        private void Справочники_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            СкидкаКлиента form = new СкидкаКлиента(this.sqlConnect, this);
           
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            РазмерЛиста form = new РазмерЛиста(this.sqlConnect, this);
           
            form.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            НаименованиеИзделия form = new НаименованиеИзделия(this.sqlConnect, this);
          
            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show();
        }
    }
}
