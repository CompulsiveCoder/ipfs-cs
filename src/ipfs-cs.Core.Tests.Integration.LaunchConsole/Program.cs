using System;
using System.Reflection;
using System.IO;
using System.Threading;

namespace ipfs.Core.Tests.Integration.LaunchConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Preparing to launch integration test...");

			var parsedArguments = new Arguments (args);

			var assemblyPath = parsedArguments ["assembly"];

			var fixtureTypeName = parsedArguments ["type"];

			Console.WriteLine ("Assembly: " + assemblyPath);
			Console.WriteLine ("Type: " + fixtureTypeName);

			Console.WriteLine ("Executing test...");

			var assembly = Assembly.LoadFrom (assemblyPath);

			if (assembly == null)
				throw new Exception ("Can't find assembly: " + assembly);

			Type fixtureType = null;

			try
			{
				fixtureType = assembly.GetType (fixtureTypeName);
			}
			catch (InvalidCastException) {
				throw new Exception ("Invalid type. Does the test inherit BaseIntegrationTestFixture?");
			}

			if (fixtureType == null)
				throw new Exception ("Can't find fixture: " + fixtureTypeName);
			
			var fixture = (BaseIntegrationTestFixture)Activator.CreateInstance (fixtureType);

			var testMethod = fixtureType.GetMethod ("Execute");

			testMethod.Invoke (fixture, null);
		}
	}
}
