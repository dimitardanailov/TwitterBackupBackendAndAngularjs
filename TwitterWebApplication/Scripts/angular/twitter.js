(function () {
    'use strict';

    configuration.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('TwitterBackup', ['ngResource', 'ngRoute'])
        .config(configuration)     

        .factory('ClientMessage', function($resource) {
            var message = $resource('/api/ClientMessage/:id', { id: '@id.clean' });
            
            return message;
        })   
        /**
         * @ngdoc overview
         * @name TwitterBackup:controller:TwitterBackupCtrl
         * @description
         * 
         * Main controller of the application.
         */
        .controller('TwitterBackupHomePageCtrl', ['$scope', '$http', '$resource', 'ClientMessage',
            function ($scope, $http, $resource, ClientMessage) {
            
            $scope.clientMessages = [];

            $http.get("/api/ClientMessage").success(function (data, status, headers, config) {
                $scope.clientMessages = data;
            }).error(function (data, status, headers, config) {
                alert("Please try again later.");
            });

            // Create a empty ClientMessage object
            $scope.clientMessage = new ClientMessage({});

            // Page elements and settings
            $scope.errorMessage = '';
            $scope.savingButtonMessage = 'Save!';

            // Save message
            $scope.save = function () {
                $scope.savingButtonMessage = 'Saving';
                $scope.clientMessage.$save(function (responseMessage) {
                    $scope.errorMessage = '';
                    $scope.clientMessage = new ClientMessage({});
                    $scope.savingButtonMessage = 'Save!';

                    console.log(responseMessage);

                }, function (error) {
                    $scope.savingButtonMessage = 'Save!';
                    if (error.hasOwnProperty('data') && error.data.hasOwnProperty('message')) {
                        $scope.errorMessage = error.data.message;
                    }
                });
            };
        }]); // END TwitterBackupHomePageCtrl

    /**
    * @ngdoc overview
    * @name Angularjs::routes configuration
    * @description
    * @param $routeProvider Used for configuring routes.
    * @param $locationProvider Use the $locationProvider to configure how the
    */
    function configuration($routeProvider, $locationProvider) {
        // Configure the routes
        $routeProvider
            // Create a new tweet post
            .when('api/tweet', {
                controller: 'TwitterBackupAddTwitterMessageCtrl',
                templateUrl: '../AngularTemplates/addTwitterMessage.html'
            })

            // Load information for home page of twitter
            .when('/Home/TwitterBackupHomePage', {
                controller: 'TwitterBackupHomePageCtrl',
                templateUrl: '/AngularTemplates/home.html'
            });

        // use the HTML5 History API
        $locationProvider.html5Mode(true);
    }

})();