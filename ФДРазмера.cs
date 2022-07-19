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
namespace Mebel.ФормыДобавления
{
    
    public partial class ФДРазмера : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФДРазмера(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФДРазмера_Load(object sender, EventArgs e)
        {
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;


            load();

        }

        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Длина(см)] as [Длина(см)], [Ширина(см)] as [Ширина(см)],[Площадь] as [Площадь] from РазмерЛиста";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox2.Text == "0")
            { MessageBox.Show("Для того, чтобы добавить 'Размер листа' требуется заполнить все поля"); }
            else
            {
               
               

              

                
               
                    cmd.CommandText = "INSERT INTO РазмерЛиста ([Длина(см)],[Ширина(см)],[Площадь]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";

                
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == " ")
            {
                textBox1.Text = "0";
            }
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == " ")
            {
                textBox2.Text = "0";
            }
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }
    }
}
