using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pwskia_nusawungu.Models
{
    public class Admin
    {
        public int? nums { get; set; }
        public string nama { get; set; }

        [StringLength(16, MinimumLength =16, ErrorMessage = "Panjang NIK harus 16 karakter")]
        public string nik { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "Panjang username minimal 8 karakter")]
        public string username { get ; set; }

        [StringLength(20, MinimumLength = 18, ErrorMessage = "Panjang NIP harus 18 karakter")]
        public string nip { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "Panjang Password minimal 8 karakter")]
        public string passw { get; set; }
        public string super { get; set; }
    }
}
