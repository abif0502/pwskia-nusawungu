using MySql.Data.MySqlClient;
using pwskia_nusawungu.Libs;
using System;
using System.Collections.Generic;

namespace pwskia_nusawungu.Models
{
    public class Desa
    {
        public int? id { get; set; }
        public int idJenis { get; set; }
        public string nama { get; set; }
        public Sasaran sasaran { get; set; }
        public int jmlBulanLalu { get; set; }
        public int jmlBulanIni { get; set; }

        public int rank { get; set; }

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
            get {
                if(_persentase == 0)
                {
                    switch (idJenis)
                    {
                        case 1:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                        case 2:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                        case 3:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                        case 4:
                            _persentase = (float)abs / sasaran.bulin * 100;
                            break;
                        case 5:
                            _persentase = (float)abs / sasaran.bulin * 100;
                            break;
                        case 6:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                        case 7:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                        default:
                            _persentase = (float)abs / sasaran.bumil * 100;
                            break;
                    }
                }
                return _persentase;
            }
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

        public List<string> GetTahun()
        {
            List<string> tahun = new List<string>();
            string query = $"SELECT DISTINCT tahun FROM tbsasaran";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tahun.Add((string)reader["tahun"]);
                }
                con.Close();

                return tahun;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int GetIdDesa(string nama)
        {
            int id = 0;
            string query = $"SELECT * FROM tbdesa WHERE nama='{nama}'";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = (Int32) reader["id"];
                }
                con.Close();

                return id;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    

        public string EditSasaran(Desa desa)
        {
            string query = $"UPDATE tbsasaran SET bumil={desa.sasaran.bumil}, bulin={desa.sasaran.bulin}, bumilRisti={desa.sasaran.bumilRisti} WHERE iddesa='{desa.id}' AND tahun='{desa.sasaran.tahun}'";

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

        public string TambahSasaran(Desa desa)
        {
            string query = $"INSERT INTO tbsasaran (iddesa,tahun,bumil,bulin,bumilristi) VALUES " +
                $"('{desa.id}', '{desa.sasaran.tahun}', '{desa.sasaran.bumil}'," +
                $"'{desa.sasaran.bulin}', '{desa.sasaran.bumilRisti}')";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();
                return "Berhasil menambah data sasaran";
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Desa> GetSasaran()
        {
            int nomor = 1;
            string query = $"SELECT * FROM tbsasaran JOIN tbdesa ON tbsasaran.iddesa = tbdesa.id";
            List<Desa> dataSasaran = new List<Desa>();
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataSasaran.Add(new Desa
                    {
                        id = nomor,
                        nama = (string)reader["nama"],
                        sasaran = new Sasaran
                        {
                            tahun = (string)reader["tahun"],
                            bumil = (Int32)reader["bumil"],
                            bulin = (Int32)reader["bulin"],
                            bumilRisti = (Int32)reader["bumilRisti"]
                        }
                    });

                    nomor++;
                }
                con.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSasaran;
        }

        public List<Desa> GetSasaranPerTahun(string tahun="", string desa="")
        {
            int nomor = 1;
            string query;
            List<Desa> dataSasaran = new List<Desa>();

            if(desa == "")
            {
                query = $"SELECT * FROM tbsasaran JOIN tbdesa ON tbsasaran.iddesa = tbdesa.id WHERE tahun = '{tahun}'";
            }
            else
            {
                query = $"SELECT * FROM tbsasaran JOIN tbdesa ON tbsasaran.iddesa = tbdesa.id WHERE tahun='{tahun}' AND nama = '{desa}'";
            }

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataSasaran.Add(new Desa
                    {
                        //id = (Int32)reader["id"],
                        id = nomor,
                        nama = (string)reader["nama"],
                        sasaran = new Sasaran
                        {
                            tahun = (string)reader["tahun"],
                            bumil = (Int32)reader["bumil"],
                            bulin = (Int32)reader["bulin"],
                            bumilRisti = (Int32)reader["bumilRisti"]
                        }
                    });

                    nomor++;
                }

                con.Close();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSasaran;
        }

        public void DeleteSasaran(string tahun, int idDesa)
        {
            string query = $"DELETE FROM tbsasaran WHERE iddesa={idDesa} AND tahun='{tahun}'";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
