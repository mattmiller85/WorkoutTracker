using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace WorkoutTrackerCore
{
    public class Workout : IWorkout
    {
		public Workout()
		{
			Settings = new Dictionary<string, object>();
			Activities = new List<Activity>();
		}
        public string User { get; set; }
		public string Description { get; set; }
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
