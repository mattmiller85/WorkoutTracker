using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTrackerCore
{
    public interface IActivity
    {
        string Name { get; set; }
        TimeSpan? Duration { get; set; }
        decimal? DistanceMiles { get; set; }
        List<Set> Sets { get; set; }
    }
}
