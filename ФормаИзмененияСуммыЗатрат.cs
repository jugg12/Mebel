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
    public partial class ФормаИзмененияСуммыЗатрат : Form
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
        public ФормаИзмененияСуммыЗатрат(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФормаИзмененияСуммыЗатрат_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[Мастер] as [Мастер], [Длина(см)] as [Длина(см)] , [Ширина(см)] as [Ширина(см)], [РазмерЛиста] as [РазмерЛиста], [СтоимостьЛиста] as [СтоимостьЛиста],[КоличествоЛистов] as [КоличествоЛистов],[Клиент] as [Клиент],[Итого] as [Итого],[idМастера] as [idМастера],[idРазмера] as [idРазмера],[idКлиента] as [idКлиента] from СуммаЗатрат";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[8].Visible = false;
                advancedDataGridView1.Columns[9].Visible = false;
                advancedDataGridView1.Columns[10].Visible = false;
                advancedDataGridView1.Columns[11].Visible = false;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[8].Value);

            cmd.CommandText = "Update СуммаЗатрат set [Мастер]='" + textBox1.Text + "',[Длина(см)]=  '" + textBox2.Text + "',[Ширина(см)]=  '" + textBox3.Text + "',[РазмерЛиста]=  '" + textBox4.Text + "',[СтоимостьЛиста]=  '" + textBox5.Text + "',[КоличествоЛистов]=  '" + textBox6.Text + "',[Клиент]=  '" + textBox7.Text + "',[Итого]=  '" + textBox8.Text + "',[idМастера]=  '" + oId+ "',[idРазмера]=  '" + koId+ "',[idКлиента]=  '" + loId+ "' where id = '" + id + "'";
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
           
            textBox2.Enabled = false;
            textBox3.Enabled = false;
          
            textBox5.Enabled = false;
            textBox6.Enabled = false;
           
            textBox8.Enabled = false;
           
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = advancedDataGridView1.Rows[e.RowIndex];
            textBox1.Text = selectedRow.Cells[0].Value.ToString();
            textBox2.Text = selectedRow.Cells[1].Value.ToString();
            textBox3.Text = selectedRow.Cells[2].Value.ToString();
            textBox4.Text = selectedRow.Cells[3].Value.ToString();
            textBox5.Text = selectedRow.Cells[4].Value.ToString();
            textBox6.Text = selectedRow.Cells[5].Value.ToString();
            textBox7.Text = selectedRow.Cells[6].Value.ToString();
            textBox8.Text = selectedRow.Cells[7].Value.ToString();
          
            oId = Convert.ToInt32( selectedRow.Cells[9].Value.ToString());
            koId = Convert.ToInt32(selectedRow.Cells[10].Value.ToString());
            loId = Convert.ToInt32(selectedRow.Cells[11].Value.ToString());


            textBox2.Enabled = true;
            textBox3.Enabled = true;
           
            textBox5.Enabled = true;
            textBox6.Enabled = true;
           
            textBox8.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [ФИО Мастера] from [Мастера]", "ФИО Мастера", callback2);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Площадь] as [Размер листа(см2)],[Длина(см)] as [Длина(см)], [Ширина(см)] as [Ширина(см)] from [РазмерЛиста]", "Размер листа(см2)", callback);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [Клиент] from [Клиенты]", "Клиент", callback1);
            form.Show();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == " ")
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
            textBox6.Text = Convert.ToString((Convert.ToDouble(textBox2.Text) * Convert.ToDouble(textBox3.Text)) / Convert.ToDouble(textBox4.Text));
        }
    }
}
