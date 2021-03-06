﻿using System;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsClientPublishIntegrationTestFixture : BaseIntegrationTestFixture
	{
		//[Test]
		public void Test_Publish()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute()
		{
			var tmpFileName = "file.txt";

			var tmpFilePath = Path.GetFullPath(tmpFileName);

			var beforeText = Guid.NewGuid().ToString();

			Console.WriteLine ("Before text: " + beforeText);

			File.WriteAllText (tmpFilePath, beforeText);

			var ipfs = new ipfsClient ();

			ipfs.Init ();

			using (var daemon = ipfs.StartDaemon ()) {
				Thread.Sleep (10000);

				var hash = ipfs.AddFile (tmpFileName);

				Console.WriteLine ("IPFS Hash: " + hash);

				var peerId = ipfs.Publish (hash);

				Console.WriteLine ("Peer ID: " + peerId);

				var fileChecker = new ipfsFileChecker ();

				fileChecker.CheckTestFile ("ipns", peerId, beforeText);

				var afterText = Guid.NewGuid ().ToString ();

				Console.WriteLine ("After text: " + afterText);

				File.WriteAllText (tmpFilePath, afterText);

				Thread.Sleep (2000);

				hash = ipfs.AddFile (tmpFileName);

				Console.WriteLine ("IPFS Hash: " + hash);

				peerId = ipfs.Publish (hash);

				Console.WriteLine ("Peer ID: " + peerId);

				fileChecker.CheckTestFile ("ipns", peerId, afterText);
			}
		}

	}
}

