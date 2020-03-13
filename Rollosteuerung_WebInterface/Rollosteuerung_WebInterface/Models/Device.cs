using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rollosteuerung_WebInterface.Models
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public bool RoomAsigned { get; set; }
        public int RoomNr { get; set; }
        public DateTime ReportedOn { get; set; }
        public DateTime AssignedOn { get; set; }
    }
}