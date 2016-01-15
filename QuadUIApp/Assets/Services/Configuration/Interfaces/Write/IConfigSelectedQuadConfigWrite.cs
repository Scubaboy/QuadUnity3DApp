using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Configuration.Interfaces.Write
{
    public interface IConfigSelectedQuadConfigWrite
    {
        CommsOptions Comms { set; }
        IMUOpions IMU { set; }
        GPSOptions GPS { set; }
        AltimeterOptions Alt { set; }
    }
}
