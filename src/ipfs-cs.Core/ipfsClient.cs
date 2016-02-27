using System;
using System.IO;
using ipfs.Core;

namespace ipfs.Core
{
	public class ipfsClient
	{
		public ipfsClient ()
		{}

		public string AddFile(string filePath)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("ipfs add {0}", Path.GetFileName (filePath))
			);

			var hash = ExtractHashAfterAddFile(starter.Output);

			return hash;
		}

		public string AddFolder (string folder)
		{
			var starter = new ProcessStarter ();

			//var originalDirectory = Environment.CurrentDirectory;

			//Directory.SetCurrentDirectory (folder);

			starter.Start ("ipfs add -r " + folder);

			//Directory.SetCurrentDirectory (originalDirectory);

			var hash = ExtractHashAfterAddFolder(starter.Output);

			return hash;
		}

		public string Publish(string hash)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("ipfs name publish {0}", hash)
			);

			var peerId = ExtractHashAfterPublish(starter.Output);

			//starter.Start (
			//	String.Format ("ipfs name resolve {0}", peerId)
			//);

			return peerId;
		}

		public string ExtractHashAfterAddFile(string output)
		{
			var beginningRemoved = output.Trim ().Substring (output.IndexOf (" ")).TrimStart();

			var hash = beginningRemoved.Substring (0, beginningRemoved.IndexOf (" "));

			return hash;
		}


		public string ExtractHashAfterAddFolder(string output)
		{
			var lines = output.Trim().Split ('\n');

			var lastLine = lines [lines.Length - 1];

			var beginningRemoved = lastLine.Trim ().Substring (lastLine.IndexOf (" ")).TrimStart();

			var hash = beginningRemoved.Substring (0, beginningRemoved.IndexOf (" "));

			return hash;
		}
			
		public string ExtractHashAfterPublish(string output)
		{
			var firstStep = output.Trim ().Replace ("Published to ", "");

			var hash = firstStep.Substring (0, firstStep.IndexOf (":"));

			return hash;
		}
	}
}

