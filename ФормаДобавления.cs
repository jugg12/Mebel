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
    public partial class ФормаДобавления : Form
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
        
        public ФормаДобавления(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФормаДобавления_Load(object sender, EventArgs e)
        {
            


            this.callback = izd;
            this.callback1 = kli;
            this.callback2 = dan;

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
           

            load();
        }

        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[ФИО] as [ФИО], [Стаж] as [Стаж], [Изделие] as [Изделие], [КонтрАгенты] as [КонтрАгенты],[СерияНомерПаспорта] as [СерияНомерПаспорта],[КемВыдан] as [КемВыдан],[ДатаПолучения] as [ДатаПолучения]from Мастера";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
             

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Наименование] as [Наименование] from [НаименованиеИзделия]", "Наименование", callback);
            
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [КонтрАгенты] from [Клиенты]", "КонтрАгенты", callback1);
           
            form.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "INSERT INTO Мастера ([ФИО],[Стаж],[Изделие],[КонтрАгенты],[СерияНомерПаспорта],[КемВыдан],[ДатаПолучения]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','"+textBox6.Text+ "','" +dateTimePicker1.Value + "')";


            await cmd.ExecuteNonQueryAsync();
           
            load();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            dateTimePicker1.Value = Convert.ToDateTime("01.01.1999");

        }
        private void izd(string result, int id)
        {
            textBox3.Text = result;
            oId = id;

        }
        private void kli(string result, int id)
        {
            textBox4.Text = textBox4.Text+"  "+result;
            oId = id;

        }
        private void dan(string result, int id)
        {
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != ""  && textBox5.Text != "" && textBox6.Text != "")
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
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""  && textBox5.Text != "" && textBox6.Text != "")
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
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox6.Text != "")
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
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""  && textBox5.Text != "" && textBox6.Text != "")
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

      

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""  && textBox5.Text != "" && textBox6.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
