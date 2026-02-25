namespace DecisionHelper.Models
{
    public class DecisionInput
    {
        public List<Options > Options { get; set; } = new();
        public List<Criterion> Criteria { get; set; } = new();
        public List<ScoreEntry> Scores { get; set; } = new();
        public List<ChooseFromTwo> PairwiseChoices { get; set; } = new();
    }
}