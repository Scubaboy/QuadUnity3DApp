using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Configuration.Interfaces.Read
{
    public interface IConfigSelectedQuadConfigRead
    {
        CommsOptions Comms { get; }
        IMUOpions IMU { get; }
        GPSOptions GPS { get; }
        AltimeterOptions Alt { get; }
    }
}
