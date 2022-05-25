namespace PowershellRunnerServer
{
	internal sealed class Functions
	{
		public static void LogMessage(string message)
		{
			ServerMain.LoggingHost.LogServerMessage(0, string.Format("[NanoPowershell]: {0}", message));
		}
	}
}