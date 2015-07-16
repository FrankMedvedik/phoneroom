namespace PhoneLogic.ViewContracts.MVVMMessenger
{
    public static class Notifications
    {
        public static string CleanupNotification { get { return "CLEAN UP!"; }}
        public static string PauseRefresh { get { return "PauseRefresh"; } }
        public static string Refresh { get { return "Refresh"; } }
        
        public static string CallRptJobNumSet { get { return "CallRptJobNumSet"; } }
        public static string CallRptJobNumCleared { get { return "CallRptJobNumCleared"; } }
        public static string CallRptDateRangeChanged {get { return "CallRptDateRangeChanged "; } }

        public static string CallRptRecruiterSet { get { return "CallRptRecruiterSet"; }}
        public static string CallRptRecruiterCleared{get { return "CallRptRecruiterCleared"; }}
        
        public static string CallRptPhoneroomChanged { get { return "CallRptPhoneroomChanged"; } }


        public static string RecruiterCallRptDateRangeChanged { get { return "RecruiterCallRptDateRangeChanged"; } }
        public static string JobCallRptDateRangeChanged { get { return "JobCallRptDateRangeChanged"; } }

    }
}