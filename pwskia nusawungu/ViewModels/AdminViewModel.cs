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

        public List<Admin> loginAdmin(string username, string password)
        {
            string enPassw = EncryptProvider.Sha256(password);

            List<Admin> admin = new List<Admin>();

            string query = $"SELECT * FROM admin WHERE username='{username}' AND password='{enPassw}'";

            try
            {
                Koneksi koneksi = new Koneksi();

                koneksi.Open();
                MySqlCommand cmd = new MySqlCommand(query, koneksi.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    admin.Add(new Admin
                    {
                        name = (string)reader["nama"],
                        nip = (string)reader["nip"]
                    });
                }

                koneksi.Close();
            }
            catch
            {
                throw new Exception(message: "Silahkan cek koneksi internet!");
            }

            return admin;
        }

        // Registration new admin
        public void registration(string name, string username, string password)
        {

        }
    }
}
