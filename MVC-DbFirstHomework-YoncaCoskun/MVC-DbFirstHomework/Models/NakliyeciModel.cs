using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Web;

namespace MVC_DbFirstHomework.Models
{
    public class NakliyeciModel
    {
        public int NakliyeciId { get; set; }
        public string SirketAdi { get; set; }
        public string IletisimIsmi { get; set; }
        public string IletisimUnvani { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }


    }
}