using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ipfs.Core
{
	public class ipfsDaemonLauncher : Component
	{
		//Thread IpfsThread;
		//Process IpfsProcess;

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
			ThreadPool.QueueUserWorkItem(delegate {
				StartProcess();
			});
		}

		public void StartProcess()
		{
			Console.WriteLine ("Attempting to start ipfs daemon");

			new ProcessStarter().Start("/bin/bash -c \"ipfs daemon&\"");
		}

		// TODO: Remove if not needed
		/*public void Close()
		{
			if (IpfsThread != null) {
				IpfsThread.Abort ();
			}
		}

		protected override void Dispose (bool release_all)
		{
			Close ();

			if (IpfsProcess != null) {
				IpfsProcess.Kill ();
				IpfsProcess.Dispose ();
				IpfsProcess = null;
			}

			if (IpfsThread != null) {
				IpfsThread = null;
			}

			base.Dispose (release_all);
		}*/

		// TODO: Remove if not needed
		public ipfsClient NewClient()
		{
			return new ipfsClient (IpfsDataPath);
		}
	}
}

