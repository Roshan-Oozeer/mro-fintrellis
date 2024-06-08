using Fintrellis.Models;


namespace Fintrellis.UnitTests.Repositories;

[Collection("Database collection")]
public class UserRepositoryTests : BaseRepositoryTests
{
    public UserRepositoryTests()
    { }

    [Fact]
    public async Task GetAllUsers_ReturnsAllUsers()
    {
        // Arrange
        var context = CreateContext("all_users");
        var repository = CreateUserRepository(context);

        // Act
        var result = await repository.GetAll();

        // Assert
        Assert.NotNull(result); // Ensure that result is not null
        Assert.Equal(2, result.Count());
        Assert.All(result, user => Assert.NotNull(user.Blogs));
    }

    [Fact]
    public async Task GetById_ReturnsCorrectUser()
    {
        // Arrange
        var context = CreateContext("id_users");
        var repository = CreateUserRepository(context);

        // Act
        var user = await repository.GetById(1);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(1, user.Id);
        Assert.Equal("User1", user.Username);
    }

    [Fact]
    public async Task InsertUser_AddsUserToDatabase()
    {
        // Arrange
        var newUser = new User
        {
            Username = "User3",
        };
        var context = CreateContext("add_users");
        var repository = CreateUserRepository(context);

        // Act
        await repository.Add(newUser);

        // Assert
        var insertedUser = context.Users.Find(3);
        Assert.NotNull(insertedUser);
        Assert.Equal("User3", insertedUser.Username);
    }

    [Fact]
    public async Task UpdateUser_UpdatesUserInDatabase()
    {
        // Arrange
        var context = CreateContext("update_users");
        var userToUpdate = context.Users.First(b => b.Id == 2);
        userToUpdate.Username = "New_User2";
        var repository = CreateUserRepository(context);

        // Act
        await repository.Update(userToUpdate);

        // Assert
        var updatedUser = context.Users.Find(2);
        Assert.NotNull(updatedUser);
        Assert.Equal("New_User2", updatedUser.Username);
    }

    [Fact]
    public async Task DeleteUser_RemovesUserFromDatabase()
    {
        // Arrange
        var context = CreateContext("del_users");
        var repository = CreateUserRepository(context);

        // Act
        bool result = await repository.Delete(2);

        // Assert
        Assert.True(result);
        var deletedUser = context.Blogs.Find(2);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task DeleteUser_CouldNotRemoveUserFromDatabase()
    {
        // Arrange
        var context = CreateContext("not_del_users");
        var repository = CreateUserRepository(context);

        // Act
        bool result = await repository.Delete(10);

        // Assert
        Assert.False(result);
    }
}
