<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Web.Services"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod] 
    public static void SignOff(string identifier)
    {
        /* Update your list of signed in users here. */
        System.Diagnostics.Debug.WriteLine("*** Signing Off: " + identifier);
    }
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript" src="Silverlight.js"></script>
<script type="text/javascript">
        function OnClose() {      
            var plugin = document.getElementById("silverlightControl");
            var shutdownManager = plugin.content.ShutdownManager;
            var shutdownResult = shutdownManager.Shutdown();
            if (shutdownResult) {
                event.returnValue = shutdownResult;
            }
            else {
                var answer = confirm("Close application?");
                if (!answer) {
                    event.returnValue = "To prevent closing the application select Cancel";
                }
            }
        }

        function NotifyWebserviceOnClose(identifier) {
            window.PageMethods.SignOff(identifier, OnSignOffComplete);            
        }

        function OnSignOffComplete(result) {
        }
        
        function OnShutdownComplete(result) {
        }
        
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
              appSource = sender.getHost().Source;
            }
            
            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
              return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " +  appSource + "\n" ;

            errMsg += "Code: "+ iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {           
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " +  args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
    <title>PhoneLogic.Inbound</title>
    <style type="text/css">
    html, body {
        height: 100%;
        overflow: auto;
    }
    body {
        padding: 0;
        margin: 0;
    }
    #silverlightControlHost {
        height: 100%;
        text-align:center;
    }
    </style>
</head>
<body onbeforeunload="OnClose()">
    <form id="form1" runat="server" style="height:100%">
    <div id="silverlightControlHost">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            <Services>
                <asp:ServiceReference Path="ShutdownService.svc" />
            </Services>
        </asp:ScriptManager>
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" 
            id="silverlightControl"
            width="100%" height="100%">
          <param name="source" value="ClientBin/PhoneLogic.Inbound.xap"/>
          <param name="onError" value="onSilverlightError" />
          <param name="background" value="white" />
          <param name="minRuntimeVersion" value="3.0.40624.0" />
          <param name="autoUpgrade" value="true" />
          <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
              <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
          </a>
        </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
    </form>
</body>
</html>
