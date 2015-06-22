using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVC_DbFirstHomework.Models;

namespace MVC_DbFirstHomework.Controllers
{
    public class RegionController : Controller
    {
        //
        // GET: /Region/
        
        public ActionResult Listele()
        {
            NorthwindEntities context=new NorthwindEntities();
      

            var bolgeler = context.Regions.Select(
                x => new BolgeModel
                {
                    BolgeId = x.RegionID,
                    BolgeAciklama = x.RegionDescription
                }).ToList();
            
            
            return View(bolgeler);
        }

        public ActionResult Gor(int id, bool gunMu)
        {
            if (gunMu)
            {
                using (NorthwindEntities context=new NorthwindEntities())
                {
                    Region bolge = context.Regions.Find(id);


                    BolgeModel model = new BolgeModel
                    {
                        BolgeId = bolge.RegionID,
                        BolgeAciklama = bolge.RegionDescription,
                        GuncellenecekMi = gunMu
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
                Region rg = context.Regions.Find(id);
                context.Regions.Remove(rg);
                context.SaveChanges();
            }
            return RedirectToAction("Listele");
       }

        public ActionResult Kaydet(BolgeModel model)
        {
            using (NorthwindEntities context=new NorthwindEntities())
            {
                Region rg;
                if (model.GuncellenecekMi)
                {
                    rg = context.Regions.Find(model.BolgeId);
                }
                else
                {
                    
                    rg=new Region();
                   
                    context.Regions.Add(rg);
                    

                }

                    rg.RegionID = model.BolgeId;
                    rg.RegionDescription = model.BolgeAciklama.Trim();
                

                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}