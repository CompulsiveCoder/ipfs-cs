﻿using System;
using System.IO;
using ipfs.Core;

namespace ipfs.Core
{
	public class ipfsClient
	{
		// TODO: Remove if not needed
		//public string IpfsCommand = "sh ipfs.sh";
		public string IpfsCommand = "ipfs";

		public string IpfsDataPath;

		public ipfsClient ()
		{
			IpfsDataPath = Path.GetFullPath(".ipfs-data");
		}

		public ipfsClient (string ipfsDataPath)
		{
			IpfsDataPath = ipfsDataPath;
		}

		public void Init()
		{
			Console.WriteLine ("Initializing ipfs client.");

			if (!Directory.Exists (IpfsDataPath))
				Directory.CreateDirectory (IpfsDataPath);

			Directory.SetCurrentDirectory (IpfsDataPath);

			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("{0} init", IpfsCommand)
			);

			Console.WriteLine (starter.Output);
		}

		public string AddFile(string filePath)
		{
			var starter = new ProcessStarter ();

			starter.Start (
				String.Format ("{0} add {1}", IpfsCommand, Path.GetFileName (filePath))
			);

			Console.WriteLine (starter.Output);

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

			Console.WriteLine (starter.Output);
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

			Console.WriteLine (starter.Output);

			var peerId = ExtractHashAfterPublish(starter.Output);

			//starter.Start (
			//	String.Format ("ipfs name resolve {0}", peerId)
			//);

			return peerId;
		}

		public string ExtractHashAfterAddFile(string output)
		{
			if (!output.Contains ("added"))
				throw new ArgumentException ("Error: " + output);
			
			var beginningRemoved = output.Trim ().Substring (output.IndexOf (" ")).TrimStart();

			var hash = beginningRemoved.Substring (0, beginningRemoved.IndexOf (" "));

			return hash;
		}


		public string ExtractHashAfterAddFolder(string output)
		{
			if (!output.Contains ("added"))
				throw new ArgumentException ("Error: " + output);

			var lines = output.Trim().Split ('\n');

			var line = "";

			foreach (var l in lines) {
				if (l.Trim().StartsWith ("added"))
					line = l;
			}

			var hash = "";

			if (line.IndexOf (" ") > -1) {
				var beginningRemoved = line.Trim ().Substring (line.IndexOf (" ")).TrimStart ();

				hash = beginningRemoved.Substring (0, beginningRemoved.IndexOf (" "));
			}

			return hash;
		}
			
		public string ExtractHashAfterPublish(string output)
		{
			if (!output.Contains ("Published to"))
				throw new ArgumentException ("Error: " + output);
			
			var firstStep = output.Trim ().Replace ("Published to ", "");

			var hash = firstStep.Substring (0, firstStep.IndexOf (":"));

			return hash;
		}

		public ipfsDaemonLauncher StartDaemon()
		{
			var launcher = new ipfsDaemonLauncher (IpfsDataPath);

			launcher.Start ();

			return launcher;
		}
	}
}

