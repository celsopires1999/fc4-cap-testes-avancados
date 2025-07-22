using System;

namespace AdvancedTests.ECommerce.UnitTests;

public class LifeCycleTestFixture :IDisposable
{
    public LifeCycleTestFixture()
    {
        Console.WriteLine("Fixture setup");
    }

    public void Dispose()
    {
        Console.WriteLine("Fixture teardown");
    }
}


[CollectionDefinition("LifeCycleTestCollection")]
public class LifeCycleTestFixtureCollection : ICollectionFixture<LifeCycleTestFixture>;