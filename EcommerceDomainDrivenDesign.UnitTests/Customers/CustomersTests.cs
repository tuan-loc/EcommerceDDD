using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.Customers;
using NSubstitute;
using NUnit.Framework;

namespace EcommerceDomainDrivenDesign.UnitTests.Customers
{
    [TestFixture]
    public class CustomersTests
    {
        const string name = "Customer";
        const string email = "test@email.com";

        [Test]
        public void Is_Customer_Email_Available()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            customerUniquenessChecker.IsUserUnique(email).Returns(true);

            // Act
            var customer = Customer.CreateCustomer(email, name, customerUniquenessChecker);

            // Assert
            Assert.True(customer.Email == email);
        }

        [Test]
        public void Is_Customer_Email_Already_In_Use()
        {
            // Arrange
            Customer customer = null;
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            customerUniquenessChecker.IsUserUnique(email).Returns(false);

            // Assert
            var businessRuleValidationException = Assert.Catch<BusinessRuleException>(() =>
            {
                // Act
                customer = Customer.CreateCustomer(email, name, customerUniquenessChecker);
            });

            Assert.IsNull(customer);
        }
    }
}
