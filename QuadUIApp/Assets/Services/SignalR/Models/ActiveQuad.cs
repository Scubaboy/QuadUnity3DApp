namespace Assets.Services.SignalR.Models
{
    public class ActiveQuad
    {
        /// <summary>
        /// Converts the class into a json object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "{\"QuadId\":{0}\",\"SupportedComms\":\"{1}\",\"SupportedIMU\":\"{2}\",\"SupportGPS\":\"{3}\",\"SupportedAlt\":\"{4}\",\"InUse\":\"{5}\"}", 
                this.QuadId,
                this.SupportedComms,
                this.SupportedIMU,
                this.SupportGPS,
                this.SupportedAlt,
                this.InUse);
        }

        public string QuadId { get; set; }
        public CommsOptions SupportedComms { get; set; }
        public IMUOpions SupportedIMU { get; set; }
        public GPSOptions SupportGPS { get; set; }
        public AltimeterOptions SupportedAlt { get; set; }
        public bool InUse { get; set; }
    }
}
