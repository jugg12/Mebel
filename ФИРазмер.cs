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
    public partial class ФИРазмер : Form
    {

        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public ФИРазмер(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФИРазмер_Load(object sender, EventArgs e)
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
                advancedDataGridView1.Columns[3].Visible = false;


            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox2.Text == "0")
            { MessageBox.Show("Для того, чтобы изменить 'Размер листа' требуется выбрать нужный размер и изменить"); }
            else
            {
                int id = Convert.ToInt32(selectedRow.Cells[3].Value);

                cmd.CommandText = "Update РазмерЛиста set [Длина(см)]='" + textBox1.Text + "',[Ширина(см)]=  '" + textBox2.Text + "',[Площадь]=  '" + textBox3.Text + "' where id = '" + id + "'";
                await cmd.ExecuteNonQueryAsync();
                load();
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
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
                textBox2.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox1.Text==" ")
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
            if (textBox3.Text == "" || textBox3.Text == " ")
            {
                textBox3.Text = "0";
            }
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }
    }
}
