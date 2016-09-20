using System;
using System.Collections.Generic;

namespace WorkoutTrackerCore
{
    public class Workout : IWorkout
    {
        public string User { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public List<IActivity> Activities { get; set; }
    }
}
