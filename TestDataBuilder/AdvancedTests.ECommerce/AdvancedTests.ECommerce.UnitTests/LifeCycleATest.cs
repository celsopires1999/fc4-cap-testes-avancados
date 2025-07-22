using System;

namespace AdvancedTests.ECommerce.UnitTests;

[Collection("LifeCycleTestCollection")]
public class LifeCycleATest : IDisposable
{

    public LifeCycleATest()
    {
        Console.WriteLine("TestA Setup");
    }

    [Fact]
    public void Test1()
    {
        Console.WriteLine("Test1");
    }

    [Fact]
    public void Test2()
    {
        Console.WriteLine("Test2");
    }

    public void Dispose()
    {
        Console.WriteLine("TestA teardown");
    }
}
