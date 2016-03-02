using System;
using System.IO;
using ipfs.Core;

namespace ipfs.Managed
{
	public class ipfsManagedClient
	{
		public string DataDirectory { get; set; }

		public ipfsClient Client { get; set; }

		public bool IsVerbose = false;

		public string IpfsCommand
		{
			get { return Client.IpfsCommand; } 
			set { Client.IpfsCommand = value; }
		}

		public ipfsManagedClient ()
		{
			DataDirectory = "ipfs-data";

			if (Environment.CurrentDirectory.Trim (Path.DirectorySeparatorChar).EndsWith (DataDirectory))
				DataDirectory = Environment.CurrentDirectory;
			else
				DataDirectory = Path.GetFullPath (DataDirectory);

			Client = new ipfsClient ();
		}

		public string AddFile(string filePath, string subFolderName)
		{
			if (IsVerbose) {
				Console.WriteLine ("Adding file (via managed client):");
				Console.WriteLine(filePath);
				Console.WriteLine();
				Console.WriteLine ("Sub folder name:");
				Console.WriteLine(subFolderName);
				Console.WriteLine();
				Console.WriteLine ("Data root directory:");
				Console.WriteLine(DataDirectory);
				Console.WriteLine();
			}

			var subFolderPath = Path.Combine (DataDirectory, subFolderName);

			if (!Directory.Exists (subFolderPath))
				Directory.CreateDirectory (subFolderPath);

			var fileName = Path.GetFileName (filePath);

			var newFilePath = Path.Combine (subFolderPath, fileName);

			File.Copy (filePath, newFilePath);

			if (IsVerbose) {
				Console.WriteLine ("File imported to:");
				Console.WriteLine (newFilePath);
				Console.WriteLine();
			}

			var hash = Client.AddFolder (DataDirectory);

			if (IsVerbose) {
				Console.WriteLine ("Folder hash: " + hash);
				Console.WriteLine();
			}

			return hash;
		}

		public string Set (string subFolderName, string fileName, string contents)
		{
			var subFolderPath = Path.Combine (DataDirectory, subFolderName);

			var dataFilePath = Path.Combine (subFolderPath, fileName);

			if (IsVerbose) {
				Console.WriteLine ("Appending file (via data client):");
				Console.WriteLine(dataFilePath);
				Console.WriteLine();
				Console.WriteLine ("Sub folder name:");
				Console.WriteLine(subFolderName);
				Console.WriteLine();
				Console.WriteLine ("Data root directory:");
				Console.WriteLine(DataDirectory);
				Console.WriteLine();
			}

			if (!Directory.Exists (subFolderPath))
				Directory.CreateDirectory (subFolderPath);

			File.WriteAllText (dataFilePath, contents);

			if (IsVerbose) {
				Console.WriteLine ("File appended:");
				Console.WriteLine (dataFilePath);
				Console.WriteLine();
			}

			var hash = Client.AddFolder (DataDirectory);

			if (IsVerbose) {
				Console.WriteLine ("Folder hash: " + hash);
				Console.WriteLine();
			}

			return hash;
		}

		public string Append (string subFolderName, string fileName, string contents, bool newLine)
		{
			var subFolderPath = Path.Combine (DataDirectory, subFolderName);

			var dataFilePath = Path.Combine (subFolderPath, fileName);

			if (IsVerbose) {
				Console.WriteLine ("Appending file (via managed client):");
				Console.WriteLine(dataFilePath);
				Console.WriteLine();
				Console.WriteLine ("Folder name:");
				Console.WriteLine(subFolderName);
				Console.WriteLine();
				Console.WriteLine ("File name:");
				Console.WriteLine(fileName);
				Console.WriteLine();
				Console.WriteLine ("Data root directory:");
				Console.WriteLine(DataDirectory);
				Console.WriteLine();
			}

			if (!Directory.Exists (subFolderPath))
				Directory.CreateDirectory (subFolderPath);

			if (!File.Exists (dataFilePath))
				File.WriteAllText (dataFilePath, "");

			var isEmpty = File.ReadAllText (dataFilePath).Trim ().Length == 0;

			if (newLine && !isEmpty)
				File.AppendAllText (dataFilePath, Environment.NewLine);

			File.AppendAllText (dataFilePath, contents);


			if (IsVerbose) {
				Console.WriteLine ("File appended:");
				Console.WriteLine (dataFilePath);
				Console.WriteLine();
			}

			var hash = Client.AddFolder (DataDirectory);

			if (IsVerbose) {
				Console.WriteLine ("Folder hash: " + hash);
				Console.WriteLine();
			}

			return hash;
		}

		public string Publish (string hash)
		{
			if (IsVerbose) {
				Console.WriteLine ("Publishing (via managed client):");
				Console.WriteLine(hash);
				Console.WriteLine();
				Console.WriteLine ("Data root directory:");
				Console.WriteLine(DataDirectory);
				Console.WriteLine();
			}

			var originalDirectory = Environment.CurrentDirectory;

			Directory.SetCurrentDirectory (DataDirectory);

			var peerId = Client.Publish (hash);

			Directory.SetCurrentDirectory (originalDirectory);

			if (IsVerbose) {
				Console.WriteLine ("Peer ID: " + peerId);
				Console.WriteLine();
			}

			return peerId;
		}
	}
}

