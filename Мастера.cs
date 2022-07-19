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
    public partial class Мастера : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        

       
        public Мастера(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button3.Enabled = false; 
            button4.Enabled = false; 
            
        }

        private void Мастера_Load(object sender, EventArgs e)
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
                string C = "SELECT id,[ФИО] as [ФИО], [Стаж] as [Стаж], [Изделие] as [Изделие], [КонтрАгенты] as [КонтрАгенты],[СерияНомерПаспорта] as [СерияНомерПаспорта],[КемВыдан] as [КемВыдан],[ДатаПолучения] as [ДатаПолучения]from Мастера";

                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
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

        
        void cls()
        {
            selectedRow = null;
            
            button3.Enabled = false;
            button4.Enabled = false;
            

            advancedDataGridView1.ClearSelection();
            
         
            





        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[7].Value);


            cmd.CommandText = "delete from Мастера where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }

       

        private  void button1_Click(object sender, EventArgs e)
        {
            ФормаДобавления form = new ФормаДобавления(this.sqlConnect, this);

            form.Show();

           
                
               
            
          
            
        }

      

       
       
      

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            ФормаИзменения form = new ФормаИзменения(this.sqlConnect, this);
          
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            load();
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
    }
}
