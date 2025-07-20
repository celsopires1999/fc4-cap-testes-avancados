using AdvancedTests.ECommerce.Domain;
using AdvancedTests.ECommerce.UnitTests.DataBuilders;
using FluentAssertions;

namespace AdvancedTests.ECommerce.UnitTests.Domain;

public class AddressTest
{

    [Fact]
    public void ThrowsExceptionWhenConstructingAndStreetIsNull()
    {
        var act = () => new AddressDataBuilder().WithStreet(null!).Build();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ThrowsExceptionWhenConstructingAndStreetIsEmpty()
    {

        var act = () => new AddressDataBuilder().WithStreet("").Build();

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ThrowsExceptionWhenConstructingAndCityIsNull()
    {
        string city = null!;

        var act = () => new AddressDataBuilder().WithCity(city).Build();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ThrowsExceptionWhenConstructingAndCityIsEmpty()
    {
        var act = () => new AddressDataBuilder().WithCity("").Build();

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreatesValidAddress()
    {
        var address = new AddressDataBuilder().Build();

        address.Should().NotBeNull();
        address.Street.Should().NotBeNullOrWhiteSpace();
        address.City.Should().NotBeNullOrWhiteSpace();
        address.State.Should().NotBeNullOrWhiteSpace();
        address.ZipCode.Should().MatchRegex(@"^\d{5}-?\d{3}$");
        address.Number.Should().BeGreaterThan(0);
    }

}