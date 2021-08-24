using System;
using System.Linq;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;

            var distance = new ManhattanDistance();
            var classifier = new BasicClassifier(distance);

            var trainingPath = @"..\data\trainingsample.csv";
            var training = DataReader.ReadObservations(trainingPath);
            classifier.Train(training);

            var validationPath = @"..\data\validationsample.csv";
            var validation = DataReader.ReadObservations(validationPath);

            var correct = Evaluator.Correct(validation, classifier);
            Console.WriteLine($"Correctly classified: {correct:P2}");

            var elapsed = DateTime.Now - startTime;
            Console.WriteLine($"Total Time: {elapsed}");
        }
    }
}
