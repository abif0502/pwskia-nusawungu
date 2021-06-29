using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pwskia_nusawungu.Libs;

namespace pwskia_nusawungu.Models
{
    public class Desa
    {
        public int? id { get; set; }
        public string nama { get; set; }
        public Sasaran sasaran { get; set; }
        public int jmlBulanLalu { get; set; }
        public int jmlBulanIni { get; set; }
        public int r { get; set; }

        private int _abs;
        private float _persentase;

        public int abs
        {
            set { _abs = value; }
            get { return jmlBulanIni + jmlBulanLalu; }
        }

        public float persentase
        {
            set { _persentase = value; }
            get { return (float)abs / sasaran.bumil * 100; }
        }
    }

    public enum EnumDesa
    {
        KARANGTAWANG,
        KARANGPAKIS,
        BANJARSARI,
        JETIS,
        BANJAREJA,
        KARANGSEMBUNG,
        PURWADADI,
        NUSAWANGKAL

    }

    public class DesaContext
    {

        private MySqlConnection con;

        public DesaContext()
        {
            Koneksi koneksi = new Koneksi();
            con = koneksi.GetConnection();
        }

        public string EditSasaran(Desa desa)
        {
            string query = $"UPDATE tbdesa SET bumil={desa.sasaran.bumil}, bulin={desa.sasaran.bulin}, bumilRisti={desa.sasaran.bumilRisti} WHERE nama='{desa.nama}'";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();
                con.Close();
                return "Berhasil mengubah nilai sasaran";
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public List<Desa> GetSasaran()
        {
            List<Desa> dataSasaran = new List<Desa>();

            string query = "SELECT * FROM tbdesa";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataSasaran.Add(new Desa
                    {
                        nama = (string)reader["nama"],
                        sasaran = new Sasaran
                        {
                            bumil = (Int32)reader["bumil"],
                            bulin = (Int32)reader["bulin"],
                            bumilRisti = (Int32)reader["bumilRisti"]
                        }
                    });
                }
                con.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSasaran;
        }

        public List<Desa> GetSasaranPerDesa(string namaDesa)
        {
            List<Desa> dataSasaran = new List<Desa>();

            string query = $"SELECT * FROM tbdesa WHERE nama='{namaDesa}'";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataSasaran.Add(new Desa
                    {
                        id = (Int32)reader["id"],
                        nama = (string)reader["nama"],
                        sasaran = new Sasaran
                        {
                            bumil = (Int32)reader["bumil"],
                            bulin = (Int32)reader["bulin"],
                            bumilRisti = (Int32)reader["bumilRisti"]
                        }
                    });
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSasaran;
        }






        //public List<Desa> GetDesa()
        //{
        //    List<Desa> desa = new List<Desa>();

        //    string query = "SELECT * FROM tbdesa";

        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand(query, con);
        //    MySqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        desa.Add(new Desa {
        //            id = (Int32)reader["id"],
        //            nama = reader["nama"].ToString()
        //        });
        //    }

        //    return desa;
        //}
    }
}
