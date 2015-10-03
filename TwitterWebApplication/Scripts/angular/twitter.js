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
        .controller('TwitterBackupHomePageCtrl', ['$scope', '$http', 'ClientMessage', 
            function ($scope, $resource, ClientMessage) {
            // Create a empty ClientMessage object
            $scope.clientMessage = new ClientMessage({});
            $scope.errorMessage = '';

            // Save message
            $scope.save = function () {
                $scope.clientMessage.$save(function (responseMessage) {
                    $scope.errorMessage = '';
                    $scope.clientMessage = new ClientMessage({});

                    console.log(responseMessage);

                }, function (error) {
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