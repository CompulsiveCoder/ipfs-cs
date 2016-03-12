using System;
using ipfs.Core;
using System.IO;

namespace ipfs.Core.Tests.Integration
{
	public class DockerTestLauncher
	{
		public string ImageName = "compulsivecoder/ubuntu-mono";

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

			//var pre = "apt-get update && apt-get install -y curl git";
			/*var command = "curl https://raw.githubusercontent.com/CompulsiveCoder/ipfs-cs/master/build-from-github.sh | sh && ls && pwd" +
				"&& mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll /fixture:" + fixtureName;*/

			//sh ../../hello.sh
			var command = "cd ipfs-cs && sh build.sh && mono bin/{0}/LaunchIntegrationTest.exe /assembly:bin/{0}/{1}.dll /type:{2}";
				//"&& mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll /fixture:" + fixtureName;


			//command = pre + " && " + command;

			//command = String.Format ("docker run {0} /bin/bash -c '{1}'", ImageName, command);
			command = String.Format ("docker run -i -v {0}:/ipfs-cs {1} {2}", Path.GetFullPath("../../../.."), ImageName, command);

			starter.Start (
				command
			);

			Console.WriteLine (starter.Output);
		}
	}
}

