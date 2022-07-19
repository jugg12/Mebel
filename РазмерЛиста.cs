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
    public partial class РазмерЛиста : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public РазмерЛиста(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void РазмерЛиста_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "мебельDataSet1.РазмерЛиста". При необходимости она может быть перемещена или удалена.
            this.размерЛистаTableAdapter.Fill(this.мебельDataSet1.РазмерЛиста);

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
         
            button4.Enabled = false;
            button8.Enabled = false;

            advancedDataGridView1.ClearSelection();

           







        }

       

      

       

       

       

       

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[3].Value);


            cmd.CommandText = "delete from РазмерЛиста where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ФДРазмера form = new ФДРазмера(this.sqlConnect, this);

            form.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {

            ФИРазмер form = new ФИРазмер(this.sqlConnect, this);

            form.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }
    }
}
