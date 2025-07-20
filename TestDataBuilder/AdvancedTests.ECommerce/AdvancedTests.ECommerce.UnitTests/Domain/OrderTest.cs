using AdvancedTests.ECommerce.Domain;
using AdvancedTests.ECommerce.UnitTests.DataBuilders;
using FluentAssertions;
using static AdvancedTests.ECommerce.UnitTests.DataBuilders.CustomerDataBuilder;
using static AdvancedTests.ECommerce.UnitTests.DataBuilders.OrderDataBuilder;
using static AdvancedTests.ECommerce.UnitTests.DataBuilders.OrderItemDataBuilder;

namespace AdvancedTests.ECommerce.UnitTests.Domain;

public class OrderTest
{
    [Fact]
    public void ThrowsAnExceptionWhenConstructionWithCustomerNull()
    {
        var act = () => new OrderDataBuilder().WithCustomer(null!).Build();

        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void Give10PercentDiscountForPremiumCustomer()
    {
        var anOrder = AnOrder()
            .From(APremiumCustomer())
            .WithItem(quantity: 2, price: 10)
            .WithItem(quantity: 4, price: 20)
            .Build();

        anOrder.Amount.Should().Be(90);
    }

    [Fact]
    public void CalculateAmountWithDiscountForMultipleItemsOfSameProduct()
    {
        var book = AnOrderItem()
            .WithName("Refactoring")
            .WithPrice(100)
            .WithQuantity(1);

        var bookWithSmallerDiscount = book
            .WithDiscount(0.10m)
            .Build();

        var bookWithGreaterDiscount = book
            .WithDiscount(0.20m)
            .Build();

        var order = AnOrder()
            .From(ARegularCustomer())
            .WithItems(bookWithSmallerDiscount, bookWithGreaterDiscount)
            .Build();

        order.Amount.Should().Be(170);
    }

    [Fact]
    public void ThrowsAnExceptionWhenTryingToCancelADeliveredOrder()
    {
        var order = AnOrder()
            .WithStatus(OrderStatus.Delivered)
            .Build();

        Action act = () => order.Cancel();

        act.Should().Throw<InvalidOperationException>()
        .WithMessage("Itens entregues não podem ser cancelados.");
    }
}