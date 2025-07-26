using AdvancedTests.ECommerce.Domain.Entities;
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

    [Theory]
    [InlineData(OrderStatus.Confirmed)]
    [InlineData(OrderStatus.Paid)]
    [InlineData(OrderStatus.Shipped)]
    [InlineData(OrderStatus.Delivered)]
    [InlineData(OrderStatus.Cancelled)]
    public void ThrowsAnExceptionWhenTryingToConfirmAnOrderWithInvalidStatus(OrderStatus invalidStatus)
    {
        var order = AnOrder()
            .WithStatus(invalidStatus)
            .Build();

        Action act = () => order.Confirm();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Somente itens no status 'Criado' podem ser confirmados.");
    }

    [Theory]
    [InlineData(OrderStatus.Created)]
    [InlineData(OrderStatus.Paid)]
    [InlineData(OrderStatus.Shipped)]
    [InlineData(OrderStatus.Delivered)]
    [InlineData(OrderStatus.Cancelled)]
    public void ThrowsAnExceptionWhenTryingToPayAnOrderWithInvalidStatus(OrderStatus invalidStatus)
    {
        var order = AnOrder()
            .WithStatus(invalidStatus)
            .Build();

        Action act = () => order.Pay();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Somente itens no status 'Confirmado' podem ser pagos.");
    }

    [Theory]
    [InlineData(OrderStatus.Created)]
    [InlineData(OrderStatus.Confirmed)]
    [InlineData(OrderStatus.Shipped)]
    [InlineData(OrderStatus.Delivered)]
    [InlineData(OrderStatus.Cancelled)]
    public void ThrowsAnExceptionWhenTryingToShipAnOrderWithInvalidStatus(OrderStatus invalidStatus)
    {
        var order = AnOrder()
            .WithStatus(invalidStatus)
            .Build();

        Action act = () => order.Ship();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Somente itens no status 'Pago' podem ser enviados.");
    }

    [Theory]
    [InlineData(OrderStatus.Created)]
    [InlineData(OrderStatus.Confirmed)]
    [InlineData(OrderStatus.Paid)]
    [InlineData(OrderStatus.Delivered)]
    [InlineData(OrderStatus.Cancelled)]
    public void ThrowsAnExceptionWhenTryingToDeliverAnOrderWithInvalidStatus(OrderStatus invalidStatus)
    {
        var order = AnOrder()
            .WithStatus(invalidStatus)
            .Build();

        Action act = () => order.Deliver();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Somente itens no status 'Enviado' podem ser entregues.");
    }
}