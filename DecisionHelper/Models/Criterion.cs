namespace DecisionHelper.Models
{
    public class Criterion
    {
        public string Name { get; set; } = "";
        public double Weight { get; set; }
        public bool  IsHigher { get; set; } = true;
    }
}
