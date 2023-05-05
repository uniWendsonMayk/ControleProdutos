using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace produtos
{
    public partial class frmComprar : Form
    {
        public frmComprar()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
        }

     

        private void frmComprar_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Connection.Query(coditem.Text, dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Comprar(Convert.ToInt32(coditem.Text), Convert.ToInt32(qtdItem.Text));
                dataGridView1.Rows.Clear();
                Connection.Query(coditem.Text, dataGridView1);
            }catch(Exception err)
            {
                MessageBox.Show("Ops! parece que houve um erro: " + err.Message);
            }
        }
    }
}
