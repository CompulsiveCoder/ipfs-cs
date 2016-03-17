using System;
using NUnit.Framework;
using System.IO;
using ipfs.Core;
using ipfs.Core.Tests;
using System.Threading;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public class ipfsClientAddFolderIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_AddFolder()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute()
		{
			Console.WriteLine ("Current directory:");
			Console.WriteLine (Environment.CurrentDirectory);

			var exampleFolderName = "example";

			var exampleFolderPath = Path.Combine(Environment.CurrentDirectory, exampleFolderName);

			Console.WriteLine ("Example folder:");
			Console.WriteLine (exampleFolderPath);

			Directory.CreateDirectory (exampleFolderPath);

			var exampleFileName = "file.txt";

			var exampleFilePath = Path.Combine(exampleFolderPath, exampleFileName);

			Console.WriteLine ("Example file:");
			Console.WriteLine (exampleFilePath);

			var text = "Hello world";

			File.WriteAllText (exampleFilePath, text);

			var ipfs = new ipfsClient ();

			ipfs.Init ();

			using (var daemon = ipfs.StartDaemon ()) {
				Thread.Sleep (5000);
				var hash = ipfs.AddFolder (exampleFolderPath);

				new ipfsFileChecker().CheckTestFile ("ipfs", hash, exampleFileName, text);
			}
		}
	}
}

