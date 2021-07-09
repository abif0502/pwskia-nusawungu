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
            string query = "SELECT DISTINCT tanggal FROM datapwskia";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] tanggal = reader["tanggal"].ToString().Split(' ');
                    tahun.Add(tanggal[3]);
                }

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
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
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
                    $"JOIN daftarJenis " +
                    $"ON datapwskia.idJenis = daftarJenis.id " +
                    $"WHERE daftarJenis.id={idJenis} " +
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
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
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
                    $"JOIN daftarJenis " +
                    $"ON datapwskia.idJenis = daftarJenis.id " +
                    $"WHERE daftarJenis.id={idJenis}";
            }
            else
            {
                query = $"SELECT * FROM datapwskia " +
                    $"JOIN daftarJenis " +
                    $"ON datapwskia.idJenis = daftarJenis.id " +
                    $"WHERE daftarJenis.id={idJenis} AND tanggal LIKE '%{bulanDanTahun}%'";
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
                            nama = (string)reader["desa"],
                            sasaran = new Sasaran
                            {
                                bumil = (Int32)reader["bumil"],
                                bulin = (Int32)reader["bulin"],
                                bumilRisti = (Int32)reader["bumilRisti"]
                            },
                            jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                            jmlBulanIni = (Int32)reader["jmlBulanIni"],
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
                $"'{pwskia.idJenis}'," +
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
                $"idJenis={pwskia.idJenis}," +
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

            }catch(Exception ex)
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
