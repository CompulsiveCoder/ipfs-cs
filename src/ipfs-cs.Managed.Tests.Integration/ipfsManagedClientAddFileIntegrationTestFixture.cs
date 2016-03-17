using System;
using NUnit.Framework;
using ipfs.Core;
using System.IO;
using System.Threading;
using ipfs.Core.Tests.Integration;
using ipfs.Managed;

namespace ipfs.Managed.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsManagedClientAddFileIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_AddFile()
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

			File.WriteAllText (sourceFile, "Hello world");

			var subFolderName = "TestSubFolder";
			managedClient.AddFile (sourceFile, subFolderName);

			var importedFolderPath = Path.Combine (dataDir, subFolderName);

			var importedFilePath = Path.Combine (importedFolderPath, Path.GetFileName (sourceFile));

			Assert.IsTrue (File.Exists (importedFilePath));
		}
	}
}

