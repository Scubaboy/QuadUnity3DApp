using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Models
{
    public class QuadSelectionConfirmed
    {
        public ActiveQuad TheQuad {get;set;}

        public bool Confirmed { get; set; }
    }
}
