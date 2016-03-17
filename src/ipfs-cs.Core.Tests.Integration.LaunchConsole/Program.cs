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

			var assembly = Assembly.LoadFrom (assemblyPath);

			var fixtureType = assembly.GetType (fixtureTypeName);

			var fixture = (BaseIntegrationTestFixture)Activator.CreateInstance (fixtureType);

			var testMethod = fixtureType.GetMethod ("Execute");
			/*
			var ipfsClient = new ipfsClient(
//			Thread.Sleep (1000);

			testMethod.Invoke (fixture, null);*/


			var ipfsDataPath = Path.GetFullPath (".ipfs-test-data");
			//			Thread.Sleep (1000);

			var ipfsClient = new ipfsClient (ipfsDataPath);
			testMethod.Invoke (fixture, null);
			ipfsClient.Init ();		
					
			//Thread.Sleep (20000); // This delay seems to prevent a "resource unavailable" error		
					
			using (var ipfsLauncher = ipfsClient.StartDaemon()) {		
					
			// TODO: Check if needed		
			Thread.Sleep (10000); // Let the daemon start		
					
			testMethod.Invoke (fixture, null);		
					
			//	ipfsLauncher.Close ();		
			}		

			Console.WriteLine (File.ReadAllText (Path.GetFullPath ("ipfs.log")));
		}
	}
}
