var workoutTrackerApp = angular.module('workoutTrackerApp', ['ui.bootstrap', 'ngRoute']);

workoutTrackerApp.config(function($routeProvider) {
	$routeProvider

		// route for the home page
		.when('/', {
			templateUrl : 'pages/home.html',
			controller  : 'mainController'
		})

		// route for the about page
		.when('/addworkout', {
			templateUrl : 'pages/addworkout.html',
			controller  : 'addWorkoutController'
		})
});

// create the controller and inject Angular's $scope
workoutTrackerApp.controller('mainController', function ($scope) {

	$scope.noWorkoutsMessage = 'There are no workouts, click "New Workout" to add one!';

	$scope.workouts = [];
});

workoutTrackerApp.controller('addWorkoutController', function ($scope) {

});