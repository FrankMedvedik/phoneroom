using System;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using PhoneLogic.Archiver.Properties;
using PhoneLogic.EF;

namespace PhoneLogic.Archiver
{
    public  class LyncCallArchiver
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private PhoneLogicEntities ctx;
        public String YearMonth { get; set; }


        public string LiveRecordingFilePath
        {
            get { return Settings.Default.LiveRecordingsBasePath; }
        }

        public string GetSubPath(DateTime startCallTime)
        {
            return string.Format(@"{0}{1}\", startCallTime.Year, startCallTime.Month.ToString().PadLeft(2,'0'));   
        }

        public LyncCallArchiver()
        {
            ctx = new PhoneLogicEntities();
         
        }

        public void ArchiveCalls(DateTime StartDate, DateTime EndDate)
        {
            int rowsTOProcess = ctx.LyncCallLogHists.Count(a => a.CallStartTime >= StartDate && a.CallStartTime <= EndDate && a.CallFile == null);
            logger.Debug(string.Format("Start Date {0}", StartDate));
            logger.Debug(string.Format("End Date  {0}", EndDate ));
            logger.Debug(string.Format("Number of Records to archive {0}", rowsTOProcess));
            //Console.WriteLine("Press any key to start...");
            //Console.ReadKey();
            ProcessDateRange(StartDate, EndDate);
        }

        private void ProcessDateRange(DateTime StartDate, DateTime EndDate)
        {
            using (ctx)
            {
                var calls = ctx.LyncCallLogHists.Where(a => a.CallStartTime >= StartDate && a.CallStartTime <= EndDate && a.CallFile == null 
                && a.CallerId == "2672550067");
                foreach (var call in calls)
                {
                    ArchiveCallFile(call);
                }
               ctx.SaveChanges();
            }
            

        }

        private void ArchiveCallFile(LyncCallLogHist call)
        {
            string srcFile = String.Empty;
            try
            {
                call.CallFile = Path.GetFullPath(string.Format(@"{0}{1}{2}", 
                    GetSubPath(call.CallStartTime.GetValueOrDefault()), call.CallID, ".wma"));

                //srcFile = Path.GetFullPath(String.Format(@"{0}{1}{2}",LiveRecordingFilePath,call.CallID,".wma"));
                srcFile = Path.GetFullPath(String.Format(@"{0}{1}{2}", LiveRecordingFilePath, "00013213-2a62-41de-b52e-730a75428609", ".wma"));

                logger.Debug(string.Format("Target file is {0}", call.CallFile));
                logger.Debug(string.Format("source file is {0}", srcFile));
                MoveCallFile(srcFile, call.CallFile);
            }
            catch (Exception e)
            {
                logger.Error(string.Format("ERROR {0}", e));
                logger.Error(string.Format("Target file is {0}", call.CallFile));
                logger.Error(string.Format("source file is {0}", srcFile));
            }
            
        }


        private void MoveCallFile(string src, string tgt)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(tgt));
            File.Copy(src, tgt);
        }

    }
}