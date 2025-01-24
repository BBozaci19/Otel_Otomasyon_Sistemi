using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Otel_Star
{
    public partial class FrmMusteri : Form
    {
        string connectionString = "Server=172.21.54.253;Database=25_132330024;Uid=25_132330024;Pwd=!nif.ogr24BO;";
        private DataTable dt;

        public FrmMusteri()
        {
            InitializeComponent();
        }

        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            LoadMusteriler();
        }

        private void LoadMusteriler()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT musteri_id, musteri_adi, musteri_soyadi, musteri_kimlikno, musteri_telefon FROM musteri";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    datagridviewMusteri.DataSource = dt; 
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void datagridviewMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datagridviewMusteri.Rows[e.RowIndex];
                Txtİd.Text = row.Cells["musteri_id"].Value.ToString();
                Txtİsim.Text = row.Cells["musteri_adi"].Value.ToString();
                TxtSoyisim.Text = row.Cells["musteri_soyadi"].Value.ToString();
                TxtKimlikNo.Text = row.Cells["musteri_kimlikno"].Value.ToString();
                TxtTelefon.Text = row.Cells["musteri_telefon"].Value.ToString();
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txtİd.Text) ||
                string.IsNullOrWhiteSpace(Txtİsim.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyisim.Text) ||
                string.IsNullOrWhiteSpace(TxtTelefon.Text) ||
                string.IsNullOrWhiteSpace(TxtKimlikNo.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            datagridviewMusteri.Rows.Add(
      Txtİd.Text,
      Txtİsim.Text,
      TxtSoyisim.Text,
      TxtTelefon.Text,
      TxtKimlikNo.Text
            );

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO musteri (musteri_id, musteri_adi, musteri_soyadi, musteri_telefon, musteri_kimlikno) " +
                                   "VALUES (@id, @adi, @soyadi, @telefon, @kimlikno)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", Txtİd.Text);
                    cmd.Parameters.AddWithValue("@adi", Txtİsim.Text);
                    cmd.Parameters.AddWithValue("@soyadi", TxtSoyisim.Text);
                    cmd.Parameters.AddWithValue("@telefon", TxtTelefon.Text);
                    cmd.Parameters.AddWithValue("@kimlikno", TxtKimlikNo.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Müşteri başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMusteriler();

                Txtİd.Clear();
                Txtİsim.Clear();
                TxtSoyisim.Clear();
                TxtTelefon.Clear();
                TxtKimlikNo.Clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txtİd.Text))
            {
                MessageBox.Show("Lütfen silmek için bir müşteri seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int musteriId = Convert.ToInt32(Txtİd.Text);
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM musteri WHERE musteri_id = @musteriId";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@musteriId", musteriId);
                    cmd.ExecuteNonQuery();
                }

                LoadMusteriler();
                MessageBox.Show("Müşteri başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txtİd.Text))
            {
                MessageBox.Show("Lütfen güncellenecek müşteriyi seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int musteriId = Convert.ToInt32(Txtİd.Text);
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE musteri SET musteri_adi = @musteriadi, musteri_soyadi = @musterisoyadi, " +
                                   "musteri_kimlikno = @musterikimlikno, musteri_telefon = @musteritelefon WHERE musteri_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@musteri_adi", Txtİsim.Text);
                    cmd.Parameters.AddWithValue("@musteri_soyadi", TxtSoyisim.Text);
                    cmd.Parameters.AddWithValue("@musteri_kimlikno", TxtKimlikNo.Text);
                    cmd.Parameters.AddWithValue("@musteri_telefon", TxtTelefon.Text);
                    cmd.Parameters.AddWithValue("@id", musteriId);
                    cmd.ExecuteNonQuery();
                }

                LoadMusteriler();
                MessageBox.Show("Müşteri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
