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
    public partial class ФормаИзменения : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        Action<string, int> callback;
        Action<string, int> callback1;
        Action<string, int> callback2;
        int oId = 0;
        int koId = 0;
        
        public ФормаИзменения(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          
            int id = Convert.ToInt32(selectedRow.Cells[7].Value);
            cmd.CommandText = "Update Мастера set [ФИО]='" + textBox1.Text + "',[Стаж]=  '" + textBox2.Text + "',[Изделие]=  '" + textBox3.Text + "',[КонтрАгенты]=  '" + textBox4.Text + "',[СерияНомерПаспорта]=  '" + textBox5.Text + "',[КемВыдан]= '" + textBox6.Text+ "',[ДатаПолучения]= '" +dateTimePicker1.Value+ "' where id = '" + id + "'";
            await cmd.ExecuteNonQueryAsync();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
           
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            load();
        }

        private void ФормаИзменения_Load(object sender, EventArgs e)
        {
          


            this.callback = izd;
            this.callback1 = kli;
            this.callback2 = dan;
           
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;


            load();
        }
        private void izd(string result, int id)
        {
            textBox3.Text = result;
            oId = id;

        }
        private void dan(string result, int id)
        {
            textBox4.Text = result;
            koId = id;

        }
        private void kli(string result, int id)
        {
            textBox5.Text = textBox5.Text + " " + result;
            oId = id;

        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[ФИО] as [ФИО], [Стаж] as [Стаж], [Изделие] as [Изделие], [КонтрАгенты] as [КонтрАгенты],[СерияНомерПаспорта] as [СерияНомерПаспорта],[КемВыдан] as [КемВыдан],[ДатаПолучения] as [ДатаПолучения]from Мастера";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[7].Visible = false;


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
                textBox4.Text = selectedRow.Cells[3].Value.ToString();
                textBox5.Text = selectedRow.Cells[4].Value.ToString();
                textBox6.Text = selectedRow.Cells[5].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells[6].Value);
                textBox1.Enabled = true;
                textBox2.Enabled = true;
             
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                button1.Enabled = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Наименование] as [Изделие] from [Изделие]", "Изделие", callback);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [КонтрАгенты] from [Клиенты]", "КонтрАгенты", callback1);
            form.Show();
        }

       

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
