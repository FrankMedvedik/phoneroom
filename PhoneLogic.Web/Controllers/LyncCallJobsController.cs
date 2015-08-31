using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class LyncCallJobsController : ApiController
    {
        

        // GET: api/LyncCallJobs?startDate=2&endDate=3
        public IEnumerable<LyncCallJob> GetLyncCallJobs(Int64 startDate, Int64 endDate)
        {
            PhoneLogicEntities db = new PhoneLogicEntities();
            try
            {
                var v  = db.rpt_GetLyncCallJobs(new DateTime(startDate), new DateTime(endDate));

                return v.ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "GetLyncCallJobs"
                };
                throw new HttpResponseException(resp);
            }
     
        }
   }
}


