using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using WorkoutTrackerApi.Services;
using WorkoutTrackerCore;

namespace WorkoutTrackerApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			config.EnableCors();
			// Web API configuration and services
			UnityConfig.RegisterComponents();

			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

	public class DependencyResolver : IDependencyResolver
	{
		public IDependencyScope BeginScope()
		{
			return null;
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(IWorkoutDataService))
				return new MongoWorkoutDataService();
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (serviceType == typeof(IWorkoutDataService))
				return new List<object>
			{
				new MongoWorkoutDataService()
			};
			return new List<object>();
		}
	}
}
