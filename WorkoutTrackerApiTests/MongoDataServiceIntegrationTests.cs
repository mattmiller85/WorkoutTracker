using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutTrackerApi.Services;
using WorkoutTrackerCore;
using Xunit;

namespace WorkoutTrackerApiTests
{
	public class MongoDataServiceIntegrationTests : IClassFixture<MongoDataServiceFixture>
	{
		[Fact]
		public void ShouldAddWorkoutToDocumentDatabase()
		{
			var workout = new Workout
			{
				Id = ObjectId.Empty.ToString(),
				Date = DateTime.Now,
				User = "_test",
				Activities = new List<Activity>
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

			Assert.NotEqual(ObjectId.Empty.ToString(), insertedWorkout.Id);
		}

		[Fact]
		public void ShouldUpdateWorkoutInDocumentDatabase()
		{
			var workout = new Workout
			{
				Id = "57f8fdc7d873bc085058ce93",
				Date = DateTime.Now,
				User = "_test updated " + new Random().Next().ToString(),
				Activities = new List<Activity>
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
							},
							new Set
							{
								Reps = 8,
								Weight = 110
							}
						}
					}
				}
			};

			var service = new MongoWorkoutDataService();
			service.UpdateWorkout(workout);
			var updatedWorkout = service.GetLatest(5).First(w => w.Id == workout.Id);

			Assert.Equal(workout.User, updatedWorkout.User);
		}

		[Fact]
		public void ShouldGetLatestWorkoutsFromData()
		{
			var workouts = new MongoWorkoutDataService().GetLatest(5);

			Assert.NotEmpty(workouts);
		}

		[Fact]
		public void ShouldDeleteWorkoutFromDocumentDatabase()
		{
			var service = new MongoWorkoutDataService();
			var workout = service.AddWorkout(new Workout { User = "_test" } );

			service.RemoveWorkout(workout.Id);
			var workouts = service.GetLatest(5);

			Assert.Empty(workouts.Where(w => w.Id == workout.Id));
		}

		[Fact]
		public void ShouldGetWorkoutFromDocumentDatabase()
		{
			var service = new MongoWorkoutDataService();
			var workout = service.AddWorkout(new Workout { User = "_test" });

			var retreivedWorkout = service.GetWorkout(workout.Id);

			Assert.NotNull(retreivedWorkout);
		}
	}

	public class MongoDataServiceFixture : IDisposable
	{
		public void Dispose()
		{
			new MongoWorkoutDataService().RemoveAll(w => w.User.StartsWith("_test"));
		}
	}
}