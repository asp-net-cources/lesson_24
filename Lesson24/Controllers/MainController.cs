using System.Collections.Generic;
using System.Diagnostics;
using Lesson24.Data.EF;
using Lesson24.Data;
using Lesson24.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lesson24.Controllers;

public class MainController : Controller
{
    public IDataContext _dataContext;


    private readonly ILogger<MainController> _logger;
    public MainController(ILogger<MainController> logger, IDataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    public async Task<IActionResult> Index()
    {
        var dbProducts =  await _dataContext.SelectProducts();
        var products = dbProducts.Select(dbProduct =>
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

        var model = new IndexModel {
            Products = products
        };

        return View(model);
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromForm]ProductModel newProduct)
    {
        await _dataContext.InsertProduct(new Data.Models.Product() {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Author = "user",
            ProductType = newProduct.ProductType
        });
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}