using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwskia_nusawungu.Models
{
    public class Kunjungan1Model
    {
        public int? idRecord { get; set; }
        public string tanggal { get; set; }
        public int jumlahBulanLalu { get; set; }
        public int jumlahBulanIni { get; set; }
        public int abs { get; set; }
        public double persentase { get; set; }
        public int R { get; set; }
    }
}
