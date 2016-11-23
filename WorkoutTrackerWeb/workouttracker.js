﻿var workoutTrackerApp = angular.module('workoutTrackerApp', ['ui.bootstrap', 'ngRoute']);
var apiPrefix = "http://localhost:63072//api/";

workoutTrackerApp.config(function ($routeProvider) {
	$routeProvider

	// route for the main page
		.when('/', {
		templateUrl: 'pages/home.html',
		controller: 'mainController'
	})

	// route for the add page
	.when('/addworkout', {
		templateUrl: 'pages/addworkout.html',
		controller: 'addWorkoutController'
	})

	.when('/editworkout/:workoutID', {
		templateUrl: 'pages/editworkout.html',
		controller: 'editWorkoutController'
	})

	.when('/editactivity', {
		templateUrl: 'pages/editactivity.html',
		controller: 'editActivityController'
	})

	.otherwise({
		redirectTo: '/'
	})
});

// create the controller and inject Angular's $scope
workoutTrackerApp.controller('mainController', function ($scope, $http) {
	var self = this;
	$scope.loadingWorkouts = true;
	$scope.noWorkoutsMessage = 'There are no workouts, click "New Workout" to add one!';
	$http({
		method: 'GET',
		url: apiPrefix + 'workouts/latest/10'
	}).then(function successCallback(response) {
		$scope.workouts = response.data;
		$scope.loadingWorkouts = false;
		$scope.noWorkouts = $scope.workouts.length === 0;
	}, function errorCallback(response) {
		$scope.workouts = [];
		$scope.hasError = true;
		$scope.errorMessage = response.statusText === "" ? "Error getting workouts." : response.statusText;
		$scope.loadingWorkouts = false;
	});
});

workoutTrackerApp.controller('addWorkoutController', function ($scope, $location, $http) {
	var self = this;
	$scope.workout = {
		"Date": new Date(),
		"Description": "",
		"User": "some_user_string"
	};
	$scope.addWorkout = function () {
		$http({
			method: 'POST',
			url: apiPrefix + 'workouts/new',
			headers: {
				'Content-Type': 'application/json',
				/*or whatever type is relevant */
				'Accept': 'application/json' /* ditto */
			},
			data: $scope.workout
		}).then(function successCallback(response) {
			$location.path("editworkout/" + response.data.Id)
		}, function errorCallback(response) {
			$scope.hasError = true;
			$scope.errorMessage = response.statusText === "" ? "Error adding workout." : response.statusText;
			$scope.loadingWorkouts = false;
		});
	};
});

workoutTrackerApp.controller('editWorkoutController', function ($scope, $routeParams, $http, $location) {
	var self = this;
	$scope.loadingWorkout = true;
	$scope.noActivities = true;
	$http({
		method: 'GET',
		url: apiPrefix + 'workout/' + $routeParams.workoutID
	}).then(function successCallback(response) {
		$scope.loadingWorkout = false;
		response.data.Date = new Date(response.data.Date);
		$scope.workout = response.data;
		$scope.noActivities = $scope.workout.Activities.length === 0;
		__currentWorkout = $scope.workout;
		if($scope.noActivities){
			$scope.workout.Activities.push({ "NameNew": "New Activity", "Name": "" });
		}
	}, function errorCallback(response) {
		$scope.hasError = true;
		$scope.errorMessage = response.statusText === "" ? "Error getting workout." : response.statusText;
		$scope.loadingWorkout = false;
		$scope.workout = null;
	});
	$scope.editActivity = function(activity){
		__currentActivity = activity;
		$location.path("editactivity");
	};
	$scope.saveWorkout = function(){
		$http({
			method: 'PUT',
			url: apiPrefix + 'workouts/update',
			headers: {
				'Content-Type': 'application/json',
				/*or whatever type is relevant */
				'Accept': 'application/json' /* ditto */
			},
			data: __currentWorkout
		}).then(function successCallback(response) {
			$location.path("/");
		}, function errorCallback(response) {
			$scope.hasError = true;
			$scope.errorMessage = response.statusText === "" ? "Error saving workout." : response.statusText;
		});
	};
	$scope.addActivity = function(){
		var activity = { "NameNew": "New Activity", "Name": "" };
		$scope.workout.Activities.push(activity);
		__currentActivity = activity;
		$location.path("editactivity");
	};
});

workoutTrackerApp.controller('editActivityController', function ($scope, $routeParams, $http, $location) {
	var self = this;
	if(__currentActivity.Sets == null)
		__currentActivity.Sets = [];
	if(__currentActivity.Sets.length === 0)
		__currentActivity.Sets.push({ "Reps": 10, "Weight": "", "showRemove": false });
	$scope.activity = __currentActivity;
	$scope.workoutID = __currentWorkout.Id;
	$scope.addSet = function(lastSet){
		lastSet.showAdd = false;
		$scope.activity.Sets.push({ "Reps": "", "Weight": "", "showRemove": true, "showAdd": true });
	};
	$scope.removeSet = function(set){
		$scope.activity.Sets.splice($scope.activity.Sets.indexOf(set), 1);
		$scope.activity.Sets[$scope.activity.Sets.length - 1].showAdd = true;
	};
	$scope.activity.Sets.forEach(function(set) {
		if(set.showRemove == undefined)
			set.showRemove = true;
	}, this);
	$scope.activity.Sets[$scope.activity.Sets.length - 1].showAdd = true;
	$scope.saveWorkout = function(){
		$http({
			method: 'PUT',
			url: apiPrefix + 'workouts/update',
			headers: {
				'Content-Type': 'application/json',
				/*or whatever type is relevant */
				'Accept': 'application/json' /* ditto */
			},
			data: __currentWorkout
		}).then(function successCallback(response) {
			$location.path("editworkout/" + __currentWorkout.Id)
		}, function errorCallback(response) {
			$scope.hasError = true;
			$scope.errorMessage = response.statusText === "" ? "Error saving workout." : response.statusText;
		});
	};
});