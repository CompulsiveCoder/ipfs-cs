using System;
using NUnit.Framework;
using ipfs.Core;
using System.IO;
using System.Threading;
using ipfs.Core.Tests.Integration;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsClientAddFileIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_AddFile()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute()
		{
			var tmpFileName = "file.txt";

			var tmpFile = Path.GetFullPath(tmpFileName);

			Console.WriteLine ("Tmp file:");
			Console.WriteLine (tmpFile);

			var text = "Hello world";

			File.WriteAllText (tmpFile, text);

			var ipfs = new ipfsClient ();

			ipfs.Init ();

			using (var daemon = ipfs.StartDaemon ()) {
				Thread.Sleep (10000);
				var hash = ipfs.AddFile (tmpFileName);
				new ipfsFileChecker ().CheckTestFile ("ipfs", hash, text);
			}
		}
	}
}

