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
    public partial class Накладные : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
       

        int oId = 0;
     
        public Накладные(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
            button4.Enabled = false;
            button8.Enabled = false;
        }

        private void Накладные_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "мебельDataSet3.Накладные". При необходимости она может быть перемещена или удалена.
            this.накладныеTableAdapter.Fill(this.мебельDataSet3.Накладные);



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
                advancedDataGridView1.Columns[7].Visible = false;
                advancedDataGridView1.Columns[8].Visible = false;
                advancedDataGridView1.Columns[9].Visible = false;
                advancedDataGridView1.Columns[10].Visible = false;

            }
        }

       

       
        void cls()
        {
            selectedRow = null;
            
            button4.Enabled = false;
            button8.Enabled = false;

            advancedDataGridView1.ClearSelection();
            oId = -1;
           





        }

        

       

      

       

        

       

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            mainForm.Show(); 
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Справочники form = new Справочники(this.sqlConnect, this);

            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ФормаДобавленияНакладных form = new ФормаДобавленияНакладных(this.sqlConnect, this);

            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ФормаИзмененияНакладных form = new ФормаИзмененияНакладных(this.sqlConnect, this);

            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[7].Value);


            cmd.CommandText = "delete from Накладные where id='" + id + "'";


            await cmd.ExecuteNonQueryAsync();
            load();

            cls();
        }
    }
}
