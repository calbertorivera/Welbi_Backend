using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WelbiBackend.Entites;
using WelbiNetApp.Properties;

namespace WelbiNetApp.Controller
{
    /**
  * Welbi Challenge - This class is the common methods and constants
  * 
  * Author: Carlos Alberto Rivera
  * Date: July 30th, 2021 Time: 9:00 
  */
    public class Common
    {
        //this token will be stored in cache during 8 min
        public static String Token
        {
            get
            {
                if (System.Web.HttpContext.Current.Cache.Get("TOKEN") == null)
                {
                    //get the Token from the start web service to bypass the security layer
                    String token = WelbiBackend.Proxy.StartService.getToken(Settings.Default.START_ENDPOINT, Settings.Default.EMAIL);
                    System.Web.HttpContext.Current.Cache.Insert("TOKEN", token, null, DateTime.Now.AddMinutes(8), TimeSpan.Zero);
                    return token;
                }
                else
                {
                    return System.Web.HttpContext.Current.Cache.Get("TOKEN").ToString();
                }

            }
        }

        //this Cache variable stores the programs
        public static List<Program> PROGRAMS_IN_CACHE
        {
            get
            {
                return (List<Program>)System.Web.HttpContext.Current.Cache.Get("PROGRAMS");

            }
            set 
            {
                if (value != null)
                {
                    System.Web.HttpContext.Current.Cache.Insert("PROGRAMS", value, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                }
                else 
                {
                    System.Web.HttpContext.Current.Cache.Remove("PROGRAMS");


                }

            }
        }
        //This cache variable stores the resident list for 30min
        public static List<Resident> RESIDENTS_IN_CACHE
        {
            get
            {
                return (List<Resident>)System.Web.HttpContext.Current.Cache.Get("RESIDENTS");

            }
            set
            {
                if (value != null)
                {
                    System.Web.HttpContext.Current.Cache.Insert("RESIDENTS", value, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                }
                else
                {
                    System.Web.HttpContext.Current.Cache.Remove("RESIDENTS");


                }

            }
        }

        //this variable stores the PROGRAMS AND ATTENDEES FULL INFORMATION
        public static List<ProgramAttendance> PROGRAMS_ATTENDEES_IN_CACHE 
        {
            get
            {
                return (List<ProgramAttendance>)System.Web.HttpContext.Current.Cache.Get("PROGRAM_ATTENDEES");

            }
            set
            {
                if (value != null)
                {
                    System.Web.HttpContext.Current.Cache.Insert("PROGRAM_ATTENDEES", value, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                }
                else
                {
                    System.Web.HttpContext.Current.Cache.Remove("PROGRAM_ATTENDEES");


                }

            }
        }
    }
}