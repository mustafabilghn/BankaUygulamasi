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

namespace BankaUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-AOEQHQU;Initial Catalog=DbBankaTest;Integrated Security=True");

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("Select * from TblKisiler where HesapNo = @p1 and Sifre = @p2", conn);
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 frm2 = new Form2();
                frm2.hesapno = maskedTextBox1.Text;
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yaptınız.Lütfen Bilgilerinizi Kontrol Ediniz!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
    }
}
