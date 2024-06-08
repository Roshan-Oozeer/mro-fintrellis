using Fintrellis.Models;
using Fintrellis.Repositories;


namespace Fintrellis.WebApi.Controllers;

public class UserController : AbstractController<IUserRepository, User>
{
	public UserController(IUserRepository repository, ILogger<UserController> logger)
        : base(repository, logger)
    { }
}


