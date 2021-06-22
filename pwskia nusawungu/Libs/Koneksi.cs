using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace pwskia_nusawungu.Libs
{
    class Koneksi
    {
        private MySqlConnection con;

        private string host = "localhost";
        private string db = "dbpwskia";
        private string port = "3306";
        private string user = "root";
        private string pass = "root";

        public Koneksi()
        {
            con = new MySqlConnection($"host={host};user={user};password={pass};database={db};port={port}");
        }

        public MySqlConnection GetConnection()
        {
            return con;
        }

        public void Open()
        {
            con.Open();
        }

        public void Close()
        {
            con.Close();
        }
    }
}
