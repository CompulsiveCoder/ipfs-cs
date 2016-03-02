using System;
using NUnit.Framework;
using ipfs.Core;
using System.IO;
using System.Threading;

namespace ipfs.Core.Tests
{
	[TestFixture]
	public class ipfsClientIntegrationTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_AddFile()
		{
			var tmpFileName = "file.txt";

			var tmpFile = Path.GetFullPath(tmpFileName);

			var text = "Hello world";

			File.WriteAllText (tmpFile, text);

			var ipfs = new ipfsClient ();

			var hash = ipfs.AddFile (tmpFileName);

			new ipfsFileChecker().CheckTestFile ("ipfs", hash, text);
		}

		[Test]
		public void Test_AddFolder()
		{
			var tmpFileName = "file.txt";

			var tmpFile = Path.GetFullPath(tmpFileName);

			var text = "Hello world";

			File.WriteAllText (tmpFile, text);

			var ifps = new ipfsClient ();

			var hash = ifps.AddFolder (Environment.CurrentDirectory);

			new ipfsFileChecker().CheckTestFile ("ipfs", hash, "file.txt", text);
		}

		[Test]
		public void Test_Publish()
		{
			var tmpFileName = "file.txt";

			var tmpFilePath = Path.GetFullPath(tmpFileName);

			var beforeText = Guid.NewGuid().ToString();

			Console.WriteLine ("Before text: " + beforeText);

			File.WriteAllText (tmpFilePath, beforeText);

			var ipfs = new ipfsClient ();

			var hash = ipfs.AddFile (tmpFileName);

			Console.WriteLine ("IPFS Hash: " + hash);

			var peerId = ipfs.Publish (hash);

			Console.WriteLine ("Peer ID: " + peerId);

			var fileChecker = new ipfsFileChecker ();

			fileChecker.CheckTestFile ("ipns", peerId, beforeText);

			var afterText = Guid.NewGuid().ToString();

			Console.WriteLine ("After text: " + afterText);

			File.WriteAllText (tmpFilePath, afterText);

			hash = ipfs.AddFile (tmpFileName);

			Console.WriteLine ("IPFS Hash: " + hash);

			peerId = ipfs.Publish (hash);

			Console.WriteLine ("Peer ID: " + peerId);

			fileChecker.CheckTestFile ("ipns", peerId, afterText);
		}


		/*[Test]
		public void Test_ExtractHashAfterAddFile()
		{
			var hash = "QmQzCQn4puG4qu8PVysxZmscmQ5vT1ZXpqo7f58Uh9QfyY";

			var outputLine = "added " + hash + " file.txt";

			var client = new ipfsClient ();

			var extractedHash = client.ExtractHashAfterAddFile (outputLine);

			Assert.AreEqual (hash, extractedHash);
		}


		[Test]
		public void Test_ExtractHashAfterAddFolder()
		{
			var hash = "QmQzCQn4puG4qu8PVysxZmscmQ5vT1ZXpqo7f58Uh9QfyY";

			var output = "added QmNRCQWfgze6AbBCaT1rkrkV5tJ2aP4oTNPb5JZcXYywve ipfs-data/TestSubFolder/file.txt\nadded QmdwbpwH3Z3gFcpkJg11fzpwe7PjNq9bEgNveWyDk4pqwY ipfs-data/TestSubFolder\nadded " + hash + " ipfs-data\n\n\t\t\t11 B  +Inf % 0\u001b[2K\n\t\t\t\t\u001b[2K\n\t\t\t\t\t\u001b[2K\n\n";

			var client = new ipfsClient ();

			var extractedHash = client.ExtractHashAfterAddFolder (output);

			Assert.AreEqual (hash, extractedHash);
		}

		[Test]
		public void Test_ExtractHashAfterPublish()
		{
			var hash = "QmeBPLVyvWcnGk3n6c5vapXzbkMH3ZQQiwhnnfTRdcSGMr";

			var outputLine = "Published to " + hash + ": QmT78zSuBmuS4z925WZfrqQ1qHaJ56DQaTfyMUF7F8ff5o";

			var client = new ipfsClient ();

			var extractedHash = client.ExtractHashAfterPublish (outputLine);

			Assert.AreEqual (hash, extractedHash);
		}*/
	}
}

