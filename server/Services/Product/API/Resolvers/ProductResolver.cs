using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Params;
using Core.Specifications;
using API.RequestHelpers;
using Infrastructure.Data;
using System.Text.Json;

namespace API.Resolvers;

public class ProductResolver : BaseResolver
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductResolver(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Pagination<ProductDto>> GetProducts(ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePagedResult<Product, ProductDto>(_unitOfWork.Repository<Product>(), spec, specParams.PageIndex, specParams.PageSize, _mapper);
    }

    public async Task<ProductDto?> GetProductById(long id)
    {
        var spec = new ProductSpecification(id);
        var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);
        if (product == null) return null;
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto?> CreateProduct(CreateProductDto createProduct)
    {
        var product = _mapper.Map<Product>(createProduct);

        // get category by Id
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(createProduct.CategoryId);
        if (category != null)
        {
            product.Category = category;
        }

        // get Attributes by Ids
        var attributes = new List<ProductAttribute>();
        foreach (var attributeId in createProduct.AttributeIds)
        {
            var attr = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(attributeId);
            if (attr != null)
            {
                attributes.Add(attr);
            }
        }
        // Populate the ProductAttributes collection
        product.ProductAttributes = attributes.Select(attr => new ProductAttributeNavigator
        {
            Product = product,
            ProductAttribute = attr
        }).ToList();

        // get AttributeOptions by Ids
        var attributeOptionMap = new Dictionary<long, AttributeOption>();
        foreach (var variant in createProduct.Variants)
        {
            foreach (var attribute in variant.AttributeValues)
            {
                if (!attributeOptionMap.ContainsKey(attribute.OptionId))
                {
                    var option = await _unitOfWork.Repository<AttributeOption>().GetByIdAsync(attribute.OptionId);
                    if (option != null)
                    {
                        attributeOptionMap[attribute.AttributeId] = option;
                    }
                }
            }
        }

        foreach (var productVariant in product.Variants)
        {
            foreach (var productAttributeValue in productVariant.AttributeValues)
            {
                if (attributeOptionMap.TryGetValue(productAttributeValue.AttributeId, out var option))
                {
                    productAttributeValue.Option = option;
                }
            }
        }

        _unitOfWork.Repository<Product>().Add(product);

        if (await _unitOfWork.Complete())
        {
            return _mapper.Map<ProductDto>(product);
        }

        return null;
    }

    public async Task<ProductDto?> UpdateProduct(long id, UpdateProductDto updateProduct)
    {
        if (updateProduct.Id != id || !IssExists<Product>(id, _unitOfWork.Repository<Product>()))
            throw new Exception($"Product with ID {id} not found or mismatched.");

        var product = _mapper.Map<Product>(updateProduct);

        // get category by Id
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(updateProduct.CategoryId);
        if (category != null)
        {
            product.Category = category;
        }

        // get Attributes by Ids
        var attributes = new List<ProductAttribute>();
        foreach (var attributeId in updateProduct.AttributeIds)
        {
            var attr = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(attributeId);
            if (attr != null)
            {
                attributes.Add(attr);
            }
        }
        // Populate the ProductAttributes collection
        product.ProductAttributes = attributes.Select(attr => new ProductAttributeNavigator
        {
            Product = product,
            ProductAttribute = attr
        }).ToList();

        // get AttributeOptions by Ids
        var attributeOptionMap = new Dictionary<long, AttributeOption>();
        foreach (var variant in updateProduct.Variants)
        {
            foreach (var attribute in variant.AttributeValues)
            {
                if (!attributeOptionMap.ContainsKey(attribute.OptionId))
                {
                    var option = await _unitOfWork.Repository<AttributeOption>().GetByIdAsync(attribute.OptionId);
                    if (option != null)
                    {
                        attributeOptionMap[attribute.AttributeId] = option;
                    }
                }
            }
        }

        foreach (var productVariant in product.Variants)
        {
            foreach (var productAttributeValue in productVariant.AttributeValues)
            {
                if (attributeOptionMap.TryGetValue(productAttributeValue.AttributeId, out var option))
                {
                    productAttributeValue.Option = option;
                }
            }
        }

        _unitOfWork.Repository<Product>().Update(product);

        if (await _unitOfWork.Complete())
        {
            return _mapper.Map<ProductDto>(product);
        }

        return null;
    }

    public async Task<ProductDto?> DeleteProduct(long id)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) throw new Exception($"Product with ID {id} not found or mismatched.");

        _unitOfWork.Repository<Product>().Remove(product);
        if (await _unitOfWork.Complete())
        {
            return _mapper.Map<ProductDto>(product);
        }

        return null;
    }

    public async Task<IReadOnlyList<string>> GetBrands()
    {
        var spec = new BrandListSpecification();

        return await _unitOfWork.Repository<Product>().ListAsync(spec);
    }

    public async Task<IReadOnlyList<string>> GetTypes()
    {
        var spec = new TypeListSpecification();

        return await _unitOfWork.Repository<Product>().ListAsync(spec);
    }

    public static async Task AddProductsFromJsonAsync(StoreContext context, ProductResolver productResolver, string filePath)
    {
        if (!context.Products.Any())
        {
            try
            {
                // Đọc dữ liệu từ file JSON
                var productsData = await File.ReadAllTextAsync(filePath);
                var createProductDtos = JsonSerializer.Deserialize<List<CreateProductDto>>(productsData);

                if (createProductDtos == null || !createProductDtos.Any())
                {
                    Console.WriteLine("No products to add from the file.");
                    return;
                }

                // Thêm từng sản phẩm vào cơ sở dữ liệu
                foreach (var createProductDto in createProductDtos)
                {
                    var result = await productResolver.CreateProduct(createProductDto);

                    if (result == null)
                    {
                        Console.WriteLine($"Failed to add product: {createProductDto.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Successfully added product: {result.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding products: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Products already exist in the database. Skipping seeding.");
        }
    }
}