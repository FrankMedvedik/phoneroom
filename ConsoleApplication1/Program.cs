using System;
using System.Linq;
using Microsoft.Win32;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s ="";
        //    var p = new PhoneRoomUsers();
        //    //Console.WriteLine("Before 1st get {0}",DateTime.Now.TimeOfDay);
        //    //IEnumerable<UserPrincipal> users = p.GetAll();
        //    var x = p.GetAllRecruiters();
        //    foreach(Recruiter y in x)
        //        s = s + String.Format("{0} : {1} : {2} : {3}", y.PhoneRoom, y.DisplayName, y.EmailAddress, Environment.NewLine);

        //    s = s + String.Format("Chalfont");
        //    var a = p.GetChalfont();
        //    foreach (var b in a)
        //        s = s + String.Format("{0} : {1} {2}", b.DisplayName, b.EmailAddress, Environment.NewLine);
        //    s = s + String.Format("MKE");
        //    a = p.GetMKE();
        //    foreach (var b in a)
        //        s = s + String.Format("{0} : {1} {2}", b.DisplayName, b.EmailAddress, Environment.NewLine);


        PhoneLogicEntities db = new PhoneLogicEntities();
        PhoneRoomUsers p = new PhoneRoomUsers();

        var jobRecruiters = db.vw_PhoneLogicTaskAgent.ToList();
        var recruiters = p.GetAllRecruiters();

          foreach (var b in jobRecruiters)
                s = s + String.Format("{0} : {1} {2}", b.jobNum, b.sip, Environment.NewLine);
          foreach (var b in recruiters)
               s = s + String.Format("{0} : {1} {2}", b.sip, b.PhoneRoom, Environment.NewLine);
            System.IO.StreamWriter file = new System.IO.StreamWriter("Recruiters.csv");
            file.WriteLine(s);
            file.Close();



            //users = p.GetAll();
            //Console.WriteLine("after 2nd get {0}", DateTime.Now.TimeOfDay);
            //users = p.GetAll();
            //Console.WriteLine("after 3nd get {0}", DateTime.Now.TimeOfDay);
            //users = p.GetAll();
            //Console.WriteLine("after 4nd get {0}", DateTime.Now.TimeOfDay);

            //foreach(var u in users)
            //    Console.WriteLine(u.EmailAddress);
            //Console.WriteLine(DateTime.Now.TimeOfDay);
            //foreach (var u in users)
            //    Console.WriteLine(u.EmailAddress);
            //Console.WriteLine(DateTime.Now.TimeOfDay);
            //foreach (var u in users)
            //    Console.WriteLine(u.EmailAddress);
            //Console.WriteLine(DateTime.Now.TimeOfDay);
   //         Console.ReadKey();
//            p.TestPhoneroomUsersList();
        } 

    }
}
