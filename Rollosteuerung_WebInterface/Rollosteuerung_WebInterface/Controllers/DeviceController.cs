using DataLibrary.DataAccess;
using Rollosteuerung_WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rollosteuerung_WebInterface.Controllers
{
    //[Authorize(Roles = "Admin,Default,SuperAdmin")]
    [Route("device")]
    public class DeviceController : BaseController
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
                else
                    return View("~/views/auth/_login.cshtml");
            }

            else
                return View("~/views/auth/_login.cshtml");
        }
        // GET: Device
        [AllowAnonymous]
        [Route("list")]
        public ActionResult List()
        {
            if (BaseUser != null)
            {
                if (BaseUser.IsInRole("Admin") || BaseUser.IsInRole("SuperAdmin"))
                {
                    var model = new List<Device>();
                    //model.Add(new Device { ID=1,Name="ESP1",RoomAsigned=true,RoomNr=367,AssignedOn=DateTime.Now.AddDays(-6.0),ReportedOn=DateTime.Now });
                    //model.Add(new Device { ID=2,Name="ESP2",RoomAsigned=true,RoomNr=368, AssignedOn = DateTime.Now.AddDays(-3.0), ReportedOn = DateTime.Now.AddDays(-1.0) });
                    //model.Add(new Device { ID=3,Name="ESP3",RoomAsigned=false,RoomNr=365, AssignedOn = DateTime.Now.AddDays(-12.0), ReportedOn = DateTime.Now });
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
                        return View("~/views/shared/Error.cshtml");
                }
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");

        }
        //[Authorize(Roles = "Admin,SuperAdmin")]
        // GET: Device/Details/5
        public ActionResult Details(int did)
        {
            if (BaseUser != null)
            {
                if (BaseUser.IsInRole("Admin") || BaseUser.IsInRole("SuperAdmin"))
                {
                    var d = DataAccess.LoadItemById(did);
                    var model = new Device
                    {
                        DeviceID = d.DeviceID,
                        Name = d.Name,
                        RoomAsigned = d.RoomAsigned,
                        AssignedOn = d.AssignedOn,
                        ReportedOn = d.ReportedOn,
                        RoomNr = d.RoomNr
                    };
                    return View("~/views/device/_details.cshtml", model);
                }
                return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }

        // GET: Device/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            BaseUser = null;
            return View("~/views/auth/_login.cshtml");
        }
        // GET: Device/Edit/5
        [HttpGet]
        public ActionResult Edit(int did)
        {
            if (BaseUser != null)
            {
                if (BaseUser.IsInRole("Admin") || BaseUser.IsInRole("SuperAdmin"))
                {
                    var x = DataAccess.LoadItemById(did);
                    var model = new Device()
                    {
                        DeviceID = x.DeviceID,
                        Name = x.Name,
                        AssignedOn = x.AssignedOn,
                        ReportedOn = x.ReportedOn,
                        RoomAsigned = x.RoomAsigned,
                        RoomNr = x.RoomNr
                    };
                    return View("~/views/device/Edit.cshtml", model);
                }
                else
                    return View("~/views/shared/Error.cshtml");
            }
            else
                return View("~/views/shared/Error.cshtml");
        }

        // POST: Device/Edit/5
        [HttpPost]
        public ActionResult Edit(Device device)
        {
            try
            {
                if (BaseUser != null)
                {
                    if (BaseUser.IsInRole("Admin") || BaseUser.IsInRole("SuperAdmin"))
                    {
                        // TODO: Add update logic here
                        //int iD, string name, bool roomAsigned, int roomNr, DateTime reportedOn, DateTime assignedOn
                        var x = DataAccess.EditItem(device.DeviceID, device.Name, device.RoomAsigned, device.RoomNr, device.ReportedOn, device.AssignedOn);
                        return RedirectToAction("List", "Device");
                    }
                    else
                        return View("~/views/shared/Error.cshtml");
                }
                else
                    return View("~/views/shared/Error.cshtml");
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Delete/5
        public ActionResult Delete(int did)
        {
            var x = DataAccess.LoadItemById(did);
            var model = new Device()
            {
                DeviceID = x.DeviceID,
                Name = x.Name,
                AssignedOn = x.AssignedOn,
                ReportedOn = x.ReportedOn,
                RoomAsigned = x.RoomAsigned,
                RoomNr = x.RoomNr
            };
            return View(model);
        }

        // POST: Device/Delete/5
        [HttpPost]
        public ActionResult Delete(Device device)
        {
            try
            {
                // TODO: Add delete logic here
                var d = DataAccess.DeleteItemById(device.DeviceID);
                if (d)
                    return RedirectToAction("List", "Device");
                else
                    return RedirectToAction("Delete", "Device", device.DeviceID);
            }
            catch
            {
                return View();
            }
        }
    }
}
