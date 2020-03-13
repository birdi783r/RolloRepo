using DataLibrary.DataAccess;
using Rollosteuerung_WebInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rollosteuerung_WebInterface.Controllers
{

    public class RoomController : ApiController
    {
        // GET: api/Room
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Room/5
        public Device Get(int did)
        {
            var x = DataAccess.LoadItemById(did);
            if (x != null)
            {
                return new Device
                {
                    DeviceID = x.DeviceID,
                    Name = x.Name
                };
            }
            else
                return new Device
                {
                    DeviceID = 0,
                    Name = "No Device"
                };
        }


        // POST: api/Room
        public string Post(int did)
        {
            var x = DataAccess.LoadItemById(did);
            if (x.DeviceID != 0)
            {
                return "success";
            }
            else
            {
                DataAccess.SaveItemByIdOnly(did);
                return "failure";
            }

        }

        // PUT: api/Room/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Room/5
        public void Delete(int id)
        {
        }
    }
}
