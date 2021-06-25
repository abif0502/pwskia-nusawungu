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

    public class DesaContext
    {

        private MySqlConnection con;

        public DesaContext()
        {
            Koneksi koneksi = new Koneksi();
            con = koneksi.GetConnection();
        }
        public List<Desa> GetDesa()
        {
            List<Desa> desa = new List<Desa>();

            string query = "SELECT * FROM tbdesa";

            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                desa.Add(new Desa {
                    id = (Int32)reader["id"],
                    nama = reader["nama"].ToString()
                });
            }

            return desa;
        }
    }
}
