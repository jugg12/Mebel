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
    public partial class ФормаИзмененияИзделий : Form
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
        public ФормаИзмененияИзделий(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФормаИзмененияИзделий_Load(object sender, EventArgs e)
        {
           
            this.callback = mast;
            this.callback1 = naim;
            this.callback2 = zachsum;

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Мастер] as [Мастер], [Наименование] as [Наименование], [Количество] as [Количество], [ЗатраченнаяСумма] as [ЗатраченнаяСумма], [Итого] as [Итого], [ДатаИзготовления] as [ДатаИзготовления], [idЗатраченной] as [idЗатраченной],[idМастера] as [idМастера],[idНаименования] as [idНаименования] from Изделие";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[6].Visible = false;
                advancedDataGridView1.Columns[7].Visible = false;
                advancedDataGridView1.Columns[8].Visible = false;
                advancedDataGridView1.Columns[9].Visible = false;


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [Мастер] from [Мастера]", "Мастер", callback);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Наименование] as [Наименование] from [НаименованиеИзделия]", "Наименование", callback1);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Итого] as [Затраченная Сумма],[Мастер] as [Мастер] from [СуммаЗатрат]", "Затраченная Сумма", callback2);

            form.Show();
        }
        private void mast(string result, int id)
        {
            textBox6.Text = result;
            loId = id;

        }
        private void naim(string result, int id)
        {
            textBox1.Text = result;
            oId = id;

        }
        private void zachsum(string result, int id)
        {
            textBox3.Text = result;
            koId = id;

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
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

                textBox4.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) * 0);
            }
            else
            {
                textBox4.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) * Convert.ToInt32(textBox3.Text));
            }

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
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

            if (textBox3.Text == "" || textBox3.Text == " ")
            {


                textBox4.Text = Convert.ToString(Convert.ToInt32(textBox3.Text) * 0);
            }
            else
            {
                textBox4.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) * Convert.ToInt32(textBox3.Text));
            }

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
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
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
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

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[6].Value);

            cmd.CommandText = "Update Изделие set [Мастер]='" + textBox6.Text + "',[Наименование]=  '" + textBox1.Text + "',[Количество]=  '" + textBox2.Text + "',[ЗатраченнаяСумма]=  '" + textBox3.Text + "',[Итого]=  '" + textBox4.Text + "',[ДатаИзготовления]=  '" + dateTimePicker1.Value + "',[idЗатраченной]= '"+koId+ "',[idМастера]= '" + loId + "',[idНаименования]= '"+oId+"' where id = '" + id + "'";
            await cmd.ExecuteNonQueryAsync();

            load();
            textBox1.Text = " ";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox6.Text = " ";
            dateTimePicker1.Value = Convert.ToDateTime("01,01,1999");
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                textBox3.Text = selectedRow.Cells[3].Value.ToString();
                textBox4.Text = selectedRow.Cells[4].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells[5].Value.ToString());
                textBox6.Text = selectedRow.Cells[0].Value.ToString();
                koId =Convert.ToInt32( selectedRow.Cells[7].Value.ToString());
                loId = Convert.ToInt32(selectedRow.Cells[8].Value.ToString());
                oId = Convert.ToInt32(selectedRow.Cells[9].Value.ToString());

            }
        }
    }
}
