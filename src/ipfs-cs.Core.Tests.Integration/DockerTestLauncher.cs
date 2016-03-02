using System;
using ipfs.Core;

namespace ipfs.Core.Tests.Integration
{
	public class DockerTestLauncher
	{
		public string ImageName = "ubuntu";

		public DockerTestLauncher ()
		{
		}
		public void Launch(string fixtureName)
		{
			var starter = new ProcessStarter ();

			var pre = "sudo apt-get update && sudo apt-get install -y curl git";
			var command = "curl https://raw.githubusercontent.com/CompulsiveCoder/ipfs-cs/master/build-from-github.sh | sh" +
				"&& mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll /fixture:" + fixtureName;

			command = pre + " && " + command;

			command = String.Format ("docker run {0} /bin/bash -c '{1}'", ImageName, command);

			starter.Start (
				command
			);

			Console.WriteLine (starter.Output);
		}
	}
}

