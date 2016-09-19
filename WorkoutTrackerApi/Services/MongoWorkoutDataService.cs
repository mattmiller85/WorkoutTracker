using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Driver;
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
				Server = new MongoServerAddress(ConfigurationManager.AppSettings["MongoServer"], int.Parse(ConfigurationManager.AppSettings["MongoPort"])),
				UseSsl = true,
				SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 }
			};

			var identity = new MongoInternalIdentity("workouttracker", ConfigurationManager.AppSettings["MongoUser"]);
			var evidence = new PasswordEvidence(ConfigurationManager.AppSettings["MongoPassword"]);

			settings.Credentials = new List<MongoCredential>
			{
				new MongoCredential("SCRAM-SHA-1", identity, evidence)
			};

			_mongoClient = new MongoClient(settings);
		}
		public List<Workout> GetLatest(int count)
		{
			var db = _mongoClient.GetDatabase("workouttracker");
			var collection = db.GetCollection<Workout>("workouts");
			return new List<Workout>();
		}

		public Workout AddUpdateWorkout(IWorkout workout)
		{
			throw new NotImplementedException();
		}
	}
}