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
using Mebel.ФормыДобавления;
using Mebel.ФормыИзменений;

namespace Mebel
{
    public partial class Клиенты : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
       

     
        public Клиенты(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button3.Enabled = false;
            button4.Enabled = false;

        }

        private void Клиенты_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "мебельDataSet21.Клиенты". При необходимости она может быть перемещена или удалена.
            this.клиентыTableAdapter4.Fill(this.мебельDataSet21.Клиенты);






            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

        }
        void cls()
        {
            selectedRow = null;

            button3.Enabled = false;
            button4.Enabled = false;


            advancedDataGridView1.ClearSelection();








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

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[8].Value);


            cmd.CommandText = "delete from Клиенты where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            load();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Справочники form = new Справочники(this.sqlConnect, this);

            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ФормаДобавленияКлиентов form = new ФормаДобавленияКлиентов(this.sqlConnect, this);

            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ФормаИзмененияКлиентов form = new ФормаИзмененияКлиентов(this.sqlConnect, this);

            form.Show();
        }
    }



















    
}


