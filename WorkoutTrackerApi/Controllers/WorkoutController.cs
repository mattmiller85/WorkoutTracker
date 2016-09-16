using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Controllers
{
    public class WorkoutController : Controller
    {
        [HttpGet]
        [Route("api/workouts/latest/{count}")]
        public IEnumerable<Workout> GetLatest(int count)
        {
            return new List<Workout>{ new Workout() };
        }
    }
}
