using System;
using NUnit.Framework;
using ipfs.Core;

namespace ipfsecho.Core.Tests
{
	[TestFixture]
	public class ipfsClientTestFixture
	{

		[Test]
		public void Test_ExtractHashAfterAddFile()
		{
			var hash = "QmQzCQn4puG4qu8PVysxZmscmQ5vT1ZXpqo7f58Uh9QfyY";

			var outputLine = "added " + hash + " file.txt";

			var client = new ipfsClient ();

			var extractedHash = client.ExtractHashAfterAddFile (outputLine);

			Assert.AreEqual (hash, extractedHash);
		}
	}
}

