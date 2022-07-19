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
    public partial class ФормаИзмененияКлиентов : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        Action<string, int> callback;
        
        int oId = 0;
        public ФормаИзмененияКлиентов(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФормаИзмененияКлиентов_Load(object sender, EventArgs e)
        {
           
  

            this.callback = status;
           

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;


            load();
        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[ФИО] as [ФИО], [Возраст] as [Возраст] , [КоличествоКупленныхИзделий] as [КоличествоКупленныхИзделий], [Статус] as [Статус],[СерияНомерПаспорта] as [СерияНомерПаспорта],[КемВыдан] as [КемВыдан],[ДатаПолучения] as [ДатаПолучения],[idСкидки] as [idСкидки] from Клиенты";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[8].Visible = false;
                advancedDataGridView1.Columns[7].Visible = false;



            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Статус] as [Статус] from [СкидкаКлиента]", "Статус", callback);
            form.Show();
        }
        private void status(string result, int id)
        {

            textBox4.Text = result;
            oId = id;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[8].Value);

            cmd.CommandText = "Update Клиенты set [ФИО]='" + textBox1.Text + "',[Возраст]=  '" + textBox2.Text + "',[КоличествоКупленныхИзделий]=  '" + textBox3.Text + "',[Статус]=  '" + textBox4.Text + "',[СерияНомерПаспорта]=  '" + textBox5.Text + "',[КемВыдан]=  '" + textBox6.Text + "',[ДатаПолучения]=  '" + dateTimePicker1.Value+ "',[idСкидки]=  '" + oId + "' where id = '" + id + "'";
            await cmd.ExecuteNonQueryAsync();

            load();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox2.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
                textBox4.Text = selectedRow.Cells[3].Value.ToString();
                textBox5.Text = selectedRow.Cells[4].Value.ToString();
                textBox6.Text = selectedRow.Cells[5].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells[6].Value);
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
