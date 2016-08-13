using System;
using System.Collections.Generic;
using System.Reflection;

using log4net;

namespace PhoneLogic.Archiver
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                logger.Debug("error calling archiver");
                Console.WriteLine("parameters required - startdatetime and enddatetime - Format - '01/08/2008 14:50:50.42'");
                return;
            }
            string YearMonth = args[0];
            var lca = new LyncCallArchiver();
            lca.ArchiveCalls(Convert.ToDateTime(args[0]), Convert.ToDateTime(args[1]));

        }
    }
}
