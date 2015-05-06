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

        // GET: api/PhoneCalls?sip=sip:fmedvedik@reckner.com
        public IQueryable<PhoneCall> GetPhoneCalls(string sip)
        {
            throw new NotImplementedException();

        }

        // GET: api/PhoneCalls?sip=sip:fmedvedik@reckner.com
        public IQueryable<PhoneCall> GetPhoneCalls(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();

        }

        // GET: api/PhoneCalls?JobNum=20140101,taskid=1
        public IQueryable<PhoneCall> GetPhoneCalls(string jobNum, int taskId)
        {
            throw new NotImplementedException();
        }

        // PUT: api/PhoneCalls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhoneCall(int id, PhoneCall p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*
            if (id != p.callID)
            {
                return BadRequest();
            }
            */
            throw new NotImplementedException();
            /*
            db.Entry(returnedCall).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!returnedCallExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
             */
        }

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

        // DELETE: api/PhoneCalls/5
        [ResponseType(typeof(PhoneCall))]
        public async Task<IHttpActionResult> DeletePhoneCall(int id)
        {
            throw new NotImplementedException();
            /*
            PhoneCall PhoneCall = await db.PhoneCall.FindAsync(id);
            if (PhoneCall == null)
            {
                return NotFound();
            }

            db.PhoneCalls.Remove(PhoneCall);
            await db.SaveChangesAsync();

            return Ok(PhoneCall); */
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