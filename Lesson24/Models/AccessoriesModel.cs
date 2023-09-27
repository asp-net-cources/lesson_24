using Lesson24.Data.Models;

namespace Lesson24.Models;

public class AccessoriesModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Accessories;
}
