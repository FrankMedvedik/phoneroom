using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogic.Repository.Utils
{
    public static class AgentUtils
    {
        public static string GetAgentId(String SIP)
        {
            var db = new PhoneLogicEntities();

            var x = from a in db.agents
                     where a.SIP == SIP
                           select a.agentID;

            return x.FirstOrDefault();
        }
    }
}
