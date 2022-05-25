using NanoCore;

namespace PowershellRunnerServer
{
    public sealed class ContextCallbacks
	{
		public static void HandlePowershellManagerClick(IClient[] clients, bool @checked)
		{
			if (clients.Length == 0)
			{
				return;
			}
			foreach (IClient client in clients)
			{
				ServerMain.RunnerForm = new RunnerForm(client);
				ServerMain.RunnerForm.Show();
			}
		}
	}
}