using System;
using NUnit.Framework;
using System.IO;
using ipfs.Core;
using ipfs.Core.Tests;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsClientAddFolderIntegrationTestFixture : BaseIntegrationTestFixture
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

			var hash = ipfs.AddFolder (Environment.CurrentDirectory);

			new ipfsFileChecker().CheckTestFile ("ipfs", hash, "file.txt", text);
		}
	}
}

