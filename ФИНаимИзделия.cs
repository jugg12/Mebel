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

namespace Mebel.ФормыИзменений
{
    public partial class ФИНаимИзделия : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФИНаимИзделия(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФИНаимИзделия_Load(object sender, EventArgs e)
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
                advancedDataGridView1.Columns[1].Visible = false;


            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||textBox1.Text == " " )
            { MessageBox.Show("Для того, чтобы изменить 'Наименование изделия' требуется заполнить все поля"); }
            else
            {
                int id = Convert.ToInt32(selectedRow.Cells[1].Value);

                cmd.CommandText = "Update НаименованиеИзделия set [Наименование]='" + textBox1.Text + "' where id = '" + id + "'";
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Enabled = false;
                textBox1.Text = "";
               
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
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox1.Enabled = true;
                

            }
        }
    }
}
