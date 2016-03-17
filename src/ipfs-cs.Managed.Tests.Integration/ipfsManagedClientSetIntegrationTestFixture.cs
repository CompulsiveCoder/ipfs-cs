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
	public class ipfsManagedClientSetIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_Set()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute()
		{
			var managedClient = new ipfsManagedClient ();
			managedClient.IsVerbose = true;
			managedClient.Init ();

			var dataDir = managedClient.DataDirectory;

			var content = "Hello World #1";

			var subFolderName = "TestSubFolder";

			managedClient.Set (subFolderName, "data.txt", content);

			var subFolderPath = Path.Combine (dataDir, subFolderName);

			var dataFilePath = Path.Combine (subFolderPath, "data.txt");

			Assert.IsTrue (File.Exists (dataFilePath));
			Assert.AreEqual (content, File.ReadAllText (dataFilePath).Trim());

			var content2 = "Hello World #2";

			managedClient.Set (subFolderName, "data.txt", content2);

			var foundContent = File.ReadAllText (dataFilePath).Trim ();

			Console.WriteLine ("Found content:");
			Console.WriteLine (foundContent);

			Assert.AreEqual (content2, foundContent);
		}
	}
}

