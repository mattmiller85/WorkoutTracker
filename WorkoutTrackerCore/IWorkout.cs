using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTrackerCore
{
    public interface IWorkout
    {
        string User { get; set; }
        Dictionary<string, object> Settings { get; set; }
        List<IActivity> Activities { get; set; }
    }
}
