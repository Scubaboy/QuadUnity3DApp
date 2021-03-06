﻿using Newtonsoft.Json;

namespace Assets.Services.Models
{
    public class ActiveQuad
    {
        /// <summary>
        /// Converts the class into a json object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int Id { get; set; }
        public string QuadId { get; set; }
        public CommsOptions SupportedComms { get; set; }
        public IMUOpions SupportedIMU { get; set; }
        public GPSOptions SupportGPS { get; set; }
        public AltimeterOptions SupportedAlt { get; set; }
        public bool InUse { get; set; }
    }
}
