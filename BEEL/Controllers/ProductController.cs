using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEL.Models;
using NHibernate;
using NHibernate.Linq;

namespace BEEL.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (ISession session = DBContext.OpenSession())
            {
                var employees = session.Query<Product>().ToList();
                return View(employees);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                using (ISession session = DBContext.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        product.ChangeDate = System.DateTime.Now;

                        //session.Save(employee);
                        session.Persist(product);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = DBContext.OpenSession())
            {
                var employee = session.Get<Product>(id);
                return View(employee);
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product prod)
        {
            try
            {
                using (ISession session = DBContext.OpenSession())
                {
                    var prodUpdate = session.Get<Product>(id);

                    prodUpdate.ProductName = prod.ProductName;
                    prodUpdate.Count = prod.Count;
                    prodUpdate.ChangeDate = System.DateTime.Now;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(prodUpdate);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = DBContext.OpenSession())
            {
                var product = session.Get<Product>(id);
                return View(product);
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            using (ISession session = DBContext.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //Ни хрена не работает
                    //session.Delete(employee);
                    //А вот так работает                    
                    session.Delete(session.Get<Product>(id));
                    //Есть еще другой метод,тоже работает
                    //session.Delete(session.Load<Employee>(id));
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
