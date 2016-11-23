using MongoDB.Bson;
using Moq;
using WorkoutTrackerApi.Controllers;
using WorkoutTrackerCore;
using Xunit;

namespace WorkoutTrackerApiTests
{
	public class WorkoutControllerTests
	{
		[Fact]
		public void ShouldTellServiceToGetLatestWorkoutsWhen_GetLatest()
		{
			var mockDataService = new Mock<IWorkoutDataService>();
			mockDataService.Setup(s => s.GetLatest(It.IsAny<int>()));
			var controller = new WorkoutController(mockDataService.Object);

			var result = controller.GetLatest(2);

			mockDataService.Verify(s => s.GetLatest(2), Times.Once());
		}

		[Fact]
		public void ShouldTellServiceToAddWorkoutWhen_AddWorkout()
		{
			var mockDataService = new Mock<IWorkoutDataService>();
			var workout = new Workout();
			mockDataService.Setup(s => s.AddWorkout(workout));
			var controller = new WorkoutController(mockDataService.Object);

			var result = controller.AddWorkout(workout);

			mockDataService.Verify(s => s.AddWorkout(workout), Times.Once());
		}

		[Fact]
		public void ShouldTellServiceToRemoveWorkoutWhen_RemoveWorkout()
		{
			var mockDataService = new Mock<IWorkoutDataService>();
			var id = ObjectId.GenerateNewId().ToString();
			mockDataService.Setup(s => s.RemoveWorkout(id));
			var controller = new WorkoutController(mockDataService.Object);

			controller.RemoveWorkout(id.ToString());

			mockDataService.Verify(s => s.RemoveWorkout(It.IsAny<string>()), Times.Once());
		}

		[Fact]
		public void ShouldTellServiceToGetWorkoutWhen_GetWorkout()
		{
			var mockDataService = new Mock<IWorkoutDataService>();
			var id = ObjectId.GenerateNewId().ToString();
			mockDataService.Setup(s => s.RemoveWorkout(id));
			var controller = new WorkoutController(mockDataService.Object);

			controller.GetWorkout(id.ToString());

			mockDataService.Verify(s => s.GetWorkout(It.IsAny<string>()), Times.Once());
		}

		[Fact]
		public void ShouldTellServiceToUpdateWorkoutWhen_UpdateWorkout()
		{
			var mockDataService = new Mock<IWorkoutDataService>();
			var workout = new Workout
			{
				Id = ObjectId.GenerateNewId().ToString()
			};
			mockDataService.Setup(s => s.UpdateWorkout(workout));
			var controller = new WorkoutController(mockDataService.Object);

			controller.UpdateWorkout(workout);

			mockDataService.Verify(s => s.UpdateWorkout(workout), Times.Once());
		}
	}
}