using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwskia_nusawungu.Models;
using pwskia_nusawungu.Libs;
using MySql.Data.MySqlClient;


namespace pwskia_nusawungu.ViewModels.PWSKIA
{
    public class PwskiaViewModel
    {
        private MySqlConnection con;
        private int nums = 1;

        public PwskiaViewModel()
        {
            Koneksi koneksi = new Koneksi();
            con = koneksi.GetConnection();
        }

        public List<string> GetDaftarTahun()
        {
            List<string> tahun = new List<string>();
            string query = "SELECT DISTINCT SUBSTRING_INDEX(tanggal,' ',-1) FROM datapwskia";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tahun.Add((string)reader.GetString(0));
                }

                con.Close();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tahun;
        }

        public List<Pwskia> CariData(string keyword)
        {
            string query = $"SELECT * FROM datapwskia " +
                $"JOIN daftarjenis " +
                $"ON daftarjenis.id = datapwskia.idJenis " +
                $"WHERE " +
                $"desa LIKE '%{keyword}%' " +
                $"OR " +
                $"tanggal LIKE '%{keyword}%'" +
                $"OR " +
                $"daftarjenis.jenis LIKE '{keyword}%'";

            List<Pwskia> dataPwskia = new List<Pwskia>();

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataPwskia.Add(new Pwskia
                    {
                        id = (Int32)reader["id"],
                        numRow = nums,

                        // jenis = k1,k4, dst....
                        jenis = (string)reader["jenis"],
                        desa = new Desa
                        {
                            idJenis = (Int32)reader["idJenis"],
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
                            persentase = (float)reader["persentase"]
                        },
                        tanggal = (string)reader["tanggal"],
                        penanggungJawab = (string)reader["penanggungJawab"]
                    });

                    nums++;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataPwskia;
        }



        public int GetJumlahBulanLalu(int idJenis, string namaDesa, string bulanLalu)
        {
            int jumlahBulanLalu = 0;
            string query = $"SELECT * FROM datapwskia WHERE desa='{namaDesa}' AND idJenis={idJenis}  AND tanggal LIKE '%{bulanLalu}%'";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    jumlahBulanLalu = (Int32)reader["jmlBulanIni"];
                }
                con.Close();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return jumlahBulanLalu;
        }

        public List<Pwskia> GetSingleDataPwsKia(int idJenis, string bulanDanTahun, string desa)
        {
            string query = $"SELECT * FROM datapwskia " +
                    $"JOIN daftarjenis " +
                    $"ON datapwskia.idJenis = daftarjenis.id " +
                    $"WHERE daftarjenis.id={idJenis} " +
                    $"AND tanggal LIKE '%{bulanDanTahun}%' " +
                    $"AND desa='{desa}'";

            List<Pwskia> dataPwskia = new List<Pwskia>();

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataPwskia.Add(new Pwskia
                    {
                        id = (Int32)reader["id"],
                        numRow = nums,
                        jenis = (string)reader["jenis"],
                        desa = new Desa
                        {
                            idJenis = (Int32)reader["idJenis"],
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
                            persentase = (float)reader["persentase"]
                        },
                        tanggal = (string)reader["tanggal"],
                        penanggungJawab = (string)reader["penanggungJawab"]
                    });

                    nums++;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataPwskia;
        }

        public List<Pwskia> GetAllDataPwskia(int idJenis, string bulanDanTahun="")
        {
            string query;
            if(bulanDanTahun == "")
            {
                query = $"SELECT * FROM datapwskia " +
                    $"JOIN daftarjenis " +
                    $"ON datapwskia.idJenis = daftarjenis.id " +
                    $"WHERE daftarjenis.id={idJenis} ";
            }
            else
            {
                query = $"SELECT * FROM datapwskia " +
                    $"JOIN daftarjenis " +
                    $"ON datapwskia.idJenis = daftarjenis.id " +
                    $"WHERE daftarjenis.id={idJenis} AND tanggal LIKE '%{bulanDanTahun}%'";
            }

            List<Pwskia> dataPwskia = new List<Pwskia>();

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataPwskia.Add(new Pwskia
                    {
                        id = (Int32)reader["id"],
                        numRow = nums,
                        jenis = (string)reader["jenis"],
                        desa = new Desa {
                            idJenis = (Int32)reader["idJenis"],
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
                            persentase = (float)reader["persentase"]
                        },
                        tanggal = (string)reader["tanggal"],
                        penanggungJawab = (string)reader["penanggungJawab"]
                    });

                    nums++;
                }
                
                con.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return dataPwskia;
        }

        public string AddDataPwskia(Pwskia pwskia)
        {
            string query = $"INSERT INTO datapwskia(`desa`, `idJenis`, `tanggal`, `bumil`, `bulin`, `bumilRisti`, `jmlBulanLalu`, `jmlBulanIni`, `abs`, `persentase`, `penanggungJawab`)" +
                $"VALUES " +
                $"('{pwskia.desa.nama}'," +
                $"'{pwskia.desa.idJenis}'," +
                $"'{pwskia.tanggal}'," +
                $"{pwskia.desa.sasaran.bumil}," +
                $"{pwskia.desa.sasaran.bulin}," +
                $"{pwskia.desa.sasaran.bumilRisti}," +
                $"{pwskia.desa.jmlBulanLalu}," +
                $"{pwskia.desa.jmlBulanIni}," +
                $"{pwskia.desa.abs}," +
                $"{pwskia.desa.persentase}," +
                $"'{pwskia.penanggungJawab}')";

            string message;
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();

                message = "Berhasil memasukkan data!";

                con.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
        }

        public string EditDataPWSKIA(Pwskia pwskia)
        {
            string message = "";
            string query = $"UPDATE datapwskia SET " +
                $"desa='{pwskia.desa.nama}', " +
                $"idJenis={pwskia.desa.idJenis}," +
                $"bumil={pwskia.desa.sasaran.bumil}," +
                $"bulin={pwskia.desa.sasaran.bulin}," +
                $"bumilristi={pwskia.desa.sasaran.bumilRisti}," +
                $"jmlBulanLalu={pwskia.desa.jmlBulanLalu}," +
                $"jmlBulanIni={pwskia.desa.jmlBulanIni}," +
                $"abs={pwskia.desa.abs}," +
                $"persentase={pwskia.desa.persentase}" +
                $"WHERE id={pwskia.id}";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();
                message = "Berhasil Memperbaharui Data";

                con.Close();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
        }

        public string DeleteDataRecord(int id)
        {
            string message = "";
            string query = $"DELETE FROM datapwskia WHERE id={id}";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();

                message = "Berhasil menghapus data";
                con.Close();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
            
        }
    }
}
