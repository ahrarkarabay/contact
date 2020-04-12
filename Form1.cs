/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
** NESNEYE DAYALI PROGRAMLAMA DERSİ
** 2019-2020 BAHAR DÖNEMİ
**
** ÖDEV NUMARASI..........: 4
** ÖĞRENCİ ADI............: MUHAMMED AHRAR KARABAY
** ÖĞRENCİ NUMARASI.......:B161200020
** DERSİN ALINDIĞI GRUP...:
****************************************************************************/
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

namespace b161200020_adresDefteri_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //SQL bağlantısı yapıldı.
        SqlConnection sql_conn = new SqlConnection("Data Source=DESKTOP-FLOAQUI\\SQLEXPRESS;Initial Catalog=CONTACTS; Integrated Security=True");
         private void showData(string sql_select)
         {
            //sql data adaptor ve data grid tanımları yapıldı. 
            SqlDataAdapter da = new SqlDataAdapter(sql_select, sql_conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
         }

        private void btn_show_Click(object sender, EventArgs e)
        {
            //sql sorgusu çalıştırıldı veriler alındı
            showData("Select * From contact ORDER BY AD,SOYAD");
            //id gizlendi.
            this.dataGridView1.Columns["id"].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            // alan kontrol uyarı fonksiyonu alan boş obırakıldığında uyarır.
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox4.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            //kullanıcı tarafından girilen veriler sql e insert edildi.
            sql_conn.Open();
            SqlCommand sql_comm = new SqlCommand("insert into contact (AD,SOYAD,ADRES,TELEFON) values(@AD,@SOYAD,@ADRES,@TELEFON)", sql_conn);
            sql_comm.Parameters.AddWithValue("@AD", textBox1.Text);
            sql_comm.Parameters.AddWithValue("@SOYAD", textBox2.Text);
            sql_comm.Parameters.AddWithValue("@ADRES", textBox3.Text);
            sql_comm.Parameters.AddWithValue("@TELEFON", textBox4.Text);
            sql_comm.ExecuteNonQuery();
            showData("Select * From contact ORDER BY AD,SOYAD");
            this.dataGridView1.Columns["id"].Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // alan kontrol uyarı fonksiyonu alan boş obırakıldığında uyarır.
            if (textBox5.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            //kullanıcı tarafından yızılan ad ile veriler sql den silindi.
            sql_conn.Open();
            SqlCommand sql_comm = new SqlCommand("delete from contact where AD=@AD",sql_conn);
            sql_comm.Parameters.AddWithValue("@AD", textBox5.Text);
            sql_comm.ExecuteNonQuery();
            showData("Select * From contact ORDER BY AD,SOYAD");
            this.dataGridView1.Columns["id"].Visible = false;
            sql_conn.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //key press olarak sadece sayı kabul edilecek fonksiyon yazıldı.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("BİR RAKAM GİRİNİZ.");
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // alan kontrol uyarı fonksiyonu alan boş obırakıldığında uyarır. 
            if (textBox6.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }
            //veri arama ve filitreleme
            sql_conn.Open();
            SqlCommand sql_comm = new SqlCommand("SELECT * FROM contact WHERE AD LIKE '%"+textBox6.Text+"%'", sql_conn);
            SqlDataAdapter da = new SqlDataAdapter(sql_comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            sql_conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //güncelleme için veri çağırma yapıldı.
            int slct_field = dataGridView1.SelectedCells[0].RowIndex;
            string ID = dataGridView1.Rows[slct_field].Cells[0].Value.ToString();
            string AD = dataGridView1.Rows[slct_field].Cells[1].Value.ToString();
            string SOYAD = dataGridView1.Rows[slct_field].Cells[2].Value.ToString();
            string ADRES = dataGridView1.Rows[slct_field].Cells[3].Value.ToString();
            string TELEFON = dataGridView1.Rows[slct_field].Cells[4].Value.ToString();

            
            textBox1.Text = AD;
            textBox2.Text = SOYAD;
            textBox3.Text = ADRES;
            textBox4.Text = TELEFON;
            textBox7.Text = ID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // alan kontrol uyarı fonksiyonu alan boş obırakıldığında uyarır.
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }

            if (textBox4.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Lütfen bu alanı boş bırakmayınız.");
                return;
            }
            //veri güncelleme yordamları yapıldı.
            sql_conn.Open();
            SqlCommand sql_comm = new SqlCommand("update contact set AD='" + textBox1.Text + "',SOYAD='" + textBox2.Text + "',ADRES='" + textBox3.Text + "',TELEFON='" + textBox4.Text + "'where ID="+textBox7.Text, sql_conn);
            sql_comm.ExecuteNonQuery();
            showData("Select * From contact ORDER BY AD,SOYAD");
            this.dataGridView1.Columns["id"].Visible = false;
            sql_conn.Close();
        }
    }
}
