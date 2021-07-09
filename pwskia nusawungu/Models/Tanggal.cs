using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pwskia_nusawungu.Models
{
    public class Tanggal
    {
        private string[] _hari = { "Senin", "Selasa", "Rabu", "Kamis", "Jum\'at", "Sabtu", "Minggu" };
        private string[] _bulan = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };

        public string TanggalSekarang()
        {
            string hariIni = _hari[(int)DateTime.Today.DayOfWeek];
            string bulanIni = _bulan[DateTime.Today.Month - 1];

            return $"{hariIni}, {DateTime.Today.Day} {bulanIni} {DateTime.Today.Year}";
        }

        public string[] GetDaftarBulan()
        {
            return _bulan;
        }

    }
}
