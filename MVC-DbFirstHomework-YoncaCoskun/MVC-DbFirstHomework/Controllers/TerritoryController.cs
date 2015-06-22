using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_DbFirstHomework.Models;

namespace MVC_DbFirstHomework.Controllers
{
    public class TerritoryController : Controller
    {
        //
        // GET: /Territory/
        public ActionResult Listele()
        {
            NorthwindEntities context = new NorthwindEntities();

            var araziler = context.Territories.Select(x => new AraziModel
            {
                AraziAciklama = x.TerritoryDescription,
                AraziId = x.TerritoryID,
                BölgeId = x.RegionID


            });

            return View(araziler);
        }


        public ActionResult Gor(string id, bool degistiMi)
        {
            if (degistiMi)
            {
                using (NorthwindEntities context = new NorthwindEntities())
                {
                    Territory arazi = context.Territories.FirstOrDefault(x => x.TerritoryID == id);


                    if (arazi != null)
                    {
                        AraziModel model = new AraziModel
                        {
                            AraziId = arazi.TerritoryID,
                            AraziAciklama = arazi.TerritoryDescription,
                            BölgeId = arazi.RegionID,
                            GuncellenecekMi = degistiMi
                        };

                        return View(model);
                    }

                }
            }
            return View();
        }

        public ActionResult Kaydet(AraziModel model)
        {


            using (NorthwindEntities context = new NorthwindEntities())
            {

                Territory tr;
                if (model.GuncellenecekMi)
                {
                    tr = context.Territories.FirstOrDefault(x => x.TerritoryID == model.AraziId);
                    tr.TerritoryDescription = model.AraziAciklama.Trim();
                    tr.RegionID = model.BölgeId;
                    context.Entry(tr).State=EntityState.Modified;
                    
                    
                }
                else
                {
                    tr = new Territory();              
                    context.Territories.Add(tr);
                    tr.TerritoryID = model.AraziId;
                    tr.TerritoryDescription = model.AraziAciklama.Trim();
                    tr.RegionID = model.BölgeId;

                }

                context.SaveChanges();

            }
            return RedirectToAction("Listele");
        }


        public ActionResult Sil(string id)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                Territory tr = context.Territories.FirstOrDefault(x => x.TerritoryID ==id);
                context.Territories.Remove(tr);
                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}