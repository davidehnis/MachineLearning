using System.Threading.Tasks;

namespace core
{
    public interface IPredictor
    {
        Task<Prediction> Predict(string modelPath, string text);
    }
}