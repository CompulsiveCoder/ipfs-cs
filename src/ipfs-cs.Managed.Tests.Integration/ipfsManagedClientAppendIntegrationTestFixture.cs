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
	public class ipfsManagedClientAppendIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_Append()
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
			managedClient.Append (subFolderName, "data.txt", content, true);

			var subFolderPath = Path.Combine (dataDir, subFolderName);

			var dataFilePath = Path.Combine (subFolderPath, Path.GetFileName ("data.txt"));

			Assert.IsTrue (File.Exists (dataFilePath));
			Assert.AreEqual (content, File.ReadAllText (dataFilePath).Trim());

			var content2 = "Hello World #2";

			managedClient.Append (subFolderName,"data.txt", content2, true);

			var combinedContent = content + Environment.NewLine + content2;

			var foundContent = File.ReadAllText (dataFilePath).Trim ();

			Console.WriteLine ("Found content:");
			Console.WriteLine (foundContent);

			Assert.AreEqual (combinedContent, foundContent);
		}
	}
}

