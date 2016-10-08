using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkoutTrackerCore;
using MongoDB.Bson.Serialization;

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
				//UseSsl = true,
				//SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12}
			};

			//var identity = new MongoInternalIdentity("workouttracker", ConfigurationManager.AppSettings["MongoUser"]);
			//var evidence = new PasswordEvidence(ConfigurationManager.AppSettings["MongoPassword"]);

			//settings.Credentials = new List<MongoCredential>
			//{
			//	new MongoCredential("SCRAM-SHA-1", identity, evidence)
			//};

			_mongoClient = new MongoClient(settings);
			
			BsonClassMap.RegisterClassMap<Workout>();
			BsonClassMap.RegisterClassMap<Activity>();
			BsonClassMap.RegisterClassMap<Set>();
		}

		public List<Workout> GetLatest(int count)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			var workouts = collection.Find(w => true)
				.SortByDescending(w => w.Date)
				.Limit(count);
			return workouts.ToList();
		}

		public Workout AddWorkout(Workout workout)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			workout.Id = ObjectId.GenerateNewId();
			collection.InsertOne(workout);			
			return workout;
		}

		public void RemoveWorkout(ObjectId workoutId)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			collection.DeleteOne(w => w.Id == workoutId);
		}

		private IMongoDatabase GetMongoDatabase()
		{
			return _mongoClient.GetDatabase("workouttracker");
		}

		public Workout UpdateWorkout(Workout workout)
		{
			throw new NotImplementedException();
		}
	}
}