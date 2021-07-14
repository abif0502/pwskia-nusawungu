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

        public List<Desa> GetSasaran(string desa="")
        {
            int nomor = 1;
            string query;
            if (desa == "")
            {
                query = "SELECT * FROM tbdesa";
            }
            else
            {
                query = $"SELECT * FROM tbdesa WHERE nama='{desa}'";
            }
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

        public List<Desa> GetSasaranPerBulan(string bulanDanTahun="", string desa="")
        {
            int nomor = 1;
            string query;
            List<Desa> dataSasaran = new List<Desa>();

            if(desa == "")
            {
                query = $"SELECT DISTINCT desa,bumil,bulin,bumilRisti FROM datapwskia WHERE tanggal LIKE '%{bulanDanTahun}%'";
            }
            else
            {
                query = $"SELECT DISTINCT desa,bumil,bulin,bumilRisti FROM datapwskia WHERE desa = '{desa}'";
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
                        nama = (string)reader["desa"],
                        sasaran = new Sasaran
                        {
                            bumil = (Int32)reader["bumil"],
                            bulin = (Int32)reader["bulin"],
                            bumilRisti = (Int32)reader["bumilRisti"]
                        }
                    });

                    nomor++;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataSasaran;
        }

        




    }
}
