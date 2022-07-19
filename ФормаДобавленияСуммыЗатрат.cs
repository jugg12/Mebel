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
    public partial class ФормаДобавленияСуммыЗатрат : Form
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
        int loId = 0;
        int a = 0;
        

        public ФормаДобавленияСуммыЗатрат(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            
        }

        private void ФормаДобавленияСуммыЗатрат_Load(object sender, EventArgs e)
        {
            this.callback = razmerlista;

            this.callback1 = kli;
            this.callback2 = mast;


            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();


        }
        private void kli(string result, int id)
        {
            textBox7.Text = result;
            loId = id;

        }
        private void razmerlista(string result, int id)
        {
            textBox4.Text = result;
            koId = id;

        }
        private void mast(string result, int id)
        {
            textBox1.Text = result;
            oId = id;

        }


        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Мастер] as [Мастер], [Длина(см)] as [Длина(см)] , [Ширина(см)] as [Ширина(см)], [РазмерЛиста] as [РазмерЛиста], [СтоимостьЛиста] as [СтоимостьЛиста], [КоличествоЛистов] as [КоличествоЛистов], [Клиент] as [Клиент], [Итого] as [Итого], [idМастера] as [idМастера], [idРазмера] as [idРазмера], [idКлиента] as [idКлиента] from СуммаЗатрат";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
            

            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text == "" || textBox8.Text == " ")
            {
                textBox8.Text = "0";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox8.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox6.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "INSERT INTO СуммаЗатрат ([Мастер],[Длина(см)],[Ширина(см)],[РазмерЛиста],[СтоимостьЛиста],[КоличествоЛистов],[Клиент],[Итого],[idМастера],[idРазмера],[idКлиента]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + oId + "','" + koId + "','" +loId +"')";
            await cmd.ExecuteNonQueryAsync();
            load();
           
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "100";
            textBox5.Text = "0";
            textBox1.Text = "";
            textBox6.Text = "0";
            textBox7.Text = "";
            textBox8.Text = "0";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Площадь] as [Площадь],[Длина(см)] as [Длина(см)], [Ширина(см)] as [Ширина(см)] from [РазмерЛиста]", "Площадь", callback);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [Клиент] from [Клиенты]", "Клиент", callback1);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [ФИО Мастера] from [Мастера]", "ФИО Мастера", callback2);
            form.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text=="" || textBox2.Text==" ")
            {
                textBox2.Text = "0";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox6.Text = Convert.ToString((Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox3.Text)) / Convert.ToDouble(textBox4.Text));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox3.Text == " ")
            {
                textBox3.Text = "0";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox6.Text = Convert.ToString((Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox3.Text)) / Convert.ToDouble(textBox4.Text));
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox4.Text == " ")
            {
                textBox4.Text = "100";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox6.Text = Convert.ToString((Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox3.Text)) / Convert.ToDouble(textBox4.Text));
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox5.Text == " ")
            {
                textBox5.Text = "0";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox8.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox6.Text));
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text == " ")
            {
                textBox6.Text = "0";
            }
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            textBox8.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox6.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "0" && textBox3.Text != "0" && textBox4.Text != "0" && textBox5.Text != "0" && textBox6.Text != "0" && textBox7.Text != "" && textBox8.Text != "0")
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
