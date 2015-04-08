using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;

namespace PhoneLogic.Web
{
	[ScriptService]
	[ServiceContract(Namespace = "PhoneLogic.Web")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ShutdownService
	{
		[WebMethod]
		[ScriptMethod]
		[OperationContract]
		public void SaveWork(string identifier)
		{
			Debug.WriteLine("*** Saving work.");
			System.Threading.Thread.Sleep(3000);
			return;
		}

		[WebMethod]
		[ScriptMethod]
		[OperationContract]
		public void DoSomething()
		{
			/* Update your list of signed in users here. */
			Debug.WriteLine("*** DoSomething ");
		}

	}
}
