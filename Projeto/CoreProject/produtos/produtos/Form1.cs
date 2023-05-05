namespace produtos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.conectar(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmComprar frm = new frmComprar();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmVender frm = new frmVender();
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Connection.Consulta(textBox1.Text, dataGridView1);
        }
    }
}