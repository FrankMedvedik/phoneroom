using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class PeopleController : ApiController
    {
        private respEntities db = new respEntities();

        // GET: api/people
        public IQueryable<person> Getperson()
        {
            return db.people.Take(100);
        }

        // GET: api/people/phoneNumber
        public IQueryable<person> Getperson(string phoneNumber)
        {
            string pn = FormatUtil.formatPhoneNumber(phoneNumber);

            return ( from p in db.people
                     from e in db.phones
                     where e.phone_number == pn &&
                     e.person == p
                     select p);
        }


        // GET: api/people/5
        [ResponseType(typeof(person))]
        public async Task<IHttpActionResult> Getperson(Guid id)
        {
            person person = await db.people.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

    }
}