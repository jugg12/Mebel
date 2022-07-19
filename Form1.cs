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
    public partial class Form1 : Form
    {
        SqlConnection sqlConnect = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Клиенты form = new Клиенты(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            sqlConnect = new SqlConnection(@"Data Source=DESKTOP-K1HANH9\SQLEXPRESS;Initial Catalog=" + "Мебель" + ";Integrated Security=True");
            await sqlConnect.OpenAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Изделия form = new Изделия(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Мастера form = new Мастера(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Накладные form = new Накладные(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            СуммаЗатрат form = new СуммаЗатрат(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }

       

        

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Справочники form = new Справочники(this.sqlConnect, this);
            this.Hide();
            form.Show();
        }
    }
}
