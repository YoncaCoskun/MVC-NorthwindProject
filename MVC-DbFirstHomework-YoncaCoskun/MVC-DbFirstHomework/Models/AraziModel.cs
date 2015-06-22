using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_DbFirstHomework.Models
{
    public class AraziModel
    {
        [Key]
        public string AraziId { get; set; }
        public string AraziAciklama { get; set; }
        public bool GuncellenecekMi { get; set; }
        public int BölgeId { get; set; }

        public AraziModel()
        {
            GuncellenecekMi = false;
        }
       
    }
}