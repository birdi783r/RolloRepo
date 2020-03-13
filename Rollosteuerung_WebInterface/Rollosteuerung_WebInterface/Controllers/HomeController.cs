using DataLibrary.DataAccess;
using Rollosteuerung_WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.DataAccess.UserAccess;

namespace Rollosteuerung_WebInterface.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (BaseUser != null)
            {
                if (BaseUser.IsInRole("Admin") || BaseUser.IsInRole("SuperAdmin"))
                {
                    var model = new List<Device>();
                    var x = DataAccess.LoadItems();
                    foreach (var d in x)
                    {
                        model.Add(new Device
                        {
                            DeviceID = d.DeviceID,
                            Name = d.Name,
                            RoomAsigned = d.RoomAsigned,
                            AssignedOn = d.AssignedOn,
                            ReportedOn = d.ReportedOn,
                            RoomNr = d.RoomNr
                        });

                    }
                    if (model.Count > 0)
                        return View("~/views/device/_list.cshtml", model);
                    else
                        return View("~/views/auth/_login.cshtml");
                }
                if (BaseUser.IsInRole("Default"))
                    return View("~/views/shared/Error.cshtml");
                else
                    return View("~/views/auth/_login.cshtml");
            }

            else
                return View("~/views/auth/_login.cshtml");
        }
        public ActionResult LoginPost(LoginModel model)
        {
            UserModel m = new UserModel("Max", "Mustermann", false, false, "test@htlwienwest.at", Encrypt("test123"));
            //var d = CreateUser(m.FirstName, m.LastName, m.IsAdmin, m.IsSuperAdmin, m.Email, m.Password);

            var x = GetUser(model.Email);
            UserModel mod = new UserModel()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsAdmin = x.IsAdmin,
                Email = x.Email,
                IsSuperAdmin = x.IsSuperAdmin,
                Password = x.Password
            };
            if (x.ID != 0)
            {
                string cryptedPW = Encrypt(model.Password);
                if (cryptedPW == x.Password)
                {
                    IPrincipal User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, mod.FirstName +" " + mod.LastName ),
                        new Claim(ClaimTypes.Email, mod.Email ),
                        new Claim(ClaimTypes.Role, mod.IsSuperAdmin ? "SuperAdmin" : (mod.IsAdmin ? "Admin" : "Default")),
                    }));
                    var xd = User.IsInRole("Admin");
                    BaseUser = User;
                    HttpContext.User = User;
                    Thread.CurrentPrincipal = User;
                    return RedirectToAction("List", "Device");
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            BaseUser = null;
            return View("~/views/auth/_login.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}