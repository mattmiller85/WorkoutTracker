using System;
using System.Collections.Generic;

namespace WorkoutTrackerCore
{
    public class Activity : IActivity
    {
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public decimal? DistanceMiles { get; set; }
        public List<Set> Sets { get; set; }
    }
}
