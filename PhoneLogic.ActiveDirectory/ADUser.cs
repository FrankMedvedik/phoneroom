//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Text;
//using System.Threading.Tasks;
//using LinqToLdap.Mapping;

//namespace PhoneLogic.ActiveDirectory
//{
//    public class User
//    {
//        public const string NamingContext = "CN=Users,CN=Employees,DC=Northwind,DC=local";

//        public string DistinguishedName { get; set; }
//        public string CommonName { get; set; }
//        public Guid Guid { get; set; }
//        public SecurityIdentifier Sid { get; set; }
//        public string Title { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public DateTime WhenCreated { get; set; }
//        public DateTime LastChanged { get; set; }

//        public void SetDistinguishedName()
//        {
//            DistinguishedName = string.Format("CN={0},{1}", CommonName, NamingContext);
//        }
//    }

//    [DirectorySchema(NamingContext, ObjectCategory = "Person", ObjectClass = "User")]
//    public class User
//    {
//        public const string NamingContext = "CN=Users,CN=Employees,DC=Northwind,DC=local";

//        [DistinguishedName]
//        public string DistinguishedName { get; set; }

//        [DirectoryAttribute("cn", ReadOnly = true)]
//        public string CommonName { get; set; }

//        [DirectoryAttribute("objectguid", StoreGenerated = true)]
//        public Guid Guid { get; set; }

//        [DirectoryAttribute("objectsid", StoreGenerated = true)]
//        public SecurityIdentifier Sid { get; set; }

//        [DirectoryAttribute]
//        public string Title { get; set; }

//        [DirectoryAttribute("givenname")]
//        public string FirstName { get; set; }

//        [DirectoryAttribute("sn")]
//        public string LastName { get; set; }

//        [DirectoryAttribute(StoreGenerated = true)]
//        public DateTime WhenCreated { get; set; }

//        [DirectoryAttribute(StoreGenerated = true)]
//        public DateTime WhenChanged { get; set; }

//        public void SetDistinguishedName()
//        {
//            DistinguishedName = string.Format("CN={0},{1}", CommonName, NamingContext);
//        }
//    }
//}