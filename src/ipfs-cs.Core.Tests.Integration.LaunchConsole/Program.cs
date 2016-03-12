using System;

namespace ipfs.Core.Tests.Integration.LaunchConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var parsedArguments = new Arguments (args);

			var assemblyName = parsedArguments ["assembly"];

			var typeName = parsedArguments ["type"];

			Console.WriteLine ("Assembly: " + assemblyName);
			Console.WriteLine ("Type: " + typeName);
		}
	}
}
