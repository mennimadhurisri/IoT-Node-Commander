using System;
using System.Collections.Generic;
using System.Text;

namespace iotcommender.models
{
    public class Sensorreading
    {
        public int Sensorid { get; set; }

        public double Temperature { get; set; }

        public DateTime timestamp { get; set; }
    }
}
