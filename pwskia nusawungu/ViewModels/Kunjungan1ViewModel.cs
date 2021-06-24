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
    public class Kunjungan1ViewModel
    {
        private MySqlConnection con;

        public Kunjungan1ViewModel()
        {
            Koneksi koneksi = new Koneksi();
            con = koneksi.GetConnection();
        }

        public List<Kunjungan> getDataKunjunganSatu()
        {
            List<Kunjungan> kunjungans = new List<Kunjungan>();
            string query = "SELECT * FROM tbKunjungan WHERE kunjunganKe=1";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kunjungans.Add(new Kunjungan
                    {
                        id = (Int32)reader["idRecord"],
                        desa = (string)reader["desa"],
                        bulan = (string)reader["bulan"],
                        jmlBulanLalu = (Int32)reader["jmlBulanLalu"],
                        jmlBulanIni = (Int32)reader["jmlBulanIni"],
                        r = (Int32)reader["r"],
                        abs = (Int32)reader["abs"],
                        persentase = (double)reader["persentase"],
                        penanggungJawab = (string)reader["penanggungJawab"]
                    });
                }
                con.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return kunjungans;
        }

        public string AddDataKunjunganSatu(Kunjungan k)
        {
            string query = $"INSERT INTO kunjungan(`desa`, `kunjunganKe`, `bulan`, `jmlBulanLalu`, `jmlBulanIni`, `abs`, `persentase`, `r`, `penanggungJawab`)" +
                $"VALUES " +
                $"('{k.desa}'," +
                $"'{k.kunjunganKe}'," +
                $"'{k.bulan}'," +
                $"{k.jmlBulanLalu}," +
                $"{k.jmlBulanIni}," +
                $"{k.abs}," +
                $"{k.persentase}," +
                $"{k.r}," +
                $"'{k.penanggungJawab}')";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();
                con.Close();

                return "Berhasil menambahkan data!";
            }catch(Exception ex)
            {
                return "";
            }
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
