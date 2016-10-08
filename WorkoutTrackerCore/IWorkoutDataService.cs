using MongoDB.Bson;
using System.Collections.Generic;

namespace WorkoutTrackerCore
{
	public interface IWorkoutDataService
	{
		List<Workout> GetLatest(int count);
		Workout AddWorkout(Workout workout);
		Workout UpdateWorkout(Workout workout);
		void RemoveWorkout(ObjectId workoutId);
	}
}