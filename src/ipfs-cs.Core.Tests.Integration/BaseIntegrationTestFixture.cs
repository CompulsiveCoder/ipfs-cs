using System;
using NUnit.Framework;
using System.IO;

namespace ipfs.Core.Tests.Integration
{
	[TestFixture]
	public abstract class BaseIntegrationTestFixture
	{
		public string OriginalDirectory = "";

		public bool DeleteTemporaryDirectory = false;

		public BaseIntegrationTestFixture()
		{}

		[SetUp]
		public void SetUp()
		{
			OriginalDirectory = Environment.CurrentDirectory;

			//var tmpDir = new TemporaryDirectoryCreator ().Create ();

			//Directory.SetCurrentDirectory (tmpDir);

			Console.WriteLine ("Current directory:");
			Console.WriteLine (Environment.CurrentDirectory);
			Console.WriteLine ();
		}

		[TearDown]
		public void TearDown()
		{
			var tmpDir = Environment.CurrentDirectory;

			Directory.SetCurrentDirectory (OriginalDirectory);

			if (DeleteTemporaryDirectory && tmpDir.ToLower().Contains(".tmp")) {
				Directory.Delete (tmpDir, true);
			}
		}

		public abstract void Execute();
	}
}

