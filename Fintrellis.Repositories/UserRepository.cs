using Fintrellis.Common.Extensions;
using Fintrellis.Models;
using Fintrellis.Repositories.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Fintrellis.Repositories;

public class UserRepository : AbstractRepository<User>, IUserRepository
{
    /// <summary>
    /// Creates an instance of <see cref="UserRepository"/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public UserRepository(DataContext context, ILogger<UserRepository> logger)
        : base(context, logger)
    {
    }

    // <summary>
    /// Gets all <see cref="User"/>.
    /// </summary>
    /// <returns>A collection of <see cref="User"/>.</returns>
    public override async Task<IEnumerable<User>> GetAll()
    {
        try
        {
            Logger.LogInformation("Getting all users");

            return await DbContext.Users
                .Include(b=>b.Blogs).ToListAsync();
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, "Error getting all users");
            throw;
        }
    }

    /// <summary>
    /// Gets a single <see cref="User"/> by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An instance of <see cref="User"/>.</returns>
    public override async Task<User> GetById(int id)
    {
        try
        {
            Logger.LogInformation($"Getting user with id {id}");

            return await DbContext.Users.FindAsync(id);
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error getting user with id {id}");
            throw;
        }
    }

    /// <summary>
    /// Adds a new <see cref="User"/> instance.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>An instance of the <see cref="User"/> added.</returns>
    public override async Task<User> Add(User user)
    {
        try
        {
            Logger.LogInformation($"Adding a new user: {user.Username}");

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, "Error adding user");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing <see cref="User"/> instance.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public override async Task<User> Update(User user)
    {
        try
        {
            Logger.LogInformation($"Updating user with id {user.Username}");

            DbContext.Entry(user).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error updating user with id {user.Id}");
            throw;
        }
    }

    /// <summary>
    /// Deletes an existing <see cref="User"/> by <see cref="User.Id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="Boolean.true"/> if successful; otherwise <see cref="Boolean.false"/></returns>
    public override async Task<bool> Delete(int id)
    {
        try
        {
            Logger.LogInformation($"Deleting user with id {id}");

            var user = await DbContext.Users.FindAsync(id);
            if (user == null)
            {
                Logger.LogWarning($"User with id {id} not found");
                return false;
            }

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            ex.LogError(Logger, $"Error deleting user with id {id}");
            throw;
        }
    }
}

