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

        // Local

        private string host = "localhost";
        private string db = "dbpwskia";
        private string port = "3306";
        private string user = "root";
        private string pass = "";

        // Online
	
	/*
        private string host = "sql6.freemysqlhosting.net";
        private string db = "sql6425927";
        private string port = "3306";
        private string user = "sql6425927";
        private string pass = "PLnVZQqtwx";
	*/

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
