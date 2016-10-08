using MongoDB.Bson;
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
				Id = ObjectId.Empty,
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

			var insertedWorkout = new MongoWorkoutDataService().AddWorkout(workout);

			Assert.NotEqual(ObjectId.Empty, insertedWorkout.Id);
		}

		[Fact]
		public void ShouldGetLatestWorkoutsFromData()
		{
			var workouts = new MongoWorkoutDataService().GetLatest(5);

			Assert.NotEmpty(workouts);
		}
	}
}