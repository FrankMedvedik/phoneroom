
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class JobCallSummary 
    {

        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return JobNumber.Substring(0, 4) + "-" + JobNumber.Substring(4, 4);
            }
        }
        public string JobNumber {get; set;}
        [Display(Name = "Abandoned")]
        public int Abandoned {get; set;}
        [Display(Name = "Callbacks")]
        public int Callback {get; set;}
        [Display(Name = "InQueue")]
        public int InQueue {get; set;}
        [Display(Name = "InboundCalls")]
        public int InboundCalls { get; set; }
        [Display(Name = "LeftMessage ")]
        public int LeftMessage { get; set; }
        [Display(Name = "NoAgentsAvail")]
        public int NoAgents { get; set; }
        [Display(Name = "OutboundCalls")]
        public int OutboundCall { get; set; }
        [Display(Name = "PlacedCalls")]
        public int PlacedCall { get; set; }
        [Display(Name = "TollFreeNumber")]
        public string TollFreeNumber {get; set;}
        }
    }
