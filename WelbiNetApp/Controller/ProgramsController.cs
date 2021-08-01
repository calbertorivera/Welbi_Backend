using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WelbiBackend.Entites;
using WelbiBackend.Proxy;
using WelbiNetApp.Filters;
using WelbiNetApp.Properties;
using System.Linq;
using WelbiBackend.Business;

namespace WelbiNetApp.Controller
{

    /**
  * Welbi Challenge - This class is the Rest API Controler to serve the basic functionalities to the front application
  * 
  * Author: Carlos Alberto Rivera
  * Date: July 30th, 2021 Time: 9:00 
  */
    [BasicAuthentication]
    public class ProgramsController : ApiController
    {
        // GET api/Program
        /// <summary>
        /// gets all the programs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Program> Get()
        {
            if (Common.PROGRAMS_IN_CACHE == null)
            {
                IWebService<Program> programsServiceCall = new ProgramsService();
                String url = String.Format(Settings.Default.PROGRAMS_ENDPOINT, Common.Token);
                //save in cache
                Common.PROGRAMS_IN_CACHE = programsServiceCall.LookUpALLItems(url);
                return Common.PROGRAMS_IN_CACHE;
            }
            else
            {
                return Common.PROGRAMS_IN_CACHE;

            }
            
        }

        // GET api/Program/5
        /// <summary>
        /// gets one program
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProgramAttendance Get(String id)
        {
           return Attendees().Where(a=>a.id == id).FirstOrDefault();
        }

        [Route("api/attendees")]
        [HttpGet]
        public List<ProgramAttendance> Attendees()
        {
            if (Common.PROGRAMS_ATTENDEES_IN_CACHE == null)
            {
                AttendeesManager attendeesManager = new AttendeesManager();
                var attendees = attendeesManager.getAllProgramAttendees(String.Format(Settings.Default.PROGRAMS_ENDPOINT, Common.Token), String.Format(Settings.Default.RESIDENTS_ENDPOINT, Common.Token));
                             
                //save in cache
                Common.PROGRAMS_ATTENDEES_IN_CACHE = attendees.ToList();
                return Common.PROGRAMS_ATTENDEES_IN_CACHE;
            }
            else
            {
                return Common.PROGRAMS_ATTENDEES_IN_CACHE;

            }
        }

        // POST api/Program
        /// <summary>
        /// Insert/Register a new program
        /// </summary>
        /// <param name="registerProgram"></param>
        /// <returns></returns>
        public String Post([FromBody] RegisterProgram registerProgram)
        {
            ProgramsManager attendeesManager = new ProgramsManager();

            bool success = attendeesManager.registerProgram(registerProgram, String.Format(Settings.Default.PROGRAMS_ENDPOINT, Common.Token));

            //clean cache
            Common.PROGRAMS_ATTENDEES_IN_CACHE = null;

            return "OK";
        }

        // PUT api/Program/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Program/5
        public void Delete(int id)
        {
        }
    }
}