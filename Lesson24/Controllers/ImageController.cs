using System.Net;
using Lesson24.Data.EF;
using Lesson24.Data;
using Lesson24.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson24.Controllers;

[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IDataContext _context;

    public ImageController(IDataContext context)
    {
        _context = context;
    }
    
    [HttpGet("{id}")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public ActionResult GetProductImage([FromRoute]int id)
    {
        var client = new WebClient();
        var data = client.DownloadData("https://picsum.photos/200/300");
        
        return File(data, "image/jpeg");
    }
}