using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using SolcastWebApi.Models;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;

namespace SolcastWebApi.Controllers
{
    //[Route("[controller]")]
    public class EnergyForecastController : ControllerBase
    {
        // private static readonly string[] Summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        private readonly ILogger<EnergyForecastController> _logger;

        public EnergyForecastController(ILogger<EnergyForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }

        [HttpGet]  
        [Route("[controller]/GetEnergyDataAnamoly")]
        public IEnumerable<String> GetEnergyDataAnamoly()  
        {  
            var context = new MLContext();

            var data = context.Data.LoadFromTextFile<EnergyData>("./Data/energy_hourly.csv", 
                hasHeader: true, 
                separatorChar: ',');

            var pipeline = context.Transforms.DetectSpikeBySsa(nameof(EnergyPrediction.Prediction), nameof(EnergyData.Energy),
                confidence: 98, trainingWindowSize: 90, seasonalityWindowSize: 30, pvalueHistoryLength: 30);

            var transformedData = pipeline.Fit(data).Transform(data);

            var predictions = context.Data.CreateEnumerable<EnergyPrediction>(transformedData, reuseRowObject: false).ToList();

            var energy = data.GetColumn<float>("Energy").ToArray();
            var date = data.GetColumn<DateTime>("Date").ToArray();

            var output = new List<string>();

            for (int i = 0; i < predictions.Count(); i++)
            {
                if (predictions[i].Prediction[0] == 1)
                {
                    output.Add(String.Format("{0}   {1:0.0000}  {2:0.00}    {3:0.00}    {4:0.00}", date[i], energy[i], predictions[i].Prediction[0], predictions[i].Prediction[1], predictions[i].Prediction[2]));                    
                }
            }

            return output.ToList();
        }  

        [HttpGet]  
        [Route("[controller]/GetTimeSeriesForecast")]
        public IEnumerable<float> GetTimeSeriesForecast()  
        {  
            var context = new MLContext();

            var data = context.Data.LoadFromTextFile<EnergyData>("./Data/energy_hourly.csv",
                hasHeader: true, separatorChar: ',');

            var pipeline = context.Forecasting.ForecastBySsa(
                nameof(EnergyForecast.Forecast),
                nameof(EnergyData.Energy),
                windowSize: 5,
                seriesLength: 10,
                trainSize: 100,
                horizon: 4);

            var model = pipeline.Fit(data);

            var forecastingEngine = model.CreateTimeSeriesEngine<EnergyData, EnergyForecast>(context);

            var forecasts = forecastingEngine.Predict();
            
            var output = new List<float>();
            foreach (var forecast in forecasts.Forecast)
            {
                output.Add(forecast);
            }
            
            return output.ToList();
        }

    }
}
