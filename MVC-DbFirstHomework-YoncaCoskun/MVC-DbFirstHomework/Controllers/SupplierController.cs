using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_DbFirstHomework.Models;

namespace MVC_DbFirstHomework.Controllers
{
    public class SupplierController : Controller
    {
        //
        // GET: /Supplier/
        public ActionResult Listele()
        {
            NorthwindEntities context=new NorthwindEntities();

            var nakliyeciler = context.Suppliers.Select(            
              x=> new NakliyeciModel
            {
                NakliyeciId = x.SupplierID,
                SirketAdi = x.CompanyName,
                IletisimIsmi = x.ContactName,
                IletisimUnvani = x.ContactTitle,
                Adres = x.Address,
                Sehir = x.City
            });
            
            
            


            return View(nakliyeciler);
        }

        public ActionResult Gor(int id)
        {
            if (id>0)
            {
                using (NorthwindEntities context=new NorthwindEntities())
                {
                    Supplier nakliyeci = context.Suppliers.Find(id);

                    NakliyeciModel model = new NakliyeciModel
                    {
                        NakliyeciId = nakliyeci.SupplierID,
                        SirketAdi = nakliyeci.CompanyName,
                        IletisimIsmi = nakliyeci.ContactName,
                        IletisimUnvani = nakliyeci.ContactTitle,
                        Adres = nakliyeci.Address,
                        Sehir = nakliyeci.City
                    };

                    return View(model);
                }
            }
            return View();
        }

        public ActionResult Sil(int id)
        {
            using (NorthwindEntities context=new NorthwindEntities())
            {
                Supplier sp = context.Suppliers.Find(id);
                context.Suppliers.Remove(sp);
                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }

        public ActionResult Kaydet(NakliyeciModel model)
        {
            using (NorthwindEntities context=new NorthwindEntities())
            {
                 Supplier sp;

            if (model.NakliyeciId>0)
            {
                sp= context.Suppliers.Find(model.NakliyeciId);
            }
            else
            {
                sp=new Supplier();

                context.Suppliers.Add(sp);
            }

                sp.CompanyName = model.SirketAdi;
                sp.ContactName = model.IletisimIsmi;
                sp.ContactTitle = model.IletisimUnvani;
                sp.Address = model.Adres;
                sp.City = model.Sehir;

                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}