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

    [Theory]
    [InlineData(null, "Value cannot be null. (Parameter 'state')")]
    [InlineData("", "Required input state was empty. (Parameter 'state')")]
    [InlineData(" ", "Required input state was empty. (Parameter 'state')")]    
    [InlineData("M", "Input minLength with length 1 is too short. Minimum length is 2. (Parameter 'minLength')")]
    [InlineData("MGG", "Input maxLength with length 3 is too long. Maximum length is 2. (Parameter 'maxLength')")]
    public void ThrowsExceptionWhenConstructingAndStateIsInvalid(string state, string errorMessage)
    {
        var act = () => new AddressDataBuilder().WithState(state).Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage(errorMessage);
    }

    [Fact]
    public void ThrowsExceptionWhenConstructingAndStateLengthIsInvalid()
    {
        var act = () => new AddressDataBuilder().WithState("MG ").Build();

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("12345-67")]
    public void ThrowsExceptionWhenConstructingAndZipCodeIsInvalid(string zipCode)
    {
        var act = () => new AddressDataBuilder().WithZipCode(zipCode).Build();

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ThrowsExceptionWhenConstructingAndNumberIsInvalid(int number)
    {
        var act = () => new AddressDataBuilder().WithNumber(number).Build();

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