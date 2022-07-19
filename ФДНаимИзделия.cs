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
    public partial class ФДНаимИзделия : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФДНаимИзделия(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФДНаимИзделия_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[Наименование] as [Наименование] from НаименованиеИзделия";
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
            if (textBox1.Text == "" || textBox1.Text == " ")
            { MessageBox.Show("Для того, чтобы добавить 'Наименование изделия' требуется заполнить все поля"); }
            else
            {
                
              
                    cmd.CommandText = "INSERT INTO НаименованиеИзделия ([Наименование]) values ('" + textBox1.Text + "')";

                
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Text = "";
            }
        }
    }
}
