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
                        username = (string)reader["username"],
                        nik = (string)reader["nik"],
                        nama = (string)reader["nama"],
                        nip = (string)reader["nip"],
                        super = (string)reader["super"]
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

        public List<Admin> GetAllAdmin(string username="", string nip="")
        {
            int nums = 1;
            List<Admin> admins = new List<Admin>();
            string query;
            if (username == "" && nip == "")
            {
                query = "SELECT * FROM admin";
            }
            else
            {
                query = $"SELECT * FROM admin WHERE username='{username}' OR nip='{nip}'";
            }
            
            try
            {
                Koneksi koneksi = new Koneksi();
                koneksi.Open();
                MySqlCommand cmd = new MySqlCommand(query, koneksi.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    admins.Add(new Admin
                    {
                        nums = nums,
                        nama = (string)reader["nama"],
                        username = (string)reader["username"],
                        nip = (string)reader["nip"],
                        super = (string)reader["super"]

                    });

                    nums++;
                }

                koneksi.Close();

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return admins;
        }
        //
        public void DeleteAdmin(string username)
        {
            string query = $"DELETE FROM admin WHERE username = '{username}'";
            try
            {
                Koneksi con = new Koneksi();
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                cmd.ExecuteReader();
                con.Close();

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Registration new admin
        public void registration(Admin admin)
        {
            string password = EncryptProvider.Sha256(admin.passw);
            string query = $"INSERT INTO admin (nama, nik, username, nip, password, super) VALUES (" +
                $" '{admin.nama}', '{admin.nik}', '{admin.username}', '{admin.nip}', '{password}', '{admin.super}' )";

            try
            {
                Koneksi con = new Koneksi();
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                cmd.ExecuteReader();
                con.Close();

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update super admin
        public void UpdateSuperAdmin(Admin admin)
        {
            string query = $"UPDATE admin SET super='{admin.super}' WHERE username='{admin.username}'";
            try
            {
                Koneksi con = new Koneksi();
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                cmd.ExecuteReader();
                con.Close();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }

        public bool UbahPasswordAdmin(Admin admin, string passLama)
        {
            if(loginAdmin(admin.username, passLama).Any())
            {
                string passEnc = EncryptProvider.Sha256(admin.passw);
                string query = $"UPDATE admin SET password='{passEnc}' WHERE username='{admin.username}'";

                try
                {
                    Koneksi con = new Koneksi();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                    cmd.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                return false;
            }
        }

        public List<Admin> CariAdmin(Admin admin)
        {
            List<Admin> data = new List<Admin>();
            string query = $"SELECT * FROM admin WHERE username='{admin.username}' AND nik='{admin.nik}' ";

            try
            {
                Koneksi con = new Koneksi();
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Admin
                    {
                        nama = (string)reader["nama"]
                    });
                }

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;

        }

        public void LupaPassowrd(Admin admin)
        {
            string passEnc = EncryptProvider.Sha256(admin.passw);
            string query = $"UPDATE admin SET password='{passEnc}' WHERE nik='{admin.nik}' AND username='{admin.username}' ";

            try
            {
                Koneksi con = new Koneksi();
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con.GetConnection());
                cmd.ExecuteReader();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
