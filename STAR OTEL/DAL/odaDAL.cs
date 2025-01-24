using MySql.Data.MySqlClient;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel_Star.DAL
{
    internal class odaDAL
    {
        internal class OdaDAL
        {
            private readonly Baglanti _baglanti;

            public OdaDAL()
            {
                _baglanti = new Baglanti();
            }

            public List<Oda> GetirOdalar()
            {
                List<Oda> odalar = new List<Oda>();
                try
                {
                    using (var conn = _baglanti.baglantiGetir())
                    {
                        var query = "SELECT oda_id, oda_numara, oda_tur, oda_fiyat FROM oda";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                odalar.Add(new Oda
                                {
                                    oda_id = reader.GetInt32("oda_id"),
                                    oda_numara = reader.GetString("oda_numara"),
                                    oda_tur = reader.GetString("oda_tur"),
                                    oda_fiyat = reader.GetDecimal("oda_fiyat")
                                });
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Veritabanı hatası: {ex.Message}");
                }
                return odalar;
            }

            public void EkleOda(Oda oda)
            {
                try
                {
                    using (var conn = _baglanti.baglantiGetir())
                    {
                        var query = "INSERT INTO oda (oda_numara, oda_tur, oda_fiyat) VALUES (@numara, @tur, @fiyat)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@numara", oda.oda_numara);
                        cmd.Parameters.AddWithValue("@tur", oda.oda_tur);
                        cmd.Parameters.AddWithValue("@fiyat", oda.oda_fiyat);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Veritabanına oda eklerken bir hata oluştu: {ex.Message}");
                }
            }

            public void SilOda(int odaId)
            {
                try
                {
                    using (var conn = _baglanti.baglantiGetir())
                    {
                        var query = "DELETE FROM oda WHERE oda_id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", odaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Oda silinirken bir hata oluştu: {ex.Message}");
                }
            }
        }
    }

}

