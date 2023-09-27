using Lesson24.Data.Models;

namespace Lesson24.Models;

public class FoodModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Food;
}
