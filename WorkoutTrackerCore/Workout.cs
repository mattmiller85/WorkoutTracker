using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTrackerCore
{
    public class Workout : IWorkout
    {
        public string User { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public List<IActivity> Activities { get; set; }
    }
}
