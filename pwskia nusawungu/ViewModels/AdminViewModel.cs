using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using pwskia_nusawungu.Models;
using pwskia_nusawungu.Libs;
using MySql.Data.MySqlClient;
using NETCore.Encrypt;

namespace pwskia_nusawungu.ViewModels
{
    class AdminViewModel
    {

        public AdminViewModel()
        {
            
        }

        // retrieving data from database

        public string loginAdmin(string username, string password)
        {
            string enPassw = EncryptProvider.Sha256(password);
            string name = null;

            string query = $"SELECT * FROM admin WHERE username='{username}' AND password='{enPassw}'";

            try
            {
                Koneksi koneksi = new Koneksi();

                koneksi.Open();
                MySqlCommand cmd = new MySqlCommand(query, koneksi.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = (string)reader["nama"];
                }

                koneksi.Close();
            }
            catch
            {
                throw new Exception(message: "Silahkan cek koneksi internet!");
            }

            return name;
        }

        // Registration new admin
        public void registration(string name, string username, string password)
        {

        }
    }
}
