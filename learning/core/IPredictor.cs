using System.Threading.Tasks;

namespace core
{
    /// <summary>
    /// Uses Categorization to predict the correct answer given an amount of text
    /// </summary>
    public interface IPredictor
    {
        Task<Prediction> Predict(ISettings settings, string text);
    }
}