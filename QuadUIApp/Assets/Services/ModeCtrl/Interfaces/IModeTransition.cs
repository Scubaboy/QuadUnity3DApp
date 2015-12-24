using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    public interface IModeTransition
    {
        Modes ActiveMode { get; set; }
        bool Complete { get; set; }
    }
}
