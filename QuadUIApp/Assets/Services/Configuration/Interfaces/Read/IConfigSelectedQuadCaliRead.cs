using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Configuration.Interfaces.Read
{
    public interface IConfigSelectedQuadCaliRead
    {
        double GyroXAxisBias { get; }
        double GyroYAxisBias { get; }
        double GyroZAxisBias { get; }

        double AccelXAxisBias { get; }
        double AccelYAxisBias { get; }
        double AccelZAxisBias { get; }

        double MagXAxisBias { get; }
        double MagYAxisBias { get; }
        double MagZAxisBias { get; }
    }
}
