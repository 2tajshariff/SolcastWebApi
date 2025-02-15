
using Microsoft.ML.Data;
using System;

namespace SolcastWebApi.Models
{
    public class EnergyData
    {
        [LoadColumn(0)]
        public DateTime Date { get; set; }

        [LoadColumn(1)]
        public float Energy { get; set; }
    }
}
