namespace Assets.Services.Configuration.Interfaces.Write
{
    public interface IConfigSelectedQuadCaliWrite
    {
        double GyroXAxisBias { set; }
        double GyroYAxisBias { set; }
        double GyroZAxisBias { set; }

        double AccelXAxisBias { set; }
        double AccelYAxisBias { set; }
        double AccelZAxisBias { set; }

        double MagXAxisBias { set; }
        double MagYAxisBias { set; }
        double MagZAxisBias { set; }
    }
}
