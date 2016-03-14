﻿using System;
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

			var projectPath = Path.GetFullPath ("../../");

			var assemblyName = Path.GetFileName(fixtureType.Assembly.Location);

			var buildMode = "Release";
			#if DEBUG
			buildMode = "Debug";
			#endif

			var command = String.Format("cp /ipfs-cs /ipfs-cs-staging -r && cd /ipfs-cs-staging && rm bin/* -r && sh build.sh {0} && cd bin/{0} && pwd && ls && mono LaunchIntegrationTest.exe /assembly:\"{1}\" /type:\"{2}\"", buildMode, assemblyName, fixtureType.FullName);

			command = String.Format ("docker run -i -v {0}:/ipfs-cs {1} /bin/bash -c '{2}'", projectPath, ImageName, command);
			starter.WriteToConsole = true;
			starter.Start (
				command
			);

			//Console.WriteLine (starter.Output);
		}
	}
}

