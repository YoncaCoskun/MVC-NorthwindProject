using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_DbFirstHomework.Models
{
    public class BolgeModel
    {
        public int BolgeId { get; set; }
        public string BolgeAciklama { get; set; }
        public bool GuncellenecekMi { get; set; }

        public BolgeModel()
        {
            GuncellenecekMi = false;
        }
    }
}