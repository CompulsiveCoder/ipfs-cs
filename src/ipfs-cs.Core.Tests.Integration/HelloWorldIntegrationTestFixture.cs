using System;
using NUnit.Framework;

namespace ipfs.Core.Tests.Integration
{
	/// <summary>
	/// This test is here to help show that the test launcher is working.
	/// </summary>
	[TestFixture("Category=Integration")]
	public class HelloWorldIntegrationTestFixture : BaseIntegrationTestFixture
	{
		[Test]
		public void Test_HelloWorld()
		{
			new DockerTestLauncher ().Launch (this);
		}

		public override void Execute ()
		{
			Console.WriteLine ("Hello world");
		}
	}
}

