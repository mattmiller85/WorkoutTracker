using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Bson;
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
		}

		public List<Workout> GetLatest(int count)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			var workouts = collection.Find(new BsonDocumentFilterDefinition<Workout>(new BsonDocument()))
				.SortByDescending(w => w.Date)
				.Limit(count);
			return workouts.ToList();
		}

		public Workout AddUpdateWorkout(Workout workout)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			var filter = Builders<Workout>.Filter.Eq("_id", workout.Id);
			var result = collection.UpdateOne(filter, new ObjectUpdateDefinition<Workout>(workout),
				new UpdateOptions {IsUpsert = true});
			workout.Id = result.UpsertedId.AsString;
			return workout;
		}

		public void RemoveWorkout(string workoutId)
		{
			var db = GetMongoDatabase();
			var collection = db.GetCollection<Workout>("workouts");
			var filter = Builders<Workout>.Filter.Eq("_id", workoutId);
			collection.DeleteOne(filter);
		}

		private IMongoDatabase GetMongoDatabase()
		{
			return _mongoClient.GetDatabase("workouttracker");
		}
	}
}