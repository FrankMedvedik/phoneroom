using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using PhoneLogic.CallBusinessIntelligence;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace ConsoleApplication1
{
    internal class Program
    {
//        static void Main(string[] args)
//        {
//            string s ="";
//        //    var p = new PhoneRoomUsers();
//        //    //Console.WriteLine("Before 1st get {0}",DateTime.Now.TimeOfDay);
//        //    //IEnumerable<UserPrincipal> users = p.GetAll();
//        //    var x = p.GetAllRecruiters();
//        //    foreach(Recruiter y in x)
//        //        s = s + String.Format("{0} : {1} : {2} : {3}", y.PhoneRoom, y.DisplayName, y.EmailAddress, Environment.NewLine);

//        //    s = s + String.Format("Chalfont");
//        //    var a = p.GetChalfont();
//        //    foreach (var b in a)
//        //        s = s + String.Format("{0} : {1} {2}", b.DisplayName, b.EmailAddress, Environment.NewLine);
//        //    s = s + String.Format("MKE");
//        //    a = p.GetMKE();
//        //    foreach (var b in a)
//        //        s = s + String.Format("{0} : {1} {2}", b.DisplayName, b.EmailAddress, Environment.NewLine);


//        PhoneLogicEntities db = new PhoneLogicEntities();
//        PhoneRoomUsers p = new PhoneRoomUsers();

//        var jobRecruiters = db.vw_PhoneLogicTaskAgent.ToList();
//        var recruiters = p.GetAllRecruiters();

//          foreach (var b in jobRecruiters)
//                s = s + String.Format("{0} : {1} {2}", b.jobNum, b.sip, Environment.NewLine);
//          foreach (var b in recruiters)
//               s = s + String.Format("{0} : {1} {2}", b.sip, b.PhoneRoom, Environment.NewLine);
//            System.IO.StreamWriter file = new System.IO.StreamWriter("Recruiters.csv");
//            file.WriteLine(s);
//            file.Close();



//            //users = p.GetAll();
//            //Console.WriteLine("after 2nd get {0}", DateTime.Now.TimeOfDay);
//            //users = p.GetAll();
//            //Console.WriteLine("after 3nd get {0}", DateTime.Now.TimeOfDay);
//            //users = p.GetAll();
//            //Console.WriteLine("after 4nd get {0}", DateTime.Now.TimeOfDay);

//            //foreach(var u in users)
//            //    Console.WriteLine(u.EmailAddress);
//            //Console.WriteLine(DateTime.Now.TimeOfDay);
//            //foreach (var u in users)
//            //    Console.WriteLine(u.EmailAddress);
//            //Console.WriteLine(DateTime.Now.TimeOfDay);
//            //foreach (var u in users)
//            //    Console.WriteLine(u.EmailAddress);
//            //Console.WriteLine(DateTime.Now.TimeOfDay);
//   //         Console.ReadKey();
////            p.TestPhoneroomUsersList();
//        }

        private static void bMain(string[] args)
        {
            string s = "";
            var v = new RecruiterUtilization();
            var recruiterActivities = v.GetRecruiterActivityRpt("sip:abarrios@reckner.com",
                new DateTime(2015, 10, 28, 10, 0, 0), new DateTime(2015, 10, 28, 17, 0, 0));

            foreach (var b in recruiterActivities)
                s = s +
                    String.Format("{0} , {1},{2} ,{3},{4}, {5}, {6}, {7}", b.DisplayName, b.ActivityType, b.ActivityName,
                        b.StartTime, b.EndTime, StringFormatSvc.DurationFormatted(b.Duration.ToString()),
                        b.Duration, Environment.NewLine);
            System.IO.StreamWriter file = new System.IO.StreamWriter("Recruiters.csv");
            file.WriteLine(s);
            file.Close();

        }

        public static string ListRecruiterActivities(List<RecruiterActivity> activities)
        {
            string s = String.Empty;
            if(activities!=null)
            foreach (var a in activities)
                    s = s + String.Format("{0},{1},{2} ,{3},{4}, {5}, {6}", a.RecruiterSIP, a.ActivityName, a.ActivityType, a.StartTime,a.EndTime,a.Duration,Environment.NewLine);
            return s;
         }

        private static void aMain(string[] args)
        {
            
            var v = new RecruiterUtilization();
            var a = v.PhoneroomTimeSummary("Chalfont Phone Room", new DateTime(2015, 11, 9, 10, 0, 0),
                new DateTime(2015, 11, 9, 12, 0, 0));
            string title = String.Format("{0},{1},{2} ,{3},{4}, {5}, {6}, {7}", "DisplayName", "RecruiterSIP", "CallCnt", "InboundCallCnt", "OutboundCallCnt", "CallTime","IdleTime", Environment.NewLine);
            string activityTitle = String.Format("{0},{1},{2} ,{3},{4}", "DisplayName", "ActivityType", "ActivityName","StartTime", "EndTime");

            string s = title;
            string j = activityTitle;
            foreach (var b in a)
            {
                s = s +
                    String.Format("{0},{1},{2} ,{3},{4}, {5}, {6}, {7}", b.DisplayName, b.RecruiterSIP, b.CallCnt,
                        b.InboundCallCnt, b.OutboundCallCnt, b.TotalCallDurationFormatted, b.TotalIdleDurationFormatted,
                        Environment.NewLine);
                j = j + ListRecruiterActivities(b.RecruiterActivities);
            }
            System.IO.StreamWriter file = new System.IO.StreamWriter("RTS2.csv");
            file.WriteLine(s);
            file.WriteLine(j);
            file.Close();

        }

        private static void Main(string[] args)
        {
            aMain(args);

        }
    }
}
