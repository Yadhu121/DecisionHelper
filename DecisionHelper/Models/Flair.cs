namespace DecisionHelper.Models
{
    public class Flair
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<DecisionRecord> DecisionRecords { get; set; } = new List<DecisionRecord>();
        public ICollection<DecisionOption> DecisionOptions { get; set; } = new List<DecisionOption>();
    }
}
