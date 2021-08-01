using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Entites;
using WelbiBackend.Proxy;
using System.Linq;

namespace WelbiBackend.Business
{
    /**
* Welbi Challenge - This class performs all the business logic related to the attendees
* 
* Author: Carlos Alberto Rivera
* Date: July 30th, 2021 Time: 9:00 
*/
    public class AttendeesManager
    {
        /// <summary>
        /// this methods merge the programs information with each resident information
        /// </summary>
        /// <param name="programsEndPoint"></param>
        /// <param name="residentsEndPoint"></param>
        /// <returns></returns>
        public ProgramAttendance[] getAllProgramAttendees(String programsEndPoint, String residentsEndPoint)
        {
            //get all the programs
            IWebService<Program> programsServiceCall = new ProgramsService();
            String url = programsEndPoint;
            List<Program> programs = programsServiceCall.LookUpALLItems(url);

            //get all the residents
            IWebService<Resident> residentServiceCall = new ResidentsService();
            url = residentsEndPoint;
            List<Resident> residents = residentServiceCall.LookUpALLItems(url);

            //merge the results
            List<ProgramAttendance> programAttendance = new List<ProgramAttendance>();
            foreach (var program in programs)
            {

                ProgramAttendance pr = new ProgramAttendance(program);

                foreach (var attendance in program.attendance)
                {
                    var resident = residents.Select(a => new ProgramAttendanceResident(a.id, a.name, a.room)).FirstOrDefault(b => b.id == attendance.residentId);
                    resident.eventStatus = attendance.status;
                    pr.attendees.Add(resident);
                }

                programAttendance.Add(pr);

            }

            return programAttendance.OrderByDescending(a => a.startTime).ToArray();


        }

        /// <summary>
        /// this method register a new Attendee
        /// </summary>
        /// <param name="registerAteendee"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool registerAttendee(RegisterAttendee registerAteendee, string url)
        {
            ResidentsService residentServiceCall = new ResidentsService();
            return residentServiceCall.registerAttendee(registerAteendee, url);
        }
    }
}
