using AdvancedTests.ECommerce.Domain.Entities;

using AdvancedTests.ECommerce.UnitTests.DataBuilders;
using FluentAssertions;

namespace AdvancedTests.ECommerce.UnitTests.Domain;

public class AddressTest
{

    [Theory]
    [InlineData(null, "Value cannot be null. (Parameter 'street')") ]
    [InlineData("", "Required input street was empty. (Parameter 'street')")]
    [InlineData(" ", "Required input street was empty. (Parameter 'street')") ]
    [InlineData("     ", "Required input street was empty. (Parameter 'street')")]
    public void ThrowsExceptionWhenConstructingAndStreetIsInvalid(string street, string errorMessage)
    {
        var act = () => new AddressDataBuilder().WithStreet(street).Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage(errorMessage);
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