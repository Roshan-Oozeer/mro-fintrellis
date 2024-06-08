using Fintrellis.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;


namespace Fintrellis.UnitTests.Repositories;

public class BaseRepositoryTests
{
    protected static DataContext CreateContext(string db_name)
    {
        // Setup the in-memory database and the context
        var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(databaseName: db_name)
                      .Options;
        var mockAppContextLogger = new Mock<ILogger<DataContext>>();
        var context = new DataContext(options, mockAppContextLogger.Object);

        // Seed the in-memory database with users and blogs
        context.Users.AddRange(StaticData.Users());
        context.Blogs.AddRange(StaticData.Blogs());

        context.SaveChanges();

        return context;
    }

    protected UserRepository CreateUserRepository(DataContext context)
    {
        var mockLogger = new Mock<ILogger<UserRepository>>();

        return new UserRepository(context, mockLogger.Object);
    }

    protected BlogRepository CreateBlogRepository(DataContext context)
    {
        var mockLogger = new Mock<ILogger<BlogRepository>>();

        return new BlogRepository(context, mockLogger.Object);
    }
}

