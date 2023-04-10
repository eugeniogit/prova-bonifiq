using ProvaPub.Models;
using ProvaPub.Utilities;
using Xunit;

namespace ProvaPub.Tests
{
    public class PurchaseRulesTest
    {
        [Fact]
        public void BoughtInMonthAlready_CustomerFirstPurchase_MustBeFalse()
        {
            // arrange
            var purchaseRules = new PurchaseRules();

            var customer = new Customer()
            {
                Orders = new List<Order>()
            };

            var currentDate = new DateTime(2023, 5, 1);

            // act
            var result = purchaseRules.BoughtInMonthAlready(customer, currentDate);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void BoughtInMonthAlready_CustomerNotBoughtInMonth_MustBeFalse()
        {
            // arrange
            var purchaseRules = new PurchaseRules();

            var currentDate = new DateTime(2023, 5, 1);

            var customer = new Customer()
            {
                Orders = new List<Order>()
                {
                    new Order() { OrderDate = currentDate.AddMonths(-1) }
                }
            };

            // act
            var result = purchaseRules.BoughtInMonthAlready(customer, currentDate);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void BoughtInMonthAlready_CustomerBoughtInMonth_MustBeTrue()
        {
            // arrange
            var purchaseRules = new PurchaseRules();

            var currentDate = new DateTime(2023, 5, 1);

            var customer = new Customer()
            {
                Orders = new List<Order>()
                {
                    new Order() { OrderDate = new DateTime(2023, 4, 20) },
                    new Order() { OrderDate = new DateTime(2023, 4, 21) }
                }
            };

            // act
            var result = purchaseRules.BoughtInMonthAlready(customer, currentDate);

            // assert
            Assert.True(result);
        }
    }
}