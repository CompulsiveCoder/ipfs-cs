using System;
using NUnit.Framework;
using ipfs.Core;
using System.IO;
using System.Threading;
using ipfs.Core.Tests.Integration;
using ipfs.Managed;
using ipfs.Core.Tests;

namespace ipfs.Managed.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsManagedClientPublishIntegrationTestFixture : BaseIntegrationTestFixture
	{
		// TODO: Fix/overhaul test and re-enable
		//[Test]
		public void Test_Publish()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute()
		{
			var managedClient = new ipfsManagedClient ();
			managedClient.IsVerbose = true;
			managedClient.Init ();

			var dataDir = managedClient.DataDirectory;

			var sourceDir = Path.GetFullPath ("source");

			Directory.CreateDirectory (sourceDir);

			var sourceFile = Path.Combine (sourceDir, "file.txt");

			var contents = "Hello world";

			File.WriteAllText (sourceFile, contents);

			var subFolderName = "TestSubFolder";

			var testDirectory = Environment.CurrentDirectory;

			Directory.SetCurrentDirectory (sourceDir);

			var hash = managedClient.AddFile (sourceFile, subFolderName);

			Directory.SetCurrentDirectory (testDirectory);

			var peerId = managedClient.Publish (hash);

			var relativeFilePath = subFolderName + "/" + Path.GetFileName (sourceFile);

			var fileChecker = new ipfsFileChecker ();
			fileChecker.CheckTestFile ("ipns", peerId, relativeFilePath, contents);
		}
	}
}

