using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using WorkoutTrackerApi.Services;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IWorkoutDataService, MongoWorkoutDataService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}