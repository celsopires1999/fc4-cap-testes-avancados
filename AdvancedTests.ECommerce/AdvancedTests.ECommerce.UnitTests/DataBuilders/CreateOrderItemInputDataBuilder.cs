using System;
using Bogus;
using AdvancedTests.ECommerce.Application.UseCases.CreateOrder;

namespace AdvancedTests.ECommerce.UnitTests.DataBuilders;

public class CreateOrderItemInputDataBuilder
{
    private string _name;
    private int _quantity;
    private decimal _price;

    public CreateOrderItemInputDataBuilder()
    {
        var _faker = new Faker("pt_BR");
        _name = _faker.Commerce.ProductName();
        _quantity = _faker.Random.Int(1, 10);
        _price = decimal.Parse(_faker.Commerce.Price());
    }

    public static CreateOrderItemInputDataBuilder AnItemInput() => new();

    public CreateOrderItemInput Build()
    {
        return new CreateOrderItemInput(_name, _quantity, _price);
    }

}
