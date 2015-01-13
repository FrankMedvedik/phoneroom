using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Model;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class FormattedPhoneController : ApiController
    {
       [ResponseType(typeof(PhoneFormatter))]
        // GET: api/FormattedPhone/PhoneNumber
        public PhoneFormatter Get(string phoneNumber)
        {
            string fmtphone = FormatUtil.formatPhoneNumber(phoneNumber);

            return new PhoneFormatter()
            {
                inputPhoneNumber = phoneNumber,
                outputPhoneNumber = fmtphone,
                formatterType = FormatUtil.DatabasePhoneFormatter
            };
        }

    }
}
