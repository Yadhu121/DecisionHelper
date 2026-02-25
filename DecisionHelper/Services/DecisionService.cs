using DecisionHelper.Models;

namespace DecisionHelper.Services
{
    public class DecisionService
    {
        private const double AddValue = 0.12;

        private double[] CalculateROCweights(int n)
        {
            if (n == 0) return Array.Empty<double>();

            var raw = new double[n];
            for(int k = 0; k < n; k++ )
            {
                double sum = 0;
                for (int j = k; j < n; j++)
                    sum += 1.0 / (j + 1);
                raw[k] = sum / n;
            }

            double total = raw.Sum();
            return raw.Select(w => w / total).ToArray();
        }

        private void ApplyPairwiseNudges(
            double[] weights,
            List<Criterion> criteria,
            List<ChooseFromTwo> choices)
        {
            var top3 = criteria.Take(3).ToList();

            foreach (var choice in choices)
            {
                int ai = top3.FindIndex(c => c.Name == choice.CriteriaA);
                int bi = top3.FindIndex(c => c.Name == choice.CriteriaB);

                if (ai == -1 || bi == -1) continue;

                if (choice.Winner == "A")
                {
                    double transfer = weights[bi] * AddValue;
                    weights[ai] += transfer;
                    weights[bi] -= transfer;
                }
                else if (choice.Winner == "B")
                {
                    double transfer = weights[ai] * AddValue;
                    weights[bi] += transfer;
                    weights[ai] -= transfer;
                }
            }
        }

        private void Normalize(double[] weights)
        {
            double total = weights.Sum();
            if (total == 0) return;
            for (int i = 0; i < weights.Length; i++)
                weights[i] /= total;
        }

        public Dictionary<string, double> Calculate(DecisionInput input)
        {
            double[] weights = CalculateROCweights(input.Criteria.Count);
            ApplyPairwiseNudges(weights, input.Criteria, input.PairwiseChoices);
            Normalize(weights);

            for (int i = 0; i < input.Criteria.Count; i++)
                input.Criteria[i].Weight = weights[i];

            var results = new Dictionary<string, double>();
            foreach (var criterion in input.Criteria)
            {
                var criterionScores = input.Scores
                    .Where(s => s.CriterionName == criterion.Name)
                    .ToList();

                double min = criterionScores.Min(s => s.Value);
                double max = criterionScores.Max(s => s.Value);
                double range = max - min;

                foreach (var score in criterionScores)
                {
                    double normalized = range == 0
                        ? 1.0
                        : (score.Value - min) / range;

                    if (!criterion.IsHigher)
                        normalized = 1.0 - normalized;

                    if (!results.ContainsKey(score.OptionName))
                        results[score.OptionName] = 0;

                    results[score.OptionName] += criterion.Weight * normalized;
                }
            }

            return results
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
