using System;
using NUnit.Framework;

namespace ipfs.Core.Tests.Integration
{
	[TestFixtureAttribute(Category="Integration")]
	public abstract class BaseIntegrationTestFixture
	{
		public BaseIntegrationTestFixture ()
		{
		}

		public abstract void Execute();
	}
}

