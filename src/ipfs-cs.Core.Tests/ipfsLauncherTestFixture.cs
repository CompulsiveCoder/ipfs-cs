using System;
using NUnit.Framework;
using System.Threading;

namespace ipfs.Core.Tests
{
	[TestFixture]
	public class ipfsLauncherTestFixture
	{
		[Test]
		public void Test_Start()
		{
			using (var launcher = new ipfsLauncher ()) {
				launcher.Start ();
				Thread.Sleep (10000);
				launcher.Close ();
			}
		}
	}
}

