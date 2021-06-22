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
                        R = (Int32)reader["r"]
                    });

                    nom++;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


            con.Close();

            return dataKunjungan1;
        }
    }
}
