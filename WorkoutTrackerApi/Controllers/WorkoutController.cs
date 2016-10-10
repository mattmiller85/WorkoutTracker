using MongoDB.Bson;
using System.Collections.Generic;
using System.Web.Http;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Controllers
{
	public class WorkoutController : ApiController
	{
		private readonly IWorkoutDataService _dataService;
		public WorkoutController(IWorkoutDataService dataService)
		{
			_dataService = dataService;
		}

		[HttpGet]
		[Route("api/workouts/latest/{count}")]
		public IEnumerable<Workout> GetLatest(int count)
		{
			return _dataService.GetLatest(count);
		}

		[HttpPost]
		[Route("api/workouts/new")]
		public Workout AddWorkout(Workout workout)
		{
			return _dataService.AddWorkout(workout);
		}

		[HttpDelete]
		[Route("api/workouts/delete")]
		public void RemoveWorkout(ObjectId workoutId)
		{
			_dataService.RemoveWorkout(workoutId);
		}

		[HttpPut]
		[Route("api/workouts/update")]
		public void UpdateWorkout(Workout workout)
		{
			_dataService.UpdateWorkout(workout);
		}
	}
}