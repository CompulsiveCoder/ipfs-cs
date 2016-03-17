using System;
using NUnit.Framework;
using System.IO;
using ipfs.Core.Tests;

namespace ipfs.Managed.Tests.Integration
{
	[TestFixture]
	public class ipfsManagedClientTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_AddFile()
		{
			var managedClient = new ipfsManagedClient ();
			managedClient.IsVerbose = true;

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

		// TODO: Move to integration test
		//[Test]
		public void Test_Set()
		{
			var managedClient = new ipfsManagedClient ();
			managedClient.IsVerbose = true;

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

		// TODO: Move to integration test
		//[Test]
		public void Test_Append()
		{
			var dataClient = new ipfsManagedClient ();
			dataClient.IsVerbose = true;

			var dataDir = dataClient.DataDirectory;

			var content = "Hello World #1";

			var subFolderName = "TestSubFolder";
			dataClient.Append (subFolderName, "data.txt", content, true);

			var subFolderPath = Path.Combine (dataDir, subFolderName);

			var dataFilePath = Path.Combine (subFolderPath, Path.GetFileName ("data.txt"));

			Assert.IsTrue (File.Exists (dataFilePath));
			Assert.AreEqual (content, File.ReadAllText (dataFilePath).Trim());

			var content2 = "Hello World #2";

			dataClient.Append (subFolderName,"data.txt", content2, true);

			var combinedContent = content + Environment.NewLine + content2;

			var foundContent = File.ReadAllText (dataFilePath).Trim ();

			Console.WriteLine ("Found content:");
			Console.WriteLine (foundContent);

			Assert.AreEqual (combinedContent, foundContent);

		}

		// TODO: Move this to integration tests
		//[Test]
		public void Test_Publish()
		{
			var dataClient = new ipfsManagedClient ();
			dataClient.IsVerbose = true;

			var dataDir = dataClient.DataDirectory;

			var sourceDir = Path.GetFullPath ("source");

			Directory.CreateDirectory (sourceDir);

			var sourceFile = Path.Combine (sourceDir, "file.txt");

			var contents = "Hello world";

			File.WriteAllText (sourceFile, contents);

			var subFolderName = "TestSubFolder";

			var testDirectory = Environment.CurrentDirectory;

			Directory.SetCurrentDirectory (sourceDir);

			var hash = dataClient.AddFile (sourceFile, subFolderName);

			Directory.SetCurrentDirectory (testDirectory);

			var peerId = dataClient.Publish (hash);

			var relativeFilePath = subFolderName + "/" + Path.GetFileName (sourceFile);

			var fileChecker = new ipfsFileChecker ();
			fileChecker.CheckTestFile ("ipns", peerId, relativeFilePath, contents);
		}
	}
}

