using iotcommender.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace iotcommender.services
{
    internal class Hardwareparserservice
    {

        public Sensorreading? Parse(string rawData)
        {
            try
            {
                string[] parts = rawData.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 4)
                    return null;

                int header = Convert.ToInt32(
                    parts[0].Replace("0x", ""),
                    16);

                int sensorId = Convert.ToInt32(
                    parts[1].Replace("0x", ""),
                    16);

                int a = Convert.ToInt32(
                    parts[2].Replace("0x", ""),
                    16);

                int b = Convert.ToInt32(
                    parts[3].Replace("0x", ""),
                    16);

                if (header != 0xAA)
                    return null;

                if (sensorId != 0x02)
                    return null;

                double temperature =
                    ((a * 256) + b) / 10.0;

                return new Sensorreading
                {
                    Sensorid = sensorId,
                    Temperature = temperature,
                    timestamp = DateTime.Now
                };
            }
            catch
            {
                return null;
            }
        }
    }
}


        
    
