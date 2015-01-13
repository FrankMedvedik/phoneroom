using System;
using PhoneLogic.Model;
using PhoneLogic.UserAuth;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new PhoneRoomUsers();
            //Console.WriteLine("Before 1st get {0}",DateTime.Now.TimeOfDay);
            //IEnumerable<UserPrincipal> users = p.GetAll();

            var x = p.GetAllRecruiters();
            foreach(Recruiter y in x)
                Console.WriteLine("{0} : {1}", y.PhoneRoom, y.EmailAddress);
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
            Console.ReadKey();
//            p.TestPhoneroomUsersList();
        } 
    }
}
