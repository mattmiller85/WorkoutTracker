using System.Collections.Generic;
using System.Web.Http;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Controllers
{
	public class WorkoutController : ApiController
	{
		[HttpGet]
		[Route("api/workouts/latest/{count}")]
		public IEnumerable<Workout> GetLatest(int count)
		{
			return new List<Workout> {new Workout()};
		}
	}
}