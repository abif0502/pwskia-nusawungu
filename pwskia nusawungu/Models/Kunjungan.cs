using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace pwskia_nusawungu.Models
{
    public class Kunjungan
    {
        public int? id { get; set; }
        public int? kunjunganKe { get; set; }
        public Desa desa { get; set; }
        public string tanggal { get; set; }
        public string penanggungJawab { get; set; }
    }

    
}
