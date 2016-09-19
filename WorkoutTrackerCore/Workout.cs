using System.Collections.Generic;

namespace WorkoutTrackerCore
{
    public class Workout : IWorkout
    {
        public string User { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public List<IActivity> Activities { get; set; }
    }
}
