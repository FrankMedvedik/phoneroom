using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;
using PhoneLogic.Model;
using PhoneLogic.Repository.Utils;

namespace PhoneLogic.Web.Controllers
{
    public class PhoneCallsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        // POST: api/PhoneCalls
        [ResponseType(typeof(PhoneCall))]
        public async Task<IHttpActionResult> PostPhoneCall(PhoneCall p)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var agentId = AgentUtils.GetAgentId(p.SIP);
            db.InsertPlacedCall(agentId, p.JobFormatted, p.phoneNum, p.EventDesc);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = 0 }, p);
        }

        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneCallExists(int id)
        {
            throw new NotImplementedException();
            // return db.PhoneCalls.Count(e => e.callID == id) > 0;
        }
    }
}