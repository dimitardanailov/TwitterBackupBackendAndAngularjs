angular.module('TwitterBackup', [])
    /**
     * @ngdoc overview
     * @name TwitterBackup:controller:TwitterBackupCtrl
     * @description
     * 
     * Source and idea: https://github.com/angular-ui/ui-router/tree/master/sample
     * Main module of the application.
     */
    .controller('TwitterBackupFavouriteUsersCtrl', ['$scope', '$http'], function ($scope, $http) {
        $scope.title = "Loading favourites users ...";
        $scope.working = false;

        // List favourites users
        $http.get("/api/twitter").success(function (data, status, headers, config) {
        }).error(function (data, status, headers, config) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });

        // Add a new user to database
        $http.post('/api/trivia', { 'userId': option.questionId, 'optionId': option.id }).success(function (data, status, headers, config) {
            
        }).error(function (data, status, headers, config) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });
    });