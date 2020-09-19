using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class RoleModel
    {
        [Required(ErrorMessage = "Rol İsmi Zorunludur.")]
        [DisplayName("Rol İsmi")]
        public string Name { get; set; }

        //KULLANICIYA GÖSTERİLMEYECEK. UPDATE VE DELETE İŞLEMİ İÇİN GEREKLİ.
        public string Id { get; set; }
    }
}
