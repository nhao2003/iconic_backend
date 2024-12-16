using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            var user = new AppUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Categories.Any())
        {
            var categoriesData = await File.ReadAllTextAsync(path + @"/Data/SeedData/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

            if (categories == null) return;

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!context.ProductAttributes.Any())
        {
            var attributesData = await File.ReadAllTextAsync(path + @"/Data/SeedData/attributes.json");
            var attributes = JsonSerializer.Deserialize<List<ProductAttribute>>(attributesData);

            if (attributes == null) return;

            context.ProductAttributes.AddRange(attributes);
            await context.SaveChangesAsync();
        }

        //if (!context.Products.Any())
        //{
        //    var productsData = await File.ReadAllTextAsync(path + @"/Data/SeedData/products.json");
        //    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        //    if (products == null) return;

        //    context.Products.AddRange(products);
        //    await context.SaveChangesAsync();
        //}

        //if (!context.Variants.Any())
        //{
        //    var variantsData = await File.ReadAllTextAsync(path + @"/Data/SeedData/variants.json");
        //    var variants = JsonSerializer.Deserialize<List<Variant>>(variantsData);

        //    if (variants == null) return;

        //    context.Variants.AddRange(variants);
        //    await context.SaveChangesAsync();
        //}

        if (!context.DeliveryMethods.Any())
        {
            var dmData = await File
                .ReadAllTextAsync(path + @"/Data/SeedData/delivery.json");

            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

            if (methods == null) return;

            context.DeliveryMethods.AddRange(methods);

            await context.SaveChangesAsync();
        }
    }
}
