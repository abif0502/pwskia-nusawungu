using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace pwskia_nusawungu.Models
{
    public class Kunjungan : Sasaran
    {
        public int? id { get; set; }
        public int? kunjunganKe { get; set; }
        public int jmlBulanLalu { get; set; }
        public int jmlBulanIni { get; set; }
        public string bulan { get; set; }
        public int r { get; set; }

        public string penanggungJawab { get; set; }

        public int abs
        {
            set { this.abs = jmlBulanIni + jmlBulanLalu; }
            get { return this.abs; }
        }

        public double persentase
        {
            
            set { this.persentase = this.abs / bumil * 100; }
            get { return this.persentase; }
        }
    }

    
}
