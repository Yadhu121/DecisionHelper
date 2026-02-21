namespace DecisionMaker.Models;

public class User{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List <PasswordResetToken> PasswordResetTokens { get; set; } = [];
}

public class PasswordResetToken
{
    public int      Id        { get; set; }
    public int      UserId    { get; set; }
    public User     User      { get; set; } = null!;
    public string   Token     { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
    public bool     Used      { get; set; } = false;
}