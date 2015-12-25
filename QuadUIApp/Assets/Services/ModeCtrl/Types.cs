using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl
{
    /// <summary>
    /// 
    /// </summary>
    public enum Modes
    {
        /// <summary>
        /// 
        /// </summary>
        Initialise,

        /// <summary>
        /// 
        /// </summary>
        SelectQuad,

        /// <summary>
        /// 
        /// </summary>
        ConfigureQuad,

        /// <summary>
        /// 
        /// </summary>
        CalibrateTestQuad,

        /// <summary>
        /// 
        /// </summary>
        MissionPlan,

        /// <summary>
        /// 
        /// </summary>
        Execute
    };

    public enum ExecutionStatus
    {
        Complete,

        Canceled,

        Running,

        Requested
    };

    public struct ModeMapping
    {
        private Modes canceledMode;
        private Modes completedMode;

        public ModeMapping(Modes canceled, Modes Completed)
        {
            this.canceledMode = canceled;
            this.completedMode = Completed;
        }
        public Modes Canceled
        {
            get
            {
                return canceledMode;
            }

        }

        public Modes Completed
        {
            get
            {
                return this.completedMode;
            }
        }
    }
}
