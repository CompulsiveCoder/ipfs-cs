using System;
using NUnit.Framework;
using ipfs.Core;
using System.IO;
using System.Threading;
using ipfs.Core.Tests.Integration;

namespace ipfs.Core.Tests.Integration
{
	[TestFixture]
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

			var text = "Hello world";

			File.WriteAllText (tmpFile, text);

			var ipfs = new ipfsClient ();
			ipfs.IpfsDataPath = "/.ipfs-data";

			var hash = ipfs.AddFile (tmpFileName);

			new ipfsFileChecker().CheckTestFile ("ipfs", hash, text);
		}
	}
}

