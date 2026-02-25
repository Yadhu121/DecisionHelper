namespace DecisionHelper.Models
{
    public class DecisionOption
    {
        public int Id { get; set; }
        public int FlairId { get; set; }
        public Flair Flair { get; set; } = null!;
        public string OptionName { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}