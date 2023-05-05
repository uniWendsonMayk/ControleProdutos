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
    public partial class frmVender : Form
    {
        public frmVender()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void frmVender_Load(object sender, EventArgs e)
        {
            Connection.conectar(dataGridView1);
        }

        private void coditem_TextChanged(object sender, EventArgs e)
        {
            if(coditem.Text != "")
            {
                dataGridView1.Rows.Clear();
                Connection.Query(coditem.Text, dataGridView1);
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
               Connection.Vender(Convert.ToInt32(coditem.Text), Convert.ToInt32(qtdItem.Text));
               dataGridView1.Rows.Clear();
               Connection.Query(coditem.Text, dataGridView1);
            }
            catch(Exception err)
            {
                MessageBox.Show("Ops! parece que houve um erro: " + err.Message);
            }
        }
    }
}
