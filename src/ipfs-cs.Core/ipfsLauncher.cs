using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ipfs.Core
{
	public class ipfsLauncher : Component
	{
		Thread IpfsThread;
		Process IpfsProcess;

		public ipfsLauncher ()
		{
		}

		public void Start()
		{
			IpfsThread = new System.Threading.Thread(StartProcess);

			IpfsThread.Start ();
		}

		public void StartProcess()
		{
			var ipfsScriptPath = Path.GetFullPath ("../../run-ipfs-for-tests.sh");

			var ipfsProcess = new Process ();
			IpfsProcess = ipfsProcess; // TODO: Clean up code
			ipfsProcess.StartInfo = new ProcessStartInfo ("sh", ipfsScriptPath + " >> ipfsdaemon.log");
			//ipfsProcess.StartInfo.WorkingDirectory = dataPath;
			//ipfsProcess.StartInfo.CreateNoWindow = true;

			ipfsProcess.StartInfo.UseShellExecute = true;
			/*ipfsProcess.StartInfo.RedirectStandardInput = true;
			ipfsProcess.StartInfo.RedirectStandardOutput = true;
			ipfsProcess.StartInfo.RedirectStandardError = true;*/
			//ipfsProcess.
			ipfsProcess.Start ();

			//Thread.Sleep (10000);
			//ipfsProcess.Kill ();
		}

		public void Close()
		{
			if (IpfsThread != null) {
			//	IpfsProcess.Kill ();
				IpfsThread.Abort ();
			}
		}

		protected override void Dispose (bool release_all)
		{
			Close ();
			if (IpfsThread != null) {
				IpfsProcess.Dispose ();
				IpfsThread = null;
			}

			base.Dispose (release_all);
		}
	}
}

