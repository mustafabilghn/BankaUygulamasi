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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-AOEQHQU;Initial Catalog=DbBankaTest;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from TblKisiler where HesapNo = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", maskedTextBox3.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Girilen Hesap Numarası Sisteme Kayıtlıdır.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            conn.Close();

            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Insert into TblKisiler (Ad,Soyad,TC,Telefon,HesapNo,Sifre) values(@p1,@p2,@p3,@p4,@p5,@p6)", conn);
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@p4", maskedTextBox2.Text);
                komut.Parameters.AddWithValue("@p5", maskedTextBox3.Text);
                komut.Parameters.AddWithValue("@p6", textBox3.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Müşteri Bilgileri Sisteme Başarıyla Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(100000, 1000000);
            maskedTextBox3.Text = sayi.ToString();
        }
    }
}
