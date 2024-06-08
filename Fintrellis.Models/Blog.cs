namespace Fintrellis.Models;

public class Blog : AbstractEntity
{
    public Blog()
    {
        CreatedAt = DateTime.Now;
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // Foreign key property
    public int UserId { get; set; }
    // Navigation property to represent the relationship
    public User User { get; set; }
}


