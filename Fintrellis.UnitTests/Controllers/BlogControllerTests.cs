using Fintrellis.Models;
using Fintrellis.Repositories;
using Fintrellis.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace Fintrellis.UnitTests.Controllers;

public class BlogControllerTests
{
    private readonly Mock<IBlogRepository> _mockRepository;
    private readonly BlogController _controller;

    public BlogControllerTests()
    {
        _mockRepository = new Mock<IBlogRepository>();
        var mockLogger = new Mock<ILogger<BlogController>>();
        _controller = new BlogController(_mockRepository.Object, mockLogger.Object);
    }

    [Fact]
    public async Task GetAllBlogs_ReturnsOkResult_WithBlogList()
    {
        // Arrange
        IEnumerable<Blog> blogs = new List<Blog>
        {
            new Blog { Id = 1, Title = "Blog 1" },
            new Blog { Id = 2, Title = "Blog 2" }
        };
        _mockRepository.Setup(repo => repo.GetAll()).Returns(
            Task.FromResult(blogs));

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(blogs, result.Value);
    }

    [Fact]
    public async Task GetBlogById_ExistingId_ReturnsOkResult_WithBlog()
    {
        // Arrange
        var blogId = 1;
        var blog = new Blog { Id = blogId, Title = "Blog 1" };
        _mockRepository.Setup(repo => repo.GetById(blogId)).Returns(
            Task.FromResult(blog));

        // Act
        var result = await _controller.GetById(blogId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(blog, result.Value);
    }

    [Fact]
    public async Task GetBlogById_NotExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var blogId = 10;
        Blog blog = null;
        _mockRepository.Setup(repo => repo.GetById(blogId)).Returns(
            Task.FromResult(blog));

        // Act
        var result = await _controller.GetById(blogId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task AddBlog_ValidData_ReturnsCreatedAtActionResult_WithBlog()
    {
        // Arrange
        var newBlog = new Blog { Id = 1, Title = "New Blog" };
        _mockRepository.Setup(repo => repo.Add(newBlog)).Returns(
            Task.FromResult(newBlog));

        // Act
        var result = await _controller.Add(newBlog) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(nameof(BlogController.GetById), result.ActionName);
        Assert.Equal(newBlog, result.Value);
    }

    [Fact]
    public async Task UpdateBlog_ExistingId_ValidData_ReturnsNoContentResult()
    {
        // Arrange
        var blogId = 1;
        var updatedBlog = new Blog { Id = blogId, Title = "Updated Blog" };

        // Act
        var result = await _controller.Update(blogId, updatedBlog) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public async Task UpdateBlog_ExistingId_InvalidData_ReturnsBadRequestResult()
    {
        // Arrange
        var blogId = 1;
        var updatedBlog = new Blog { Id = blogId, Title = "Updated Blog" };

        // Act
        var result = await _controller.Update(10, updatedBlog) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task DeleteBlog_ExistingId_ReturnsNoContentResult()
    {
        // Arrange
        var blogId = 1;
        _mockRepository.Setup(repo => repo.Delete(blogId)).Returns(
            Task.FromResult(true));

        // Act
        var result = await _controller.Delete(blogId) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public async Task DeleteBlog_NotExistingId_ReturnsNotFoundResult()
    {
        // Arrange
        var blogId = 1;
        _mockRepository.Setup(repo => repo.Delete(blogId)).Returns(
            Task.FromResult(false));

        // Act
        var result = await _controller.Delete(blogId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }
}

