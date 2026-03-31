using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.CurrencyExchange;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.DataSeed
{
    public static class SeedProducts
    {
        public async static Task SeedData(IEcommerceUnitOfWork unitOfWork, ICurrencyConverter converter)
        {
            //Creating products
            List<Product> products = new List<Product>();
            var rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                var price = new decimal(rand.NextDouble());
                products.Add(new Product($"Product {i}", Money.Of(price, converter.GetBaseCurrency().Name)));
            }

            await unitOfWork.ProductRepository.AddProducts(products);
            await unitOfWork.CommitAsync();
        }
    }
}
