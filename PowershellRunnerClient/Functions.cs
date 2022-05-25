namespace PowershellRunnerClient
{
    internal sealed class Functions
	{
		public static void LogMessage(string message)
		{
			ClientMain.LoggingHost.LogClientMessage(string.Format("[NanoPowershell]: {0}", message));
		}
	}
}