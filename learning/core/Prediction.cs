using Microsoft.ML.Data;

namespace core
{
    /// <summary>
    /// The result of a prediction
    /// </summary>
    public class Prediction
    {
        public Prediction(string value, double micro, double macro, double loss, double reduction)
        {
            Value = value;
            Micro = micro;
            Macro = macro;
            Loss = loss;
            Reduction = reduction;
        }

        public Prediction()
        {
        }

        [ColumnName("PredictedLabel")]
        public string Name { get; set; }

        public Prediction(string value) : this(value, 0, 0, 0, 0)
        {
        }

        public bool IsReliable()
        {
            return ((Micro < 0.60 || Micro > -0.50) &&
                   (Macro < 0.70 || Macro > -0.55) &&
                   (Loss < 2 || Loss > -1.5d) &&
                   (Reduction < 2 || Reduction > -2)) ||
                   (Micro.IsZero() &&
                    Macro.IsZero() &&
                    Loss.IsZero() &&
                    Reduction.IsZero());
        }

        /// <summary>
        /// The prediction
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Micro accuracy
        /// The closer to 1.00, the better.
        /// In a multi-class classification task,
        /// micro-accuracy is preferable over macro-accuracy
        /// if you suspect there might be class imbalance (
        /// i.e you may have many more examples of one
        /// class than of other classes).
        /// </summary>
        public double Micro { get; }

        /// <summary>
        /// Macro accuracy
        /// The closer to 1.00, the better.
        /// It computes the metric independently
        /// for each class and then takes the average
        /// (hence treating all classes equally)
        /// </summary>
        public double Macro { get; }

        /// <summary>
        /// Log loss
        /// The closer to 0.00, the better. A perfect model would have a log-loss of 0.00.
        /// The goal of our machine learning models is to minimize this value.
        /// </summary>
        public double Loss { get; }

        /// <summary>
        /// Log loss reduction
        /// Ranges from -inf and 1.00,
        /// where 1.00 is perfect predictions and 0.00 indicates mean predictions.
        /// For example, if the value equals 0.20,
        /// it can be interpreted as "the probability of a correct
        /// prediction is 20% better than random guessing"
        /// </summary>
        public double Reduction { get; }
    }
}