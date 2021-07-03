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
    public class KunjunganViewModel
    {
        private MySqlConnection con;
        private int nums = 1;

        public KunjunganViewModel()
        {
            Koneksi koneksi = new Koneksi();
            con = koneksi.GetConnection();
        }

        public List<string> GetDaftarTahun()
        {
            List<string> tahun = new List<string>();
            string query = "SELECT DISTINCT tanggal FROM kunjungan";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] tanggal = reader["tanggal"].ToString().Split(' ');
                    tahun.Add(tanggal[2]);
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tahun;
        }

        public List<Kunjungan> CariData(string keyword)
        {
            string query = $"SELECT * FROM kunjungan WHERE desa LIKE '%{keyword}%' OR tanggal LIKE '%{keyword}%'";
            List<Kunjungan> kunjungans = new List<Kunjungan>();

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kunjungans.Add(new Kunjungan
                    {
                        //id = (Int32)reader["id"],
                        id = nums,
                        kunjunganKe = (Int32)reader["kunjunganKe"],
                        desa = new Desa
                        {
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
                            r = (Int32)reader["r"],
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

            return kunjungans;
        }



        public int GetJumlahBulanLalu(int kunjunganKe, string namaDesa, string bulanLalu)
        {
            Int32 jmlBulanLalu = 0;
            string query = $"SELECT * FROM kunjungan WHERE desa='{namaDesa}' AND kunjunganKe={kunjunganKe}  AND tanggal LIKE '%{bulanLalu}%'";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    jmlBulanLalu = (Int32)reader["jmlBulanIni"];
                }
                con.Close();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return jmlBulanLalu;
        }

        public List<Kunjungan> GetAllDataKunjungan(int ke, string bulanDanTahun="")
        {
            string query;
            if(bulanDanTahun == "")
            {
                query = $"SELECT * FROM kunjungan WHERE kunjunganKe={ke}";
            }
            else
            {
                query = $"SELECT * FROM kunjungan WHERE kunjunganKe={ke} AND tanggal LIKE '%{bulanDanTahun}'";
            }

            List<Kunjungan> kunjungans = new List<Kunjungan>();

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kunjungans.Add(new Kunjungan
                    {
                        //id = (Int32)reader["id"],
                        id = nums,
                        kunjunganKe = (Int32)reader["kunjunganKe"],
                        desa = new Desa {
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
                            r = (Int32)reader["r"],
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
            
            return kunjungans;
        }

        public string AddDataKunjungan(Kunjungan k)
        {
            string query = $"INSERT INTO kunjungan(`desa`, `kunjunganKe`, `tanggal`, `bumil`, `bulin`, `bumilRisti`, `jmlBulanLalu`, `jmlBulanIni`, `abs`, `persentase`, `r`, `penanggungJawab`)" +
                $"VALUES " +
                $"('{k.desa.nama}'," +
                $"'{k.kunjunganKe}'," +
                $"'{k.tanggal}'," +
                $"{k.desa.sasaran.bumil}," +
                $"{k.desa.sasaran.bulin}," +
                $"{k.desa.sasaran.bumilRisti}," +
                $"{k.desa.jmlBulanLalu}," +
                $"{k.desa.jmlBulanIni}," +
                $"{k.desa.abs}," +
                $"{k.desa.persentase}," +
                $"{k.desa.r}," +
                $"'{k.penanggungJawab}')";

            string message;
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();

                message = "Berhasil memasukkan data!";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return message;
        }

















        /*
        public List<Kunjungan1Model> GetDataKunjunganByArgs(string tanggal = "", string desa = "")
        {
            List<Kunjungan1Model> dataKunjungan1 = new List<Kunjungan1Model>();
            string query;

            if (tanggal == "")
            {
                query = $"SELECT * FROM `kunjungan1perdesa` WHERE nama='{desa}'";
            }else if (desa == "")
            {
                query = $"SELECT * FROM tbkunjungan1 WHERE tanggal='{tanggal}'";
            }else if(desa != "" && tanggal != "")
            {
                query = $"SELECT * FROM `kunjungan1perdesa` WHERE tanggal='{tanggal}' AND nama='{desa}'";
            }
            else
            {
                query = null;
            }

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                int nom = 1;
                while (reader.Read())
                {
                    dataKunjungan1.Add(new Kunjungan1Model
                    {
                        idRecord = nom,
                        tanggal = (string)reader["tanggal"],
                        jumlahBulanLalu = (Int32)reader["jmlBulanLalu"],
                        jumlahBulanIni = (Int32)reader["jmlBulanIni"],
                        abs = (Int32)reader["abs"],
                        persentase = (double)reader["persentase"],
                        R = (Int32)reader["r"],
                        penanggungJawab = (string)reader["penanggungJawab"]
                    });

                    nom++;
                } 
                con.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataKunjungan1;
        }

        public string UbahDataPerBulan(Kunjungan1Model kj1)
        {
            Int32 idDesa = 0;
            DesaContext desaContext = new DesaContext();

            foreach(Desa desa in desaContext.GetDesa())
            {
                if(kj1.desa.nama == desa.nama)
                {
                    idDesa = (Int32)desa.id;
                }
            }

            string query = $"INSERT INTO tbkunjungan1(`idDesa`, `tanggal`, `jmlBulanLalu`, `jmlBulanIni`, `abs`, `persentase`, `r`, `penanggungJawab`) VALUES " +
                $"({idDesa}, " +
                $"'{kj1.tanggal}', " +
                $"{kj1.jumlahBulanLalu}, " +
                $"{kj1.jumlahBulanIni}, " +
                $"{kj1.abs}, " +
                $"{kj1.persentase}, " +
                $"{kj1.R}, " +
                $"'{kj1.penanggungJawab}')";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.ExecuteReader();

                con.Close();

                return "Sukses mengubah data!";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        */
    }
}
