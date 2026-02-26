using System;

namespace DecisionHelper.Models
{
    public class UserDecisionOption
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string OptionName { get; set; } = "";
        public int FlairId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserCriterion
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string CriterionName { get; set; } = "";
        public bool IsHigher { get; set; }
        public int FlairId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}