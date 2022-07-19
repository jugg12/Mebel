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
    public partial class Изделия : Form
        
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
      
        int oId = 0;
        
        public Изделия(SqlConnection sqlConnect, Form f)
        {
           
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button10.Enabled= false;
            button8.Enabled= false;
           
            

        }

        private void Изделия_Load(object sender, EventArgs e)
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

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                button8.Enabled = true;
                button10.Enabled = true;










            }

        }

     
        void cls()
        {
            selectedRow = null;
            
            button10.Enabled = false;
            button8.Enabled = false;
            
            advancedDataGridView1.ClearSelection();
          
           
          





        }

      

      

   
       

      

       

       

      

      
        

      

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Справочники form = new Справочники(this.sqlConnect, this);
           
            form.Show();
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[6].Value);


            cmd.CommandText = "delete from Изделие where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ФормаДобавленияИзделий form = new ФормаДобавленияИзделий(this.sqlConnect, this);

            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            load();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ФормаИзмененияИзделий form = new ФормаИзмененияИзделий(this.sqlConnect, this);

            form.Show();
        }
    }
}
