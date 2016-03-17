using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ipfs.Core
{
	public class ipfsDaemonLauncher : Component
	{
		Thread IpfsThread;
		Process IpfsProcess;

		public string IpfsDataPath { get;set; }

		public ipfsDaemonLauncher ()
		{
			IpfsDataPath = Path.GetFullPath(".ipfs-data");
		}

		public ipfsDaemonLauncher (string ipfsDataPath)
		{
			IpfsDataPath = ipfsDataPath;
		}

		public void Start()
		{
			//StartProcess ();
			IpfsThread = new System.Threading.Thread(StartProcess);

			IpfsThread.Start ();
		}

		public void StartProcess()
		{
			Console.WriteLine ("Attempting to start ipfs daemon");
			//var ipfsScriptPath = Path.GetFullPath ("../../run-ipfs-for-tests.sh");

			var ipfsProcess = new Process ();
			IpfsProcess = ipfsProcess; // TODO: Clean up code

			//ipfsProcess.StartInfo = new ProcessStartInfo ("/bin/bash", "-c \"screen ipfs daemon\"");

			// TODO: Overhaul this function or remove it

			ipfsProcess.StartInfo = new ProcessStartInfo (
				//"/bin/bash", " -c \"cd ../../ && sh run-ipfs-for-test.sh\""
				"ipfs", "daemon"
			);

			//ipfsProcess.StartInfo.WorkingDirectory = dataPath;
			//ipfsProcess.StartInfo.CreateNoWindow = true;

			//ipfsProcess.StartInfo.UseShellExecute = false;
			//ipfsProcess.StartInfo.RedirectStandardInput = true;
			//ipfsProcess.StartInfo.RedirectStandardOutput = true;
			//ipfsProcess.StartInfo.RedirectStandardError = true;
			//ipfsProcess.
			ipfsProcess.Start ();
			ipfsProcess.WaitForExit ();
			//Console.WriteLine (ipfsProcess.StandardOutput.ReadToEnd ());
			//Console.WriteLine (File.ReadAllText (Path.GetFullPath ("ipfs.log")));
			//ipfsProcess.WaitForExit ();
			//Thread.Sleep(5000);
			//Thread.Sleep (10000);
			//ipfsProcess.Kill ();
		}

		public void Close()
		{
			if (IpfsThread != null) {
				IpfsThread.Abort ();
			}
		}

		protected override void Dispose (bool release_all)
		{
			Close ();

			if (IpfsProcess != null) {
				IpfsProcess.Dispose ();
				IpfsProcess = null;
			}

			if (IpfsThread != null) {
				IpfsThread = null;
			}

			base.Dispose (release_all);
		}

		public ipfsClient NewClient()
		{
			return new ipfsClient (IpfsDataPath);
		}
	}
}

