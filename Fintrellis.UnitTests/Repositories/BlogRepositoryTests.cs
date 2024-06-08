using Fintrellis.Models;


namespace Fintrellis.UnitTests.Repositories;

[Collection("Database collection")]
public class BlogRepositoryTests : BaseRepositoryTests
{
    public BlogRepositoryTests()
    { }

    [Fact]
    public async Task GetAllBlogs_ReturnsAllBlogs()
    {
        // Arrange
        var context = CreateContext("all_blogs");
        var repository = CreateBlogRepository(context);

        // Act
        var result = await repository.GetAll();

        // Assert
        Assert.NotNull(result); // Ensure that result is not null
        Assert.Equal(3, result.Count());
        Assert.All(result, blog => Assert.NotNull(blog.User));
    }

    [Fact]
    public async Task GetById_ReturnsCorrectBlog()
    {
        // Arrange
        var context = CreateContext("id_blogs");
        var repository = CreateBlogRepository(context);

        // Act
        var blog = await repository.GetById(1);

        // Assert
        Assert.NotNull(blog);
        Assert.Equal(1, blog.Id);
        Assert.Equal("Blog 1", blog.Title);
        Assert.NotNull(blog.User);
        Assert.Equal(1, blog.UserId);
    }

    [Fact]
    public async Task InsertBlog_AddsBlogToDatabase()
    {
        // Arrange
        var newBlog = new Blog
        {
            Title = "New Blog",
            Content = "New Content",
            UserId = 1
        };
        var context = CreateContext("add_blogs");
        var repository = CreateBlogRepository(context);

        // Act
        await repository.Add(newBlog);

        // Assert
        var insertedBlog = context.Blogs.Find(4);
        Assert.NotNull(insertedBlog);
        Assert.Equal("New Blog", insertedBlog.Title);
        Assert.Equal("New Content", insertedBlog.Content);
        Assert.Equal(1, insertedBlog.UserId);
    }

    [Fact]
    public async Task UpdateBlog_UpdatesBlogInDatabase()
    {
        // Arrange
        var context = CreateContext("update_blogs");
        var blogToUpdate = context.Blogs.First(b => b.Id == 1);
        blogToUpdate.Title = "Updated Blog 1";
        var repository = CreateBlogRepository(context);

        // Act
        await repository.Update(blogToUpdate);

        // Assert
        var updatedBlog = context.Blogs.Find(blogToUpdate.Id);
        Assert.NotNull(updatedBlog);
        Assert.Equal("Updated Blog 1", updatedBlog.Title);
    }

    [Fact]
    public async Task DeleteBlog_RemovesBlogFromDatabase()
    {
        // Arrange
        var context = CreateContext("del_blogs");
        var repository = CreateBlogRepository(context);

        // Act
        bool result = await repository.Delete(3);

        // Assert
        Assert.True(result);
        var deletedBlog = context.Blogs.Find(3);
        Assert.Null(deletedBlog);
    }

    [Fact]
    public async Task DeleteBlog_CouldNotRemoveBlogFromDatabase()
    {
        // Arrange
        var context = CreateContext("not_del_blogs");
        var repository = CreateBlogRepository(context);

        // Act
        bool result = await repository.Delete(10);

        // Assert
        Assert.False(result);
    }
}
