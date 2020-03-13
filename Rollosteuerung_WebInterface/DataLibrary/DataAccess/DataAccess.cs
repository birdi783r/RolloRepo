using DataLibrary.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;

namespace DataLibrary.DataAccess
{
    public class DataAccess
    {
        public static string GetConnectionString(string connectionName = "postgresql")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }


        public static List<DeviceModel> LoadItems()
        {
            List<DeviceModel> list = new List<DeviceModel>();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand("select * from device;", conn);
                NpgsqlDataReader dr = comm.ExecuteReader();
                //int iD, string name, bool roomAsigned, int roomNr, DateTime reportedOn, DateTime assignedOn
                while (dr.Read())
                {
                    var id = int.Parse(dr[0].ToString());
                    var did = int.Parse(dr[6].ToString());
                    var name = dr[1].ToString();
                    var tf = bool.Parse(dr[2].ToString());
                    var roomNr = int.Parse(dr[3].ToString());
                    DateTime reported = DateTime.Parse(dr[4].ToString());
                    DateTime assigned = DateTime.Parse(dr[5].ToString());
                    list.Add(new DeviceModel(id, did, name, tf, roomNr, reported, assigned));
                }


                conn.Close();
            }
            //list.Add(new DeviceModel() { ID = 1, Name = "Esp1" });
            //list.Add(new DeviceModel() { ID = 2, Name = "Esp2" });
            if (list.Count != 0 && list.Count > 0)
                return list;
            else
                throw new NotImplementedException();
        }

        public static DeviceModel LoadItemById(int did)
        {
            DeviceModel model = new DeviceModel();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"select * from device where deviceid={did};", conn);
                NpgsqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                    model = new DeviceModel(int.Parse(dr[0].ToString()), int.Parse(dr[6].ToString()), dr[1].ToString(), bool.Parse(dr[2].ToString()),
                        int.Parse(dr[3].ToString()), DateTime.Parse(dr[4].ToString()), DateTime.Parse(dr[5].ToString()));
            }
            if (model.ID != 0 && model.DeviceID != 0)
                return model;
            else
            {
                //var x = SaveItemByIdOnly(did);
                //if (x)
                //    return LoadItemById(did);
                //else
                    return new DeviceModel() { ID = 0, Name = "no device" };
            }
        }

        public static bool SaveItemById(int did, string name, bool roomAsigned, int roomNr, DateTime r, DateTime a)
        {
            int i = 0;
            var dd = r.Year + "-" + r.Month + "-" + r.Day;
            var aa = a.Year + "-" + a.Month + "-" + a.Day;
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"insert into device (name,deviceid,room_asigned,roomnumber,reported_on,assigned_on) " +
                    $"values ('{name}',{did.ToString()},{roomAsigned.ToString()},{roomNr.ToString()},'{dd.ToString().Substring(10)}','{aa.ToString().Substring(10)}');", conn);
                i = comm.ExecuteNonQuery();

            }
            if (i == 1)
                return true;
            else
                return false;
        }
        public static bool EditItem(int did, string name, bool roomAsigned, int roomNr, DateTime r, DateTime a)
        {
            int i = 0;
            var dd = r.Year + "-" + r.Month + "-" + r.Day;
            var aa = a.Year + "-" + a.Month + "-" + a.Day;
            var x =
 $"update device set name='{name}', room_asigned={roomAsigned.ToString()},roomnumber={roomNr.ToString()},reported_on='{dd}',assigned_on='{aa}' where deviceid = {did};";

            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                var d = 0;
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand(x, conn);
                i = comm.ExecuteNonQuery();

            }
            if (i == 1)
                return true;
            else
                return false;
        }
        public static bool SaveItemByIdOnly(int did)
        {
            int i = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                string dnow = "" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"insert into device (name,deviceid,room_asigned,roomnumber,reported_on,assigned_on) " +
                    $"values ('',{did},{false},0,'{dnow}','{dnow}');", conn);
                i = comm.ExecuteNonQuery();

            }
            if (i == 1)
                return true;
            else
                return false;
        }
        public static bool DeleteItemById(int did)
        {
            int i = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"delete from device where deviceid = {did};", conn);
                i = comm.ExecuteNonQuery();

            }
            if (i == 1)
                return true;
            else
                return false;
        }
    }
}
