//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneLogic.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class returnedCall
    {
        public int callbackID { get; set; }
        public string SIP { get; set; }
        public System.DateTime startCallDate { get; set; }
        public Nullable<System.DateTime> endCallDate { get; set; }
        public string phoneNum { get; set; }
    }
}