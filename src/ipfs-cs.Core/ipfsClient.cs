using System;
using System.IO;

namespace ipfsecho.Core
{
	public class ipfsClient
	{
		public ipfsClient ()
		{
		}

		public string AddFile(string filePath)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("ipfs add {0}", Path.GetFileName (filePath))
			);

			var hash = ExtractHashAfterAddFile(starter.Output);

			return hash;
		}

		public string ExtractHashAfterAddFile(string output)
		{
			var firstStep = output.Trim ().Substring (output.IndexOf (" ")).TrimStart();

			var hash = firstStep.Substring (0, firstStep.IndexOf (" "));

			return hash;
		}
	}
}

