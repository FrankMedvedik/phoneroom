﻿namespace Phonelogic.Alert.MVVMMessenger
{
    public static class Notifications
    {
        public static string GlobalReportCriteriaChanged { get { return "GlobalReportCriteriaChanged"; } }
        //public static string CallRptRecruiterSet { get { return "CallRptRecruiterSet"; }}
        //public static string CallRptRecruiterCleared{get { return "CallRptRecruiterCleared"; }}

        //public static string JobCallbackRptDateRangeChanged { get { return "JobCallbackRptDateRangeChanged"; } }

        
        /* communicate playback status */
        public static string AudioPlaybackStarted { get { return "AudioPlaybackStarted"; } }
        public static string AudioPlaybackEnded{ get { return "AudioPlaybackEnded"; } }
        public static string DateRangeChanged { get { return "DateRangeChanged"; } }
        public static string PhoneroomChanged { get { return "PhoneroomChanged"; } }
    }
}