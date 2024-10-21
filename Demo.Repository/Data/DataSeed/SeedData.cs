using Azure.Core.Serialization;
using Demo.Core.Models;
using Demo.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Repository.Data.DataSeed
{
    public static class SeedData
    {
        public async static Task seedDataToDBAsync(StoreDbContext context)
        {
            if (context.Brands.Count() == 0)
            {
                var brandsData = await File.ReadAllTextAsync(@"..\Demo.Repository\Data\DataSeed\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Count() > 0)
                {

                    await context.Brands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

            if (context.Types.Count() == 0)
            {
                var TypesData = await File.ReadAllTextAsync(@"..\Demo.Repository\Data\DataSeed\types.json");

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types is not null && Types.Count() > 0)
                {

                    await context.Types.AddRangeAsync(Types);
                    await context.SaveChangesAsync();
                }
            }

            if (context.Products.Count() == 0)
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\Demo.Repository\Data\DataSeed\products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products is not null && Products.Count() > 0)
                {

                    await context.Products.AddRangeAsync(Products);
                    await context.SaveChangesAsync();
                }
            }

            if (context.deliveryMethods.Count() == 0)
            {
                var DeliveryData = await File.ReadAllTextAsync(@"..\Demo.Repository\Data\DataSeed\delivery.json");

                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (Delivery is not null && Delivery.Count() > 0)
                {
                    await context.deliveryMethods.AddRangeAsync(Delivery);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
