using System.Threading.Tasks;

namespace core
{
    public interface IPredictor
    {
        Task<Prediction> Predict(ISettings settings, string text);
    }
}