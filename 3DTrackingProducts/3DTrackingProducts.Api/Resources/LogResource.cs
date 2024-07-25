using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Resources
{
    public class LogResource
    {
        public int RSSI { get; set; }

        public string IPAddress { get; set; }

        public double Angle { get; set; }

        public string TagEPC { get; set; }
    }
}

