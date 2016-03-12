using System;
using NUnit.Framework;
using System.IO;
using ipfs.Core;
using ipfs.Core.Tests;

namespace ipfs.Core.Tests.Integration
{
	[TestFixture]
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

			var ifps = new ipfsClient ();

			var hash = ifps.AddFolder (Environment.CurrentDirectory);

			new ipfsFileChecker().CheckTestFile ("ipfs", hash, "file.txt", text);
		}
	}
}

