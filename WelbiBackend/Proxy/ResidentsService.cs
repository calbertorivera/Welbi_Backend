using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Entites;
using WelbiBackend.Proxy.RequestEntities;
using WelbiBackend.Proxy.ResponseEntities;
using WelbiCommon;

namespace WelbiBackend.Proxy
{
    public class ResidentsService : IWebService<Resident>
    {
        /// <summary>
        /// this method Insert a new Resident through the web API
        /// </summary>
        /// <param name="item"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public bool Insert(WelbiBackend.Entites.Resident item, String endPoint)
        {

            //transform the request
            CreateResidentRequest resident = new CreateResidentRequest();
            resident.name = item.name;
            resident.firstName = item.firstName;
            resident.lastName = item.lastName;
            resident.preferredName = item.preferredName;
            resident.status = item.status;
            resident.room = item.room;
            resident.levelOfCare = item.levelOfCare;
            resident.ambulation = item.ambulation;
            resident.birthDate = Convert.ToDateTime(item.birthDate.time).ToUniversalTime().ToString("o");
            resident.moveInDate = Convert.ToDateTime(item.moveInDate.time).ToUniversalTime().ToString("o");

            //serialize
            string output = JsonConvert.SerializeObject(resident);

            //send
            String response = Utilities.CallRestService(endPoint, output, "POST");
            try
            {
                Resident residentResponse = JsonConvert.DeserializeObject<Resident>(response);
                return true;
            }
            catch (Exception exc)
            {                
                throw new Exception("There was an error creating the resident");
            }

            
        }

        /// <summary>
        /// this method gets all the residents
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public List<Resident> LookUpALLItems(string endPoint)
        {

            try
            {
                String response = Utilities.CallRestService(endPoint, null, "GET");
                JArray residentsArray = JArray.Parse(response.Replace("@ts","time"));
                return JsonConvert.DeserializeObject<List<Resident>>(residentsArray.ToString());
            }
            catch(Exception exc) 
            { 
                throw new Exception("There was an exception casting the response");
            }

        }

        public Resident LookUpItem(string endPoint, int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Resident item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Resident item, string endPoint)
        {
            throw new NotImplementedException();
        }

        public bool registerAttendee(RegisterAttendee registerAteendee, string url) 
        {
            try
            {
                string output = JsonConvert.SerializeObject(new RegisterAtendeeRequest() { residentId = registerAteendee.attendeeId, status = registerAteendee.status});
                String response = Utilities.CallRestService(url, output, "POST");
               
                return true;
            }
            catch (Exception exc)
            {
                return false;
              
            }
        }

    }
}
