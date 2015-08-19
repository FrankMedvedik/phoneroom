using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class PhoneroomRecruiterJob
    {
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        public string jobNum { get; set; }
        public int taskID { get; set; }
        public string taskName { get; set; }
        public string taskDscr { get; set; }
        public string taskTypeID { get; set; }
        public string sip { get; set; }
        [Display(Name = "Name")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }
        [Display(Name = "Title")]
        public String Description { get; set; }
        public String tollFreeNumber{ get; set; }


        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(tollFreeNumber);
            }
        }


    }
}