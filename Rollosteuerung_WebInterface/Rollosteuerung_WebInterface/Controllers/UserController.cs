using Rollosteuerung_WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.DataAccess.UserAccess;

namespace Rollosteuerung_WebInterface.Controllers
{
    public class UserController : BaseController
    {
        public IPrincipal Principal;
        public UserController()
        {
            Principal = BaseUser;
        }
        // GET: User
        public ActionResult List()
        {
            if (BaseUser != null && !BaseUser.IsInRole("Default") && !BaseUser.IsInRole("Admin"))
            {
                var d = GetAllUsers();
                var model = new List<UserModel>();
                foreach (var x in d)
                {
                    model.Add(new UserModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        IsAdmin = x.IsAdmin,
                        Email = x.Email,
                        IsSuperAdmin = x.IsSuperAdmin,
                        Password = x.Password
                    });
                }
                if (model.Count > 0)
                    return View(model);
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            if (BaseUser.IsInRole("SuperAdmin"))
            {
                var ePass = Encrypt(model.Password);
                var x = CreateUser(model.FirstName, model.LastName, model.IsAdmin, model.IsSuperAdmin, model.Email, ePass);
                if (x)
                    return View();
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }
        [HttpPost]
        public ActionResult Delete(UserModel model)
        {
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                var x = DeleteUser(model.Email, model.FirstName, model.LastName);

                if (x)
                    return View(model);
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }
        [HttpGet]
        public ActionResult Delete()
        {
            var model = new UserModel();
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                return View(model);
            }
            else
                return View("~/views/shared/Error.cshtml");
        }

        [HttpGet]
        public ActionResult Details(string email)
        {
            if(email.Contains("%40"))
                email = email.Replace("%40","@");
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                var x = GetUser(email);
                var model = new UserModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsAdmin = x.IsAdmin,
                    Email = x.Email,
                    IsSuperAdmin = x.IsSuperAdmin,
                    Password = x.Password
                };

                if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
                {
                    return View("~/views/user/Details.cshtml",model);
                }
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");

        }
        [HttpGet]
        public ActionResult Edit(string email)
        {
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                var x = GetUser(email);
                var model = new UserModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsAdmin = x.IsAdmin,
                    Email = x.Email,
                    IsSuperAdmin = x.IsSuperAdmin,
                    Password = x.Password
                };
                return View(model);
            }
            else
                return View("~/views/shared/Error.cshtml");
        }
        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                //update fehlt 
                return View(model);
            }
            else
                return View("~/views/shared/Error.cshtml");
        }
        [HttpPost]
        public ActionResult Details(UserModel model)
        {
            if (BaseUser != null && BaseUser.IsInRole("SuperAdmin"))
            {
                var x = GetUser(model.Email);
                if (x != null)
                {
                    model = new UserModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        IsAdmin = x.IsAdmin,
                        Email = x.Email,
                        IsSuperAdmin = x.IsSuperAdmin,
                        Password = x.Password
                    };
                    return View(model);
                }
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }
    }
}