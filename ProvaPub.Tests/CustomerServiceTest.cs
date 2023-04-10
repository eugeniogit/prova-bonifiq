using Moq;
using Moq.AutoMock;
using ProvaPub.Generic;
using ProvaPub.Models;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CanPurchase_InvalidCustomerId_MustThrowsExceptions(int customerId)
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();

            // act & assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchaseAsync(customerId, 0, DateTime.UtcNow));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CanPurchase_InvalidPurchaseValue_MustThrowsExceptions(int purchaseValue)
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();

            // act & assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchaseAsync(1, purchaseValue, DateTime.UtcNow));
        }

        [Fact]
        public void CanPurchase_CustomerNotFound_MustThrowsExceptions()
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();
            var customerId = 1;
            var purchaseValue = 1;

            moker.GetMock<ICustomerRepository>()
                .Setup(m => m.FindAsync(customerId))
                .ReturnsAsync(default(Customer));

            // act & assert
            Assert.ThrowsAsync<InvalidOperationException>(() => service.CanPurchaseAsync(customerId, purchaseValue, DateTime.UtcNow));
        }

        [Fact]
        public async Task CanPurchase_BoughtInMonthAlready_MustBeFalse()
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();
            var customerId = 1;
            var purchaseValue = 1;

            moker.GetMock<IPurchaseRules>()
                .Setup(m => m.BoughtInMonthAlready(It.IsAny<Customer>(), It.IsAny<DateTime>()))
                .Returns(true);

            moker.GetMock<ICustomerRepository>()
                .Setup(m => m.FindAsync(customerId))
                .ReturnsAsync(new Customer());

            // act
            var result = await service.CanPurchaseAsync(customerId, purchaseValue, DateTime.UtcNow);

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_FirstPurchaseHasGreatterValue_MustBeFalse()
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();
            var customerId = 1;
            var purchaseValue = 1;

            moker.GetMock<IPurchaseRules>()
                .Setup(m => m.BoughtInMonthAlready(It.IsAny<Customer>(), It.IsAny<DateTime>()))
                .Returns(false);

            moker.GetMock<IPurchaseRules>()
                .Setup(m => m.FirstPurchaseHasGreatterValue(It.IsAny<Customer>(), purchaseValue))
                .Returns(true);

            moker.GetMock<ICustomerRepository>()
                .Setup(m => m.FindAsync(customerId))
                .ReturnsAsync(new Customer());

            // act
            var result = await service.CanPurchaseAsync(customerId, purchaseValue, DateTime.UtcNow);

            // assert
            Assert.False(result);
        }


        [Fact]
        public async Task CanPurchase_MustBeTrue()
        {
            // arrange
            var moker = new AutoMocker();
            var service = moker.CreateInstance<CustomerService>();
            var customerId = 1;
            var purchaseValue = 1;

            moker.GetMock<IPurchaseRules>()
                .Setup(m => m.BoughtInMonthAlready(It.IsAny<Customer>(), It.IsAny<DateTime>()))
                .Returns(false);

            moker.GetMock<IPurchaseRules>()
                .Setup(m => m.FirstPurchaseHasGreatterValue(It.IsAny<Customer>(), purchaseValue))
                .Returns(false);

            moker.GetMock<ICustomerRepository>()
                .Setup(m => m.FindAsync(customerId))
                .ReturnsAsync(new Customer());

            // act
            var result = await service.CanPurchaseAsync(customerId, purchaseValue, DateTime.UtcNow);

            // assert
            Assert.True(result);
        }

    }
}