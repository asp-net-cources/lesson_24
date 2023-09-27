using Lesson24.Data.EF;
using Lesson24.Data;
using Lesson24.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson24.Controllers;

[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDataContext _context;

    public ProductController(IDataContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<ProductModel[]> GetProductList()
    {
        var dbProducts = await _context.SelectProducts();

        var products = dbProducts.Select((dbProduct) =>
        {

            ProductModel product = dbProduct.ProductType switch
            {
                Data.Models.ProductType.Accessories => new AccessoriesModel(),
                Data.Models.ProductType.Book => new BookModel(),
                Data.Models.ProductType.Food => new FoodModel()
            };

            product.Id = dbProduct.Id;
            product.Name = dbProduct.Name;
            product.Description = dbProduct.Description;
            product.Price = dbProduct.Price;

            return product;
        }).ToArray();
        
        return products;
    }
    
    
    [HttpGet("{id}")]
    public async Task<ProductModel?> GetProduct([FromRoute]int id)
    {
        var dbProducts = await _context.SelectProducts();
        var dbProduct =  dbProducts.FirstOrDefault(p => p.Id == id);

        ProductModel product = dbProduct.ProductType switch {
            Data.Models.ProductType.Accessories => new AccessoriesModel(),
            Data.Models.ProductType.Book => new BookModel(),
            Data.Models.ProductType.Food => new FoodModel()
        };

        product.Id = dbProduct.Id;
        product.Name = dbProduct.Name;
        product.Description = dbProduct.Description;
        product.Price = dbProduct.Price;

        return product;
    }
    
    [HttpPost("{id}")]
    public async Task<ProductModel?> UpdateProduct([FromRoute]int id, [FromBody] ProductModel updatedProduct)
    {
        updatedProduct.Id = id;
        await _context.UpdateProduct(new Data.Models.Product() {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Description = updatedProduct.Description,
            Price = updatedProduct.Price,
            Author = "user",
            ProductType = updatedProduct.ProductType
        });
        return updatedProduct;
    }

    [HttpPost("create-product")]
    public async Task<ProductModel?> CreateProduct([FromBody] ProductModel createdProduct) {
        await _context.InsertProduct(new Data.Models.Product() {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            Author = "user",
            ProductType = createdProduct.ProductType
        });
        return createdProduct;
    }
}