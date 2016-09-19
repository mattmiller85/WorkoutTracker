using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Services
{
	public class MongoWorkoutDataService : IWorkoutDataService
	{
		public List<Workout> GetLatest(int count)
		{
			var client = new MongoDB.Driver.MongoClient(ConfigurationManager.AppSettings["MongoServer"]);
			var db = client.GetDatabase("workouttracker");
			var collection = db.GetCollection<Workout>("workouts");

			//return collection.Find(w => true, new FindOptions { o}
		}

		public Workout AddUpdateWorkout(IWorkout workout)
		{
			throw new NotImplementedException();
		}
	}
}