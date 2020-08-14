using Microsoft.ML.Data;

namespace SolcastWebApi.Models
{
    public class EnergyPrediction
    {
        [VectorType(2)]
        public double[] Prediction { get; set; }
    }
}
