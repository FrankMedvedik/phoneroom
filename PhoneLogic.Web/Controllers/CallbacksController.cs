using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Model;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class CallbacksController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();



        // GET: api/Callbacks/id
        public callback GetCallbacks(int id)
        {
            return db.callbacks.FirstOrDefault(b => b.callbackID == id);
        }

        
        // GET: api/Callbacks/sip
        public List<callbackDTO> GetCallbacks(string SIP)
        {
            List<callbackDTO> list = db.getMyCallbacks(SIP).ToList();
            return list;
        }

        // PUT: api/Callbacks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcallback(int id, CallbackDto cb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cb.callbackID)
            {
                return BadRequest();
            }

            try
            {
                
                var x = db.callbacks.FirstOrDefault(e => e.callbackID == cb.callbackID);
                x.Status= cb.status;
                x.StatusDate = cb.statusDate;
                x.SIP = cb.SIP;
                db.Entry(x).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }

            catch (Exception e)
            {
                if (!callbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Callbacks/4
        [ResponseType(typeof(callback))]
        public async Task<IHttpActionResult> Postcallback(int id, callback cb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cb.callbackID)
            {
                return BadRequest();
            }

            try
            {

                var x = db.callbacks.FirstOrDefault(e => e.callbackID == cb.callbackID);
                x.Status = cb.Status;
                x.StatusDate = cb.StatusDate;
                x.SIP = cb.SIP;
                db.Entry(x).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(cb);
            }

            catch (Exception e)
            {
                if (!callbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Callbacks/5
        public async Task<IHttpActionResult> Deletecallback(int id)
        {
            callback cb = await db.callbacks.FindAsync(id);

            if (cb == null)
            {
                return NotFound();
            }

            db.callbacks.Remove(cb);
            await db.SaveChangesAsync();

            return Ok(cb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool callbackExists(int id)
        {
            return db.callbacks.Count(e => e.callbackID == id) > 0;
        }
    }
}