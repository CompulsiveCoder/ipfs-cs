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

			// TODO: Clean up this function

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

			// TODO: Clean up
			/*
			var ipfsClient = new ipfsClient(
//			Thread.Sleep (1000);

			testMethod.Invoke (fixture, null);*/


			//var ipfsDataPath = Path.GetFullPath (".ipfs-test-data");
			//			Thread.Sleep (1000);

			//var ipfsClient = new ipfsClient (ipfsDataPath);
			testMethod.Invoke (fixture, null);
			//ipfsClient.Init ();		
					
			//Thread.Sleep (20000); // This delay seems to prevent a "resource unavailable" error		
					
			//using (var ipfsLauncher = ipfsClient.StartDaemon()) {		
					
			// TODO: Check if needed		
			//Thread.Sleep (10000); // Let the daemon start		
					
			//testMethod.Invoke (fixture, null);		
					
			//	ipfsLauncher.Close ();		
			//}		

			//Console.WriteLine (File.ReadAllText (Path.GetFullPath ("ipfs.log")));
		}
	}
}
