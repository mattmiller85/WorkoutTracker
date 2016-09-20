using System.Collections.Generic;

namespace WorkoutTrackerCore
{
	public interface IWorkoutDataService
	{
		List<Workout> GetLatest(int count);
		Workout AddUpdateWorkout(Workout workout);
		void RemoveWorkout(string workoutId);
	}
}