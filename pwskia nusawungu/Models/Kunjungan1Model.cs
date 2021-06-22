using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwskia_nusawungu.Models
{
    class Kunjungan1Model
    {
        public int idRecord { get; set; }
        public string bulan { get; set; }
        public int jumlahBulanLalu { get; set; }
        public int jumlahBulanIni { get; set; }
        public int abs { get; set; }
        public int persentase { get; set; }
    }
}
