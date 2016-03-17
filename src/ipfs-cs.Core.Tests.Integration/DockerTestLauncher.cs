using System;
using ipfs.Core;
using System.IO;

namespace ipfs.Core.Tests.Integration
{
	public class DockerTestLauncher
	{
		public string ImageName = "compulsivecoder/ubuntu-mono-ipfs";

		public DockerTestLauncher ()
		{
		}
		public void Launch(BaseIntegrationTestFixture fixture)
		{
			Launch (fixture.GetType ());
		}

		private void Launch(Type fixtureType)
		{
			var starter = new ProcessStarter ();

			var projectPath = Path.GetFullPath ("../../").TrimEnd('/'); // Use this line if the test is running in a temporary directory
			//var projectPath = Path.GetFullPath ("../../../../"); // Use this line if temporary directories are disabled

			var assemblyName = Path.GetFileName(fixtureType.Assembly.Location);

			var buildMode = "Release";
			#if DEBUG
			buildMode = "Debug";
			#endif

			Console.WriteLine ("Project path: " + projectPath);
			// TODO: Clean ups
			//var command = String.Format("cp /ipfs-cs /ipfs-cs-staging -r && cd /ipfs-cs-staging && rm bin/* -r && sh build.sh {0} && cd bin/{0} && mono LaunchIntegrationTest.exe /assembly:\"{1}\" /type:\"{2}\"", buildMode, assemblyName, fixtureType.FullName);

			var command = String.Format("cd /ipfs-cs && && sh start-integration-test.sh {0} {1} {2}", buildMode, assemblyName, fixtureType.FullName);

			command = String.Format ("docker run -v /var/run/docker.sock:/var/run/docker.sock -v {0}:/ipfs-cs {1} /bin/bash -c '{2}'", projectPath, ImageName, command);
			starter.WriteToConsole = true;
			starter.Start (
				command
			);
			Console.WriteLine (starter.Output);
			if (starter.IsError)
				throw new Exception ("Error launching docker based test");
		}
	}
}

