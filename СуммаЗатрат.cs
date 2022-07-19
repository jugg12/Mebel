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
    public partial class СуммаЗатрат : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        
        public СуммаЗатрат(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
           
        }

        private void СуммаЗатрат_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button8.Enabled = false;



            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            load();

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

      

      

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        void cls()
        {
            selectedRow = null;
            

            advancedDataGridView1.ClearSelection();
            button4.Enabled = false;
            button8.Enabled = false;






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
            ФормаДобавленияСуммыЗатрат form = new ФормаДобавленияСуммыЗатрат(this.sqlConnect, this);

            form.Show();
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[8].Value);


            cmd.CommandText = "delete from СуммаЗатрат where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ФормаИзмененияСуммыЗатрат form = new ФормаИзмененияСуммыЗатрат(this.sqlConnect, this);

            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            load();
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
    }
}
