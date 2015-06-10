﻿namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class myCallback
    {
        [Display(Name = "Callback ID")]
        public int callbackID { get; set; }
        [Display(Name = "Job")]
        public string jobNum { get; set; }
        [Display(Name = "Task")]
        public int taskID { get; set; }
        [Display(Name = "Phone")]
        public string phoneNum { get; set; }
        [Display(Name = "UTC")]
        public int? UTC_code { get; set; }
        [Display(Name = "State")]
        public string Region { get; set; }
        [Display(Name = "Status Date")]
        public DateTime? statusDate { get; set; }
        [Display(Name = "Call Date")]
        public DateTime timeEntered { get; set; }
        [Display(Name = "File Name")]
        public string msgScr { get; set; }
        [Display(Name = "Msg Length")]
        public string msgLength { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        private String _sip; 
        [Display(Name = "Agent")]
        public string SIP {
            get
            {
                    return _sip;
            }
            set
            { if(!String.IsNullOrWhiteSpace(value))
                _sip = value.Trim();
            }
        }
        [Display(Name = "Toll Free")]
        public string tollFreeNumber { get; set; }

        [Display(Name = "Msg Status")]
        public Boolean DisplayStatusContact
        {
            get
            {
                return (!String.IsNullOrWhiteSpace(SIP));
            }
        }
        

        [Display(Name = " MyTime Zone")]
        public string TZ
        {
            get
            {
                return TimeZoneInfo.Local.ToString();
            }
        }
        [Display(Name = "Elaspsed Time Since Call")]
        public string elaspsedTime
        {
            get
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = dt - timeEntered;
                return ts.ToString();
            }
        }
        
        [Display(Name = "Phone #")]
        public string phoneFormatted
        {
            get
            {
                // for testing 
                //phoneNum = "2159181557";
                return String.Format("{0:(###) ###-####}", double.Parse(phoneNum));
            }
        }

        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }


        [Display(Name = "Toll Free #")]
        public string tollFreeFormatted
        {
            get
            {
                if (tollFreeNumber != null)
                    return String.Format("{0:(###) ###-####}", double.Parse(tollFreeNumber));
                else
                    return "";
            }
        }

        [Display(Name = "Caller's Current Time")]
        public DateTime caller_current_time
        {
            get
            {
                DateTime dt = DateTime.Now;

                dt = dt.ToUniversalTime();
                return dt.AddHours((double)UTC_code + (dt.IsDaylightSavingTime() ? 0 : 1));
            }
        }
        public String callbackNum
        {
            get
            {
                // this number gets used to make the call

                string retval; 
                //    retval = !String.IsNullOrEmpty(tollFreeNumber) ? String.Concat("tel:+", tollFreeNumber, phoneNum) : String.Concat("tel:",phoneNum);
                // return retval;
                return String.Concat("tel:", phoneNum);
            }
        }

        public String callbackSubject
        {
            get
            {
                return String.Concat("Job: ", JobFormatted, " TollFreeNumber: ", tollFreeFormatted, "Caller ID:", phoneFormatted);
            }
        }
        [Display(Name = "Topic")]
        public string taskName { get; set; }
        [Display(Name = "Type")]
        public string TaskTypeId { get; set; }

    }
}

