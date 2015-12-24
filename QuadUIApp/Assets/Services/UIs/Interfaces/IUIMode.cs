using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.UIs.Interfaces
{
    public interface IUIMode
    {
        void Execute();

        bool IsExecuting();
    }
}
