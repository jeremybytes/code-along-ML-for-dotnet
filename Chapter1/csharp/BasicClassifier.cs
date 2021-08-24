using System.Collections.Generic;

namespace csharp
{
    public class BasicClassifier : IClassifier
    {
        private IEnumerable<Observation> data;
        private readonly IDistance distance;

        public BasicClassifier(IDistance distance)
        {
            this.distance = distance;
        }

        public void Train(IEnumerable<Observation> trainingSet)
        {
            this.data = trainingSet;
        }

        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = double.MaxValue;

            foreach (Observation obs in this.data)
            {
                var dist = this.distance.Between(obs.Pixels, pixels);
                if (dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }

            return currentBest.Label;
        }
    }
}