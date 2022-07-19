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
    public partial class НаименованиеИзделия : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        public НаименованиеИзделия(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void НаименованиеИзделия_Load(object sender, EventArgs e)
        {
            

            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Наименование] as [Наименование] from НаименованиеИзделия";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[1].Visible = false;

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

       

        

     

        

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Справочники form = new Справочники(this.sqlConnect, this);

            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ФДНаимИзделия form = new ФДНаимИзделия(this.sqlConnect, this);

            form.Show();
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[1].Value);


            cmd.CommandText = "delete from НаименованиеИзделия where id='" + id + "'";


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

        private void button7_Click(object sender, EventArgs e)
        {
            ФИНаимИзделия form = new ФИНаимИзделия(this.sqlConnect,this);
            form.Show();

        }
    }
}
