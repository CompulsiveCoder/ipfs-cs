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
			// TODO: Clean up this function

			var parsedArguments = new Arguments (args);

			var assemblyPath = parsedArguments ["assembly"];

			var fixtureTypeName = parsedArguments ["type"];

			Console.WriteLine ("Assembly: " + assemblyPath);
			Console.WriteLine ("Type: " + fixtureTypeName);

			Console.WriteLine ("Executing test...");

			var assembly = Assembly.LoadFrom(assemblyPath);

			var fixtureType = assembly.GetType (fixtureTypeName);

			var fixture = (BaseIntegrationTestFixture)Activator.CreateInstance(fixtureType);

			var testMethod = fixtureType.GetMethod ("Execute");

//			Thread.Sleep (1000);

			testMethod.Invoke (fixture, null);
		}
	}
}
