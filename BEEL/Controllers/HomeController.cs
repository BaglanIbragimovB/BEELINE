using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEL.Models;
using BEEL.Models.Identity;

namespace BEEL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            MyProduct = new Product();
            MyUser = User.
        }
        public ActionResult Index()
        {
            ViewBag.MyProduct = MyProduct;
            ViewBag.MyUser = MyUser;
            return View(CurrentUser);                                    
        }

        private User currentUser = null;
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    string userName = User.Identity.Name;
                    if (userName != null)
                    {
                        currentUser = new DBContext().Users.FindByNameAsync(userName).Result;
                    }
                }
                return currentUser;
            }
        }
        public Product MyProduct { get; set; }
        public User    MyUser { get; set; }        
    }
}