using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi.Services
{
	public class MongoWorkoutDataService : IWorkoutDataService
	{
		private readonly MongoClient _mongoClient;

		public MongoWorkoutDataService()
		{
			var settings = new MongoClientSettings
			{
				Server =
					new MongoServerAddress(ConfigurationManager.AppSettings["MongoServer"],
						int.Parse(ConfigurationManager.AppSettings["MongoPort"])),
				UseSsl = true,
				SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12}
			};

			var identity = new MongoInternalIdentity("workouttracker", ConfigurationManager.AppSettings["MongoUser"]);
			var evidence = new PasswordEvidence(ConfigurationManager.AppSettings["MongoPassword"]);

			settings.Credentials = new List<MongoCredential>
			{
				new MongoCredential("SCRAM-SHA-1", identity, evidence)
			};

			_mongoClient = new MongoClient(settings);
			WorkoutCollection = _mongoClient.GetDatabase("workouttracker").GetCollection<Workout>("workouts");

			if (BsonClassMap.IsClassMapRegistered(typeof(Workout)))
				return;
			BsonClassMap.RegisterClassMap<Workout>();
			BsonClassMap.RegisterClassMap<Activity>();
			BsonClassMap.RegisterClassMap<Set>();
		}

		private IMongoCollection<Workout> WorkoutCollection;

		public List<Workout> GetLatest(int count)
		{			
			var workouts = WorkoutCollection.Find(w => true)
				.SortByDescending(w => w.Date)
				.Limit(count);
			return workouts.ToList();
		}

		public Workout AddWorkout(Workout workout)
		{
			workout.Id = ObjectId.GenerateNewId();
			WorkoutCollection.InsertOne(workout);	
			return workout;
		}

		public void RemoveWorkout(ObjectId workoutId)
		{
			WorkoutCollection.DeleteOne(w => w.Id == workoutId);
		}

		public void RemoveAll(Func<Workout, bool> filter)
		{
			WorkoutCollection.DeleteMany(w => w.User.Contains("_test"));
		}

		public Workout UpdateWorkout(Workout workout)
		{
			WorkoutCollection.ReplaceOne(w => w.Id == workout.Id, workout, new UpdateOptions { IsUpsert = true });
			return workout;
		}
	}
}