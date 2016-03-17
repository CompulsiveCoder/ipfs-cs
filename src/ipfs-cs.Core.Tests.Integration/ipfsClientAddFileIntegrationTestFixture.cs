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
			// Prepare for the test
			var tmpFileName = "file.txt";

			var tmpFile = Path.GetFullPath(tmpFileName);

			Console.WriteLine ("Tmp file:");
			Console.WriteLine (tmpFile);

			var text = "Hello world";

			File.WriteAllText (tmpFile, text);

			// Start the test
			var ipfs = new ipfsClient ();

			ipfs.Init ();

			using (var daemon = ipfs.StartDaemon ()) {
				// TODO: Fixture out a way to reduce this duration
				Thread.Sleep (10000); // Sleep to let the daemon start, otherwise an error may occur
				var hash = ipfs.AddFile (tmpFileName);
				new ipfsFileChecker ().CheckTestFile ("ipfs", hash, text);
			}
		}
	}
}
