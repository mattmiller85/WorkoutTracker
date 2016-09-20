using System;
using System.Collections.Generic;
using WorkoutTrackerApi.Services;
using WorkoutTrackerCore;
using Xunit;

namespace WorkoutTrackerApiTests
{
	public class MongoDataServiceIntegrationTests
	{
		[Fact]
		public void ShouldAddWorkoutToDocumentDatabase()
		{
			var workout = new Workout
			{
				Id = string.Empty,
				Date = DateTime.Now,
				User = "test",
				Activities = new List<IActivity>
				{
					new Activity
					{
						Name = "TestWorkout",
						Sets = new List<Set>
						{
							new Set
							{
								Reps = 10,
								Weight = 100
							}
						}
					}
				}
			};

			var insertedWorkout = new MongoWorkoutDataService().AddUpdateWorkout(workout);

			Assert.NotEqual(string.Empty, insertedWorkout.Id);
		}
	}
}