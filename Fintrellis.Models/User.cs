namespace Fintrellis.Models;

public class User : AbstractEntity
{
    public User()
    {
        Blogs = new List<Blog>();
    }

    public string Username { get; set; }
    public string Email { get; set; }

    public List<Blog> Blogs { get; set; } // Navigation property
}

