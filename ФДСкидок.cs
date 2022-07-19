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
    public partial class ФДСкидок : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФДСкидок(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФДСкидок_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[Скидка] as [Скидка], [Статус] as [Статус] from СкидкаКлиента";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);


            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == " " || textBox2.Text == " ")
            { MessageBox.Show("Для того, чтобы добавить 'Скидку' требуется заполнить все поля"); }
            else
            {
                                             
             
                    cmd.CommandText = "INSERT INTO СкидкаКлиента ([Скидка],[Статус]) values ('" + textBox1.Text + "','" + textBox2.Text + "')";

               
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Text = "";
                textBox2.Text = "";

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}




