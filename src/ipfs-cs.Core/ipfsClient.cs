﻿using System;
using System.IO;
using ipfs.Core;

namespace ipfs.Core
{
	public class ipfsClient
	{
		public string IpfsCommand = "/usr/local/bin/ipfs";

		public ipfsClient ()
		{}

		public string AddFile(string filePath)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("{0} add {1}", IpfsCommand, Path.GetFileName (filePath))
			);

			var hash = ExtractHashAfterAddFile(starter.Output);

			return hash;
		}

		public string AddFolder (string folder)
		{
			var starter = new ProcessStarter ();

			//var originalDirectory = Environment.CurrentDirectory;

			//Directory.SetCurrentDirectory (folder);

			starter.Start (
				String.Format("{0} add -r {1}", IpfsCommand, folder)
			);

			//Directory.SetCurrentDirectory (originalDirectory);

			var hash = ExtractHashAfterAddFolder(starter.Output);

			return hash;
		}

		public string Publish(string hash)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("{0} name publish {1}", IpfsCommand, hash)
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

