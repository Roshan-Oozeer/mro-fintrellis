using Fintrellis.Models;
using Fintrellis.Repositories;


namespace Fintrellis.WebApi.Controllers;

public class BlogController : AbstractController<IBlogRepository, Blog>
{
    public BlogController(IBlogRepository repository, ILogger<BlogController> logger)
        : base(repository, logger)
    { }
}

