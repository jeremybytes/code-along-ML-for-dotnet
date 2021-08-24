using System.Collections.Generic;

namespace csharp
{
    public interface IClassifier
    {
        void Train(IEnumerable<Observation> trainingSet);
        string Predict(int[] pixels);
    }
}