using Fintrellis.Models;


namespace Fintrellis.UnitTests;

public static class StaticData
{
	public static List<User> Users()
	{
		return new List<User>
		{
			new User { Id = 1, Username = "User1" },
			new User { Id = 2, Username = "User2" }
		};
	}

	public static List<Blog> Blogs()
	{
		return new List<Blog>
		{
			new Blog { Id = 1, Title = "Blog 1", UserId = 1 },
			new Blog { Id = 2, Title = "Blog 2", UserId = 2 },
			new Blog { Id = 3, Title = "Blog 3", UserId = 1 }
		};
	}
}

