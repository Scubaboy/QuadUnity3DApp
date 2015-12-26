using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    public interface IModeTracking
    {
        Modes ActiveMode { get; }
        ExecutionStatus Status { get; }
    }
}
