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
    public void ThrowsAnExceptionWhenConstructionWithItemsNull()
    {
        var act = () => new OrderDataBuilder().WithItems(null!).Build();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Give10PercentDiscountForPremiumCustomerWhenTotalIsGreaterThan1_000()
    {
        var anOrder = AnOrder()
            .From(APremiumCustomer())
            .WithItem(quantity: 2, price: 1_000)
            .WithItem(quantity: 4, price: 2_000)
            .Build();

        anOrder.Amount.Should().Be(9_000);
    }

    [Fact]
    public void GiveNoDiscountForPremiumCustomerWhenTotalIsEqualOrLowerThan1_000()
    {
        var anOrder = AnOrder()
            .From(APremiumCustomer())
            .WithItem(quantity: 1, price: 1_000)
            .Build();

        anOrder.Amount.Should().Be(1_000);
    }

    [Fact]
    public void GiveNoDiscountForRegularCustomerWhenTotalIsGreaterThan1_000()
    {
        var anOrder = AnOrder()
            .From(ARegularCustomer())
            .WithItem(quantity: 2, price: 1_000)
            .WithItem(quantity: 4, price: 2_000)
            .Build();

        anOrder.Amount.Should().Be(10_000);
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

    [Fact]
    public void ThrowsAnExceptionWhenTryingToAddANullItemToOrder()
    {
        var order = AnOrder()
            .WithStatus(OrderStatus.Created)
            .Build();

        Action act = () => order.AddItem(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ThrowsExceptionWhenAddItemToOrderButStatusDoesntAllowIt()
    {
        var order = AnOrder()
            .WithStatus(OrderStatus.Paid)
            .Build();
        
        var item = AnOrderItem().Build();
        
        var act = () => order.AddItem(item);
        
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Item não pode ser adicionado ao pedido.");
    }

    [Fact]
    public void AddItemToOrderWhenStatusIsCreated()
    {
        var order = AnOrder()
            .WithStatus(OrderStatus.Created)
            .WithItem(quantity: 1, price: 10)
            .Build();

        var item = AnOrderItem()
            .WithName("Refactoring")
            .WithPrice(100)
            .WithQuantity(1)
            .Build();

        order.AddItem(item);

        order.Items.Should().Contain(item);
        order.Amount.Should().Be(110);
    }
}