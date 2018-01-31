using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NHibernate;
using NHibernate.Linq;
using BEEL.Models.Identity;
using BEEL.Models;

namespace BEEL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public SignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<SignInManager>(); }
        }
        public UserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
        }

        // GET: Employee
        public ActionResult Index()
        {
            using (ISession session = DBContext.OpenSession())
            {
                var employees = session.Query<User>().ToList();
                return View(employees);
            }
        }


        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(User employee)
        {   //Убрал нахрен try/catch,не могу отлавить ошибки
            try
            {
                using (ISession session = DBContext.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        //session.Save(employee);
                        session.Persist(employee);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            using (ISession session = DBContext.OpenSession())
            {
                var employee = session.Get<User>(id);
                return View(employee);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User employee)
        {
            try
            {
                using (ISession session = DBContext.OpenSession())
                {
                    var employeetoUpdate = session.Get<User>(id);

                    employeetoUpdate.UserName = employee.UserName;
                    employeetoUpdate.FullName = employee.FullName;
                    employeetoUpdate.ChangeDate = System.DateTime.Now;

                    employeetoUpdate.user_list = employee.user_list;
                    employeetoUpdate.user_edit = employee.user_edit;
                    employeetoUpdate.user_delete = employee.user_delete;
                    employeetoUpdate.prod_list = employee.prod_list;
                    employeetoUpdate.prod_edit = employee.prod_edit;
                    employeetoUpdate.prod_delete = employee.prod_delete;


                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(employeetoUpdate);
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = DBContext.OpenSession())
            {
                var employee = session.Get<User>(id);
                return View(employee);
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User employee)
        {   //Убрал try/catch потому что хочу видеть все ошибки
            //try
            //{
            using (ISession session = DBContext.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //Ни хрена не работает
                    //session.Delete(employee);
                    //А вот так работает                    
                    session.Delete(session.Get<User>(id));
                    //Есть еще другой метод,тоже работает
                    //session.Delete(session.Load<Employee>(id));
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
            //}
            //catch (Exception exception)
            //{
            //return View();
            //}
        }
    }
}