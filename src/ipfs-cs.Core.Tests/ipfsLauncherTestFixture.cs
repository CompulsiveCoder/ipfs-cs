using System;
using NUnit.Framework;
using System.Threading;

namespace ipfs.Core.Tests
{
	[TestFixture]
	public class ipfsLauncherTestFixture
	{
		// TODO: This test is slow so it's disabled
		//[Test]
		public void Test_Start()
		{
			using (var launcher = new ipfsDaemonLauncher ()) {
				launcher.Start ();
				Thread.Sleep (5000);
				launcher.Close ();
			}
		}
	}
}

