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
    public partial class ФормаДобавленияНакладных : Form
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
        Action<string, int> callback3;
        Action<string, int> callback4;
        int oId = 0;
        int koId = 0;
        int loId = 0;
        int moId = 0;
        int a=0;
        public ФормаДобавленияНакладных(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void ФормаДобавленияНакладных_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "мебельDataSet3.Накладные". При необходимости она может быть перемещена или удалена.
            this.накладныеTableAdapter.Fill(this.мебельDataSet3.Накладные);
        
           
            this.callback = kli;
            this.callback1 = skid;
            this.callback2 = izd;
            this.callback3 = bez;
          

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[НомерНакладной] as [НомерНакладной], [Дата] as [Дата] , [Клиент] as [Клиент], [СкидкаКлиента] as [СкидкаКлиента], [Изделие] as [Изделие],[СуммаБезСкидок] as [СуммаБезСкидок],[Итого] as [Итого],[idКлиента] as [idКлиента],[idСкидки] as [idСкидки],[idИзделия] as [idИзделия] from Накладные";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void kli(string result, int id)
        {
            textBox2.Text = result;
            oId = id;

        }
       
        private void skid(string result, int id)
        {
            textBox3.Text = result;
            koId = id;

        }
        private void izd(string result, int id)
        {
            textBox4.Text = result;
            loId = id;

        }
        private void bez(string result, int id)
        {
            textBox1.Text = result;
            moId = id;

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "INSERT INTO Накладные ([НомерНакладной],[Дата],[Клиент],[СкидкаКлиента],[Изделие],[СуммаБезСкидок],[Итого],[idКлиента],[idСкидки],[idИзделия]) values ('" + textBox6.Text + "','" + dateTimePicker1.Value + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox1.Text + "','" + textBox5.Text+ "','"+oId+ "','"+koId+ "','"+loId + "')";
            await cmd.ExecuteNonQueryAsync();

            load();
            textBox1.Text = "0";
            textBox2.Text = " ";
            textBox3.Text = "0";
            textBox4.Text = " ";
            textBox6.Text = "000000";
            textBox5.Text = "0";
            button3.Enabled = false;
           
            dateTimePicker1.Value = Convert.ToDateTime("01.01.1999");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[ФИО] as [Клиент] from [Клиенты]", "Клиент", callback);
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Скидка] as [СкидкаКлиента],[Статус] as [Статус] from [СкидкаКлиента]", "СкидкаКлиента", callback1);
            form.Show();
    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Наименование] as [Изделие] from [НаименованиеИзделия]", "Изделие", callback2);
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Выбор form = new Выбор(this.sqlConnect, "select id,[Итого] as [СуммаБезСкидки],[Клиент] as [Клиенту] from [СуммаЗатрат]", "СуммаБезСкидки", callback3);
            form.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            a = Convert.ToInt32(textBox3.Text);
            textBox5.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - (Convert.ToInt32(textBox1.Text) * (a * 0.01)));
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            a = Convert.ToInt32(textBox3.Text);
            textBox5.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - (Convert.ToInt32(textBox1.Text) * (a * 0.01)));
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            a = Convert.ToInt32(textBox3.Text);
            textBox5.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - (Convert.ToInt32(textBox1.Text) * (a * 0.01)));
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }
    }
}
