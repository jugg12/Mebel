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
    public partial class ФИСкидки : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФИСкидки(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФИСкидки_Load(object sender, EventArgs e)
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
                advancedDataGridView1.Columns[2].Visible = false;


            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == " " || textBox2.Text == " ")
            { MessageBox.Show("Для того, чтобы изменить 'Скидку' требуется выбрать нужный вариант"); }
            else
            {
                int id = Convert.ToInt32(selectedRow.Cells[2].Value);
                cmd.CommandText = "Update СкидкаКлиента set [Скидка]='" + textBox1.Text + "',[Статус]=  '" + textBox2.Text + "' where id = '" + id + "'";
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox2.Text = selectedRow.Cells[1].Value.ToString();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

