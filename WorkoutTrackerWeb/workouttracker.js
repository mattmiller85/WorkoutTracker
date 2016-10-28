var workoutTrackerApp = angular.module('workoutTrackerApp', ['ui.bootstrap']);

// create the controller and inject Angular's $scope
workoutTrackerApp.controller('workoutController', function ($scope) {

	$scope.noWorkoutsMessage = 'There are no workouts, click "New Workout" to add one!';

	$scope.workouts = [];
});