using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
