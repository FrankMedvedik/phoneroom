using System.DirectoryServices.AccountManagement; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using PhoneLogic.Model;

namespace PhoneLogic.UserAuth
{
    public  class PhoneRoomUsers
    {
           private ObjectCache cache;
           private CacheItemPolicy policy;
           private IEnumerable<UserPrincipal> ChalfontUsers;
           private IEnumerable<UserPrincipal> MKEUsers;
           private IEnumerable<UserPrincipal> AllUsers;

        public IEnumerable<Recruiter> GetAllRecruiters()
        {
            List<Recruiter> recruiters = new List<Recruiter>();
            GetAll();
            GetChalfont();
            GetMKE();


            foreach (var r in AllUsers)
            {
                if (r.EmailAddress != null)
                {
                    if (ChalfontUsers.Contains(r))
                        recruiters.Add(new Recruiter()
                        {
                            EmailAddress = r.EmailAddress,
                            Description = r.Description,
                            DisplayName = r.DisplayName,
                            PhoneRoom = "Chalfont",
                        });
                    else if (MKEUsers.Contains(r))
                        recruiters.Add(new Recruiter()
                        {
                            EmailAddress = r.EmailAddress,
                            Description = r.Description,
                            DisplayName = r.DisplayName,
                            PhoneRoom = "Milwaukee",
                        });
                    else
                        recruiters.Add(new Recruiter()
                        {
                            EmailAddress = r.EmailAddress,
                            Description = r.Description,
                            DisplayName = r.DisplayName,
                            PhoneRoom = "Unknown",
                        });
                }
            }
            return recruiters;
        }

        public PhoneRoomUsers()
        {
               cache = MemoryCache.Default;
               policy = new CacheItemPolicy
               {
                   AbsoluteExpiration =
                       DateTimeOffset.Now.AddSeconds(Convert.ToInt32(UserAuth.Properties.Resources.ADCacheDuration))
               };
               ChalfontUsers = cache["ChalfontUsers"] as IEnumerable<UserPrincipal> ;
               MKEUsers = cache["MKEUsers"] as IEnumerable<UserPrincipal> ;
               AllUsers = cache["Allsers"] as IEnumerable<UserPrincipal> ;
        }
        #if DEBUGLOCAL
                private PrincipalContext ctx = new PrincipalContext(ContextType.Domain,"reckner.com","fmedvedik","~solar~53");
        #else
                private PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
        #endif

        public Boolean IsSupervisor(string userName)
        {
            var src = UserPrincipal.FindByIdentity(ctx, userName).GetGroups(ctx);
            var result = new List<string>();
            src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
            var l = result.FirstOrDefault(s => s.Equals( UserAuth.Properties.Resources.PhoneRoomSupervisors));
            return (l != "");
        }
        
        public IEnumerable<UserPrincipal> GetAll()
        {
            if (AllUsers == null)
            {
                AllUsers = GetGroupUserList(UserAuth.Properties.Resources.AllPhoneRoom);
                cache.Set("AllUsers", AllUsers, policy);
            }
            return AllUsers;
        }

        public IEnumerable<UserPrincipal> GetChalfont()
        {
            if (ChalfontUsers == null)
            {
                ChalfontUsers = GetGroupUserList(UserAuth.Properties.Resources.ChalfontPhoneRoom);
                cache.Set("ChalfontUsers", ChalfontUsers, policy);
            }
            return ChalfontUsers;
        }

        public IEnumerable<UserPrincipal> GetMKE()
        {
            if (MKEUsers == null)
            {
                MKEUsers = GetGroupUserList(UserAuth.Properties.Resources.MKEPhoneRoom);
                cache.Set("MKEUsers", MKEUsers, policy);
            }
            return MKEUsers;
        }

        private IEnumerable<UserPrincipal> GetGroupUserList(String groupName)
        {
            var users = new List<UserPrincipal>();
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, groupName );
            if (group != null)
            {
                // iterate over members
                foreach (Principal p in group.GetMembers())
                {
                    if (p.StructuralObjectClass == "user")
                        users.Add((UserPrincipal) p);
                }
            }
            return users;
        }

        private void PrintList(string groupName)
        {
            IEnumerable<UserPrincipal> lst = GetGroupUserList(groupName);
            foreach (var usr in lst)
                Console.WriteLine("{0}:{1}:{2}", usr.DisplayName, usr.DistinguishedName, usr.Description);
        }

        public void TestPhoneroomUsersList()
        {
            PrintList(UserAuth.Properties.Resources.MKEPhoneRoom);
            Console.ReadKey();

            PrintList(UserAuth.Properties.Resources.ChalfontPhoneRoom);
            Console.ReadKey();

            PrintList(UserAuth.Properties.Resources.AllPhoneRoom);
            Console.ReadKey();

        }


    }
}

