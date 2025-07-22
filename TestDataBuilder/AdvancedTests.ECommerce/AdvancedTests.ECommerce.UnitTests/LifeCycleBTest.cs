using System;

namespace AdvancedTests.ECommerce.UnitTests;

[Collection("LifeCycleTestCollection")]
public class LifeCycleBTest : IDisposable
{
    public LifeCycleBTest()
    {
        Console.WriteLine("TestB Setup");
    }

    [Fact]
    public void Test1()
    {
        Console.WriteLine("Test3");
    }

    [Fact]
    public void Test2()
    {
        Console.WriteLine("Test4");
    }

    public void Dispose()
    {
        Console.WriteLine("TestB teardown");
    }
}
