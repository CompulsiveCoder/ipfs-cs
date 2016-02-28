using System;
using System.IO;

namespace ipfs.Core.Tests
{
	public class TemporaryDirectoryCreator
	{
		public TemporaryDirectoryCreator ()
		{
		}

		public string Create ()
		{
			var tmpDir = Path.GetFullPath (".tmp");

			if (!Directory.Exists (tmpDir))
				Directory.CreateDirectory (tmpDir);

			var uniqueTmpDir = Path.Combine (tmpDir, Guid.NewGuid ().ToString ());

			Directory.CreateDirectory (uniqueTmpDir);

			return uniqueTmpDir;
		}
	}
}

