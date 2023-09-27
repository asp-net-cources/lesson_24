using Lesson24.Data.Models;

namespace Lesson24.Models;

public class BookModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Book;
}
