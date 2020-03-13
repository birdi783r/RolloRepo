using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessModel
{
    public class DeviceModel
    {
        public DeviceModel(string name,int did, bool roomAsigned, int roomNr, DateTime reportedOn, DateTime assignedOn)
        {
            Name = name;
            DeviceID = did;
            RoomAsigned = roomAsigned;
            RoomNr = roomNr;
            ReportedOn = reportedOn;
            AssignedOn = assignedOn;
        }

        public DeviceModel()
        {
        }

        public DeviceModel(int iD, int did,string name, bool roomAsigned, int roomNr, DateTime reportedOn, DateTime assignedOn)
        {
            ID = iD;
            DeviceID = did;
            Name = name;
            RoomAsigned = roomAsigned;
            RoomNr = roomNr;
            ReportedOn = reportedOn;
            AssignedOn = assignedOn;
        }

        public int DeviceID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool RoomAsigned { get; set; }
        public int RoomNr { get; set; }
        public DateTime ReportedOn { get; set; }
        public DateTime AssignedOn { get; set; }
    }
}
