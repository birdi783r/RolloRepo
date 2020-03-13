using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rollosteuerung_WebInterface.Models
{
    public class ResponseModel
    {
        public static string SUCCESS = "SUCCESS";
        public static string FAILED = "FAILED";
        public string TYPE { get; set; }
    }
}