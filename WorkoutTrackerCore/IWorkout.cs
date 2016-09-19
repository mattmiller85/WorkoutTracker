using System.Collections.Generic;

namespace WorkoutTrackerCore
{
    public interface IWorkout
    {
        string User { get; set; }
        Dictionary<string, object> Settings { get; set; }
        List<IActivity> Activities { get; set; }
    }
}
