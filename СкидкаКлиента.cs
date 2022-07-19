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
    public partial class СкидкаКлиента : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        
        public СкидкаКлиента(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void СкидкаКлиента_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "мебельDataSet.СкидкаКлиента". При необходимости она может быть перемещена или удалена.
         

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();
        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Скидка] as [Скидка], [Статус] as [Статус] from СкидкаКлиента";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[2].Visible = false;

            }
        }

        

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
               
                button4.Enabled = true;

                button8.Enabled = true;
           
             





            }
        }
        void cls()
        {
            selectedRow = null;
           
            button8.Enabled = false;
            button4.Enabled = false;

            advancedDataGridView1.ClearSelection();

            

            





        }

      

        

        private async void button1_Click(object sender, EventArgs e)
        {
           
        }

       

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[2].Value);


            cmd.CommandText = "delete from СкидкаКлиента where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

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

        private void button9_Click(object sender, EventArgs e)
        {
            ФДСкидок form = new ФДСкидок(this.sqlConnect, this);

            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ФИСкидки form = new ФИСкидки(this.sqlConnect, this);

            form.Show();
        }
    }
}
