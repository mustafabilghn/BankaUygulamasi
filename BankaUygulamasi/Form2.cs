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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-AOEQHQU;Initial Catalog=DbBankaTest;Integrated Security=True");
        void liste()
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("Select * from TblKisiler where HesapNo != @p1", conn);
            komut.Parameters.AddWithValue("@p1", hesapno);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "HesapNo";
            comboBox1.DataSource = dt;
            conn.Close();
        }

        public string hesapno;

        void bakiye()
        {
            conn.Open();
            SqlCommand command = new SqlCommand("Select Bakiye from TblHesap inner join TblKisiler kisi\t\r\non\r\nkisi.HesapNo = TblHesap.HesapNo\r\nwhere kisi.HesapNo = @k1", conn);
            command.Parameters.AddWithValue("@k1", hesapno);
            SqlDataReader dr2 = command.ExecuteReader();
            while (dr2.Read())
            {
                label12.Text = dr2[0].ToString();
            }
            conn.Close();
        }

        void hareketler()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into TblHareket (Gonderen,Alici,Tutar) values (@p1,@p2,@p3)", conn);
            cmd.Parameters.AddWithValue("@p1", hesapno);
            cmd.Parameters.AddWithValue("@p2", comboBox1.Text);
            cmd.Parameters.AddWithValue("@p3", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void gelen()
        {
            SqlCommand cmd = new SqlCommand("Select Ad + ' ' + Soyad as 'Gönderen',Tutar from TblHareket inner join TblKisiler\r\non\r\nTblKisiler.HesapNo = TblHareket.Gonderen\r\n where Alici = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", hesapno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void gonderilen()
        {
            SqlCommand cmd = new SqlCommand("Select Ad + ' ' + Soyad as 'Alici',Tutar from TblHareket inner join TblKisiler\r\n\ton\r\n\tTblKisiler.HesapNo = TblHareket.Alici where Alici = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            label6.Text = hesapno;
            bakiye();
            liste();

            conn.Open();
            SqlCommand komut = new SqlCommand("Select * from TblKisiler where HesapNo = @p1", conn);
            komut.Parameters.AddWithValue("@p1", hesapno);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label5.Text = dr[1] + " " + dr[2];
                label7.Text = dr[4].ToString();
                label8.Text = dr[3].ToString();
            }
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("Update TblHesap set Bakiye=Bakiye+@p1 where HesapNo=@p2", conn);
            komut.Parameters.AddWithValue("@p1", decimal.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@p2", comboBox1.Text);
            komut.ExecuteNonQuery();
            conn.Close();
           
            conn.Open();
            SqlCommand komut2 = new SqlCommand("Update TblHesap set Bakiye=Bakiye-@k1 where HesapNo=@k2", conn);
            komut2.Parameters.AddWithValue("@k1", decimal.Parse(textBox1.Text));
            komut2.Parameters.AddWithValue("@k2", hesapno);
            komut2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("İşleminiz Gerçekleştirildi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bakiye();
            hareketler();
            gelen();
            gonderilen();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Size = new Size(458, 479);
                radioButton1.Checked = false;
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Size = new Size(882, 479);
                radioButton2.Checked = false;
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                gelen();
                gonderilen();
            }
            
        }
    }
}
