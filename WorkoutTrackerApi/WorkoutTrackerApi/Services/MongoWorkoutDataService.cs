using System;
using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Services
{
	public class MongoWorkoutDataService : IWorkoutDataService
	{
		public List<Workout> GetLatest(int count)
		{
			var client = new MongoClient(ConfigurationManager.AppSettings["MongoServer"]);
			var db = client.GetDatabase("workouttracker");
			var collection = db.GetCollection<Workout>("workouts");

			return new List<Workout>();
		}

		public Workout AddUpdateWorkout(IWorkout workout)
		{
			throw new NotImplementedException();
		}
	}
}