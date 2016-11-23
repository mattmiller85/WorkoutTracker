using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace WorkoutTrackerCore
{
	public interface IWorkout
	{
		string User { get; set; }
		string Description { get; set; }
		string Id { get; set; }
		DateTime Date { get; set; }
		Dictionary<string, object> Settings { get; set; }
		List<Activity> Activities { get; set; }
	}
}
