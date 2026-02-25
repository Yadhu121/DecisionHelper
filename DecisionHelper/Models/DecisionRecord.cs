namespace DecisionHelper.Models
{
    public class DecisionRecord
    {
        public int Id { get; set; }
        public int FlairId { get; set; }
        public Flair Flair { get; set; } = null!;
        public string WinningOption { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}