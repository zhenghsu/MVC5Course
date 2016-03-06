using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index(bool? IsActive ,string keyword)
        {
        //    var Product = new Product()
        //            {
        //                ProductName = "BMW",
        //                Price = 5,
        //                Stock = 1,
        //                Active = true
        //            };
        //    db.Product.Add(Product);

            SaveChanges();
            //var pkey = Product.ProductId;
            //var data = db.Product.ToList();
            //var data = db.Product.Where(p => p.ProductId == pkey).ToList();//多筆的集合型別
            //var data = db.Product.OrderByDescending(p => p.ProductId).Take(5);
            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();
            //IsActive = false;
            if (IsActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false);
            }
           // keyword="BM";
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword)).Take(5);
            }
                
            //    foreach (var item in data)
            //{
            //    item.Price= item.Price+1;
            //}
            db.SaveChanges();
            return View(data);
        }

        private void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    string entityname = item.Entry.Entity.GetType().Name;
                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(entityname + "" + err.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult Detail(int Id)
        {
            var data = db.Product.Where(p => p.ProductId == Id).FirstOrDefault();
            SaveChanges();
            return View(data);
        }



        public ActionResult Delete(int Id)
        {
            var Product = db.Product.Find(Id);
            ////法一
            //foreach (var Ol in db.OrderLine.Where(p=>p.ProductId==Id).ToList())
            //{
            //    db.OrderLine.Remove(Ol);
            //}
            ////法二
            //foreach (var ol in Product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}
            ////法三
            db.OrderLine.RemoveRange(Product.OrderLine);

            db.Product.Remove(Product);
          

            SaveChanges();
            return  RedirectToAction("Index");
        }
      
    }

}