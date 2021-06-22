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
        Koneksi koneksi = new Koneksi();

        public List<string> GetDesa()
        {
            List<string> desa = new List<string>();

            string query = "SELECT * FROM tbdesa";

            koneksi.Open();
            MySqlCommand cmd = new MySqlCommand(query, koneksi.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                desa.Add(reader["nama"].ToString());
            }

            return desa;
        }
    }
}
