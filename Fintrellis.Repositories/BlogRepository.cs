using Fintrellis.Common.Extensions;
using Fintrellis.Models;
using Fintrellis.Repositories.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Fintrellis.Repositories;

public class BlogRepository : AbstractRepository<Blog>, IBlogRepository
{
    /// <summary>
    /// Creates an instance of <see cref="BlogRepository"/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public BlogRepository(DataContext context, ILogger<BlogRepository> logger)
		: base(context, logger)
	{
	}

    // <summary>
    /// Gets all <see cref="Blog"/>.
    /// </summary>
    /// <returns>A collection of <see cref="Blog"/>.</returns>
    public override async Task<IEnumerable<Blog>> GetAll()
    {
        try
        {
            Logger.LogInformation("Getting all blogs");

            return await DbContext.Blogs
                .Include(u=>u.User)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, "Error getting all blogs");
            throw;
        }
    }

    /// <summary>
    /// Gets a single <see cref="Blog"/> by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An instance of <see cref="Blog"/>.</returns>
    public override async Task<Blog> GetById(int id)
    {
        try
        {
            Logger.LogInformation($"Getting blog with id {id}");

            return await DbContext.Blogs.FindAsync(id);
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error getting blog with id {id}");
            throw;
        }
    }

    /// <summary>
    /// Adds a new <see cref="Blog"/> instance.
    /// </summary>
    /// <param name="blog"></param>
    /// <returns>An instance of the <see cref="Blog"/> added.</returns>
    public override async Task<Blog> Add(Blog blog)
    {
        try
        {
            Logger.LogInformation($"Adding a new product: {blog.Title}");

            DbContext.Blogs.Add(blog);
            await DbContext.SaveChangesAsync();
            return blog;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, "Error adding blog");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing <see cref="Blog"/> instance.
    /// </summary>
    /// <param name="blog"></param>
    /// <returns></returns>
    public override async Task<Blog> Update(Blog blog)
    {
        try
        {
            Logger.LogInformation($"Updating blog with id {blog.Id}");

            DbContext.Entry(blog).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return blog;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error updating blog with id {blog.Id}");
            throw;
        }
    }

    /// <summary>
    /// Deletes an existing <see cref="Blog"/> by <see cref="Blog.Id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="Boolean.true"/> if successful; otherwise <see cref="Boolean.false"/></returns>
    public override async Task<bool> Delete(int id)
    {
        try
        {
            Logger.LogInformation($"Deleting blog with id {id}");

            var blog = await DbContext.Blogs.FindAsync(id);
            if (blog == null)
            {
                Logger.LogWarning($"Blog with id {id} not found");
                return false;
            }

            DbContext.Blogs.Remove(blog);
            await DbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error deleting blog with id {id}");
            throw;
        }
    }
}

