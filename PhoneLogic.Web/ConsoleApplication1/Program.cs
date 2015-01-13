using PhoneLogic.Repository;
using System;
using System.Threading;
using PhoneLogic.Model;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleApplication1
{


    //class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //            var plc = new PhoneLogicContext()
    //            {
    //                callerId = "2159181557",
    //                conversationId = "{274F9F1E-3340-4580-BA30-30ADC0BCFA47}",
    //                dialedNumber = "8005551212",
    //                jobNumber = "2014001",
    //                timeReceived = "9/10/2014 12:27:04"
    //            };
                        
    //                     Console.Write(JsonConvert.SerializeObject(plc));
    //        }
    //    }



    /* test the audio converions tool */

    class Program
    {
        public static void Main(string[] args)
        {
            var db = new PhoneLogicEntities();
            var md = new MsgDispatcher();
            var oldestFileDate = DateTime.Now.AddDays(-30); ;

            var newCallbacks = (
                from c in db.callbacks
                where c.msgLength == null 
                //&& c.timeEntered > oldestFileDate
                select c);

            foreach (var c in newCallbacks)
            {
                string msg = md.PrepMessage(c.callbackID);
                Console.Write("Audio file conversion output: " + msg);
            }
        }
    }
//class TimerExample
//{
//    static void Main()
//    {
//        // Create an event to signal the timeout count threshold in the 
//        // timer callback.
//        AutoResetEvent autoEvent     = new AutoResetEvent(false);

//        StatusChecker  statusChecker = new StatusChecker(10);

//        // Create an inferred delegate that invokes methods for the timer.
//        TimerCallback tcb = statusChecker.CheckStatus;

//        // Create a timer that signals the delegate to invoke  
//        // CheckStatus after one second, and every 1/4 second  
//        // thereafter.
//        Console.WriteLine("{0} Creating timer.\n", 
//            DateTime.Now.ToString("h:mm:ss.fff"));
//        Timer stateTimer = new Timer(tcb, autoEvent, 1000, 250);

//        // When autoEvent signals, change the period to every 
//        // 1/2 second.
//        autoEvent.WaitOne(5000, false);
//        stateTimer.Change(0, 500);
//        Console.WriteLine("\nChanging period.\n");

//        // When autoEvent signals the second time, dispose of  
//        // the timer.
//        autoEvent.WaitOne(5000, false);
//        stateTimer.Dispose();
//        Console.WriteLine("\nDestroying timer.");
//    }
//}

//class StatusChecker
//{
//    private int invokeCount;
//    private int  maxCount;

//    public StatusChecker(int count)
//    {
//        invokeCount  = 0;
//        maxCount = count;
//    }

//    // This method is called by the timer delegate. 
//    public void CheckStatus(Object stateInfo)
//    {
//        AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
//        Console.WriteLine("{0} Checking status {1,2}.", 
//            DateTime.Now.ToString("h:mm:ss.fff"), 
//            (++invokeCount).ToString());

//        if(invokeCount == maxCount)
//        {
//            // Reset the counter and signal Main.
//            invokeCount  = 0;
//            autoEvent.Set();
//        }
//    }}
}
    

