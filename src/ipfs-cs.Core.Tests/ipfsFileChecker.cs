using System;
using System.Threading;
using NUnit.Framework;

namespace ipfs.Core.Tests
{
	public class ipfsFileChecker
	{
		public int MaxTries = 100;

		public int DelayBetweenTries = 2000;

		public ipfsFileChecker ()
		{
		}

		public void CheckTestFile(string protocol, string hash, string expectedContent)
		{
			var url = "http://localhost:8080/" + protocol + "/" + hash;

			CheckTestFile (url, expectedContent);
		}

		public void CheckTestFile(string protocol, string peerId, string relativeFilePath, string expectedContent)
		{
			var url = "http://localhost:8080/" + protocol + "/" + peerId + "/" + relativeFilePath.TrimStart('/');

			CheckTestFile (url, expectedContent);
		}

		public void CheckTestFile(string url, string expectedContent)
		{
			Console.WriteLine ("Checking test file...");
			Console.WriteLine ("");
			Console.WriteLine ("URL:");
			Console.WriteLine (url);
			Console.WriteLine ("");
			Console.WriteLine ("Expected content:");
			Console.WriteLine (expectedContent);
			Console.WriteLine ("");

			bool foundMatch = false;

			int currentTry = 1;

			// Keep retrying as it takes time for changes to propagate
			while (!foundMatch
				&& currentTry <= MaxTries)
			{
				Console.WriteLine ("Try #" + currentTry);

				var starter = new ProcessStarter ();
				starter.Start ("curl -s " + url);

				var outputText = starter.Output.Trim ();

				Console.WriteLine ("Found contents:");
				Console.WriteLine ("\"" + (String.IsNullOrEmpty(outputText) ? "[empty]" : outputText)  + "\"");

				if (outputText.Trim() == expectedContent.Trim()) {
					foundMatch = true;
					Console.WriteLine ("Successful match");
				} else
					Console.WriteLine ("Failed to match");

				currentTry++;

				Thread.Sleep (DelayBetweenTries);
			}

			Assert.IsTrue (foundMatch);
		}

	}
}

