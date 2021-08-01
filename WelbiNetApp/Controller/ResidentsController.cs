using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
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
    public class ResidentsController : ApiController
    {


        // GET api/<controller>
        /// <summary>
        /// Gets the full list of residents
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Resident> Get()
        {
            if (Common.RESIDENTS_IN_CACHE == null)
            {
                ResidentsService residentServiceCall = new ResidentsService();
                String url = String.Format(Settings.Default.RESIDENTS_ENDPOINT, Common.Token);

                //save in cache
                Common.RESIDENTS_IN_CACHE = residentServiceCall.LookUpALLItems(url).OrderBy(a => a.name).ToList();
                return Common.RESIDENTS_IN_CACHE;
            }
            else
            {
                return Common.RESIDENTS_IN_CACHE;

            }

        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets a single resident
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Resident Get(String id)
        {
            return Get().Where(a => a.id == id).FirstOrDefault();
        }

        /// <summary>
        /// This method registers a new resident
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        public String Post([FromBody] Resident resident)
        {
            ResidentsService residentServiceCall = new ResidentsService();
            residentServiceCall.Insert(resident, Settings.Default.RESIDENTS_ENDPOINT);
            Common.RESIDENTS_IN_CACHE = null;

            return "OK";
        }

        /// <summary>
        /// This method registers a new Attendee
        /// </summary>
        /// <param name="registerAteendee"></param>
        /// <returns></returns>
        [Route("api/registerAttendee")]
        [HttpPost]
        public String Attendees([FromBody] RegisterAttendee registerAteendee)
        {
            AttendeesManager attendeesManager = new AttendeesManager();

            bool success = attendeesManager.registerAttendee(registerAteendee, String.Format(Settings.Default.REGISTER_ATTENDEE_ENDPOINT, registerAteendee.programId, Common.Token));

            //save in cache
            Common.PROGRAMS_ATTENDEES_IN_CACHE = null;

            return "OK";
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}