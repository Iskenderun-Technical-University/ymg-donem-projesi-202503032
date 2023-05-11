using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GİO
{
    public partial class Form7 : Form
    {
        SqlDataAdapter da;
        DataSet ds;
        public static string SqlCon = "Data Source = DESKTOP-3BUBNE9\\SQLEXPRESS; Initial Catalog = GİO; Integrated Security = TRUE";
        SqlConnection con = new SqlConnection(SqlCon);

        public Form7()
        {
            InitializeComponent();
        }
        void Müşteri()
        {
            con.Open();
            da = new SqlDataAdapter("select *from Müşteri", con);
            ds = new DataSet();
            da.Fill(ds, "Müşteri");
            dataGridView1.DataSource = ds.Tables["Müşteri"];
        }

            private void button4_Click(object sender, EventArgs e)
        {
            string kayit = "SELECT * FROM Müşteri WHERE Ad LIKE '%' + @Ad + '%' ";
            SqlCommand komut = new SqlCommand(kayit, con);
            komut.Parameters.AddWithValue("@Ad", textBox8.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            if (textBox8.Text == "") Müşteri();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Müşteri (ID,CariTür,Ad,Bakiye) values (@p1,@p2,@p3,@p4)", con);
            cmd.Parameters.AddWithValue("@p1", textBox7.Text);
            cmd.Parameters.AddWithValue("@p2", textBox1.Text);
            cmd.Parameters.AddWithValue("@p3", textBox5.Text);
            cmd.Parameters.AddWithValue("@p4", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Müşteri();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Müşteri where ID= @p1", con);
            cmd.Parameters.AddWithValue("@p1",textBox7.Text );
            cmd.ExecuteNonQuery();
            con.Close();
            Müşteri();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Müşteri set CariTür=@p2,Ad=@p3,Bakiye=@p4 where ID=@p1", con);
            cmd.Parameters.AddWithValue("@p1", textBox7.Text);
            cmd.Parameters.AddWithValue("@p2", textBox1.Text);
            cmd.Parameters.AddWithValue("@p3", textBox5.Text);
            cmd.Parameters.AddWithValue("@p4", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Müşteri() ;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Müşteri();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
