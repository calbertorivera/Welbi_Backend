using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Entites;
using WelbiBackend.Proxy.RequestEntities;
using WelbiCommon;

namespace WelbiBackend.Proxy
{
    public class ProgramsService : IWebService<Program>
    {


        public bool Insert(Program item, String endpoint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// this method gets all the items
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public List<Program> LookUpALLItems(string endPoint)
        {
            try
            {
                String response = Utilities.CallRestService(endPoint, null, "GET");
                JArray residentsArray = JArray.Parse(response.Replace("@ts", "time").Replace("\"tags\":[{\"tags\":\"\"}]", "\"tags\":[]"));
                return JsonConvert.DeserializeObject<List<Program>>(residentsArray.ToString());
            }
            catch (Exception exc)
            {
                throw new Exception("There was an exception casting the response");
            }
        }

        public Program LookUpItem(string endPoint, int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Program item)
        {
            throw new NotImplementedException();
        }
        public bool Update(Program item, string endPoint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// this method register a new program through the webserice
        /// </summary>
        /// <param name="registerProgram"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool registerProgram(RegisterProgram registerProgram, string url)
        {
            try
            {
                string output = JsonConvert.SerializeObject(new RegisterProgramRequest()
                {
                    name = registerProgram.name,
                    location = registerProgram.location,
                    allDay = Convert.ToBoolean(registerProgram.allDay),
                    start = Convert.ToDateTime(registerProgram.start).ToUniversalTime().ToString("o"),
                    end = Convert.ToDateTime(registerProgram.end).ToUniversalTime().ToString("o"),
                    tags = registerProgram.tags.Split(','),
                    hobbies = registerProgram.hobbies.Split(','),
                    facilitators = registerProgram.hobbies.Split(','),
                    isRepeated = Convert.ToBoolean(registerProgram.isRepeated),
                    levelOfCare = registerProgram.levelOfCare,
                    dimension = registerProgram.dimension
                });
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
