using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Configuration.Interfaces.Write
{
    public interface IConfigMissionPlanWrite
    {
        List<GPSLocation>  MissionPlan { set; }
    }
}
