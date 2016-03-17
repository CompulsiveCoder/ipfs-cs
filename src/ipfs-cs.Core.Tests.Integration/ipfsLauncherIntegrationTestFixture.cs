using System;
using NUnit.Framework;
using System.Threading;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsLauncherIntegrationTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Start()
		{
			using (var launcher = new ipfsDaemonLauncher ()) {
				launcher.Start ();
				for (int i = 0; i < 20; i++) {
					Thread.Sleep (100);
					Console.Write(".");
				}
				launcher.Close ();
			}
		}
	}
}

