using System;

namespace ipfs.Core.Tests.Integration
{
	public abstract class BaseIntegrationTestFixture
	{
		public BaseIntegrationTestFixture ()
		{
		}

		public abstract void Execute();
	}
}

