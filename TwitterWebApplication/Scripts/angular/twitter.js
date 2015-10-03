﻿(function () {
    'use strict';

    configuration.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('TwitterBackup', ['ngResource', 'ngRoute'])
        .config(configuration)     

        .directive('myMaxlength', function() {
            return {
                require: 'ngModel',
                link: function (scope, element, attrs, ngModelCtrl) {
                    var maxlength = Number(attrs.myMaxlength);
                    function fromUser(text) {
                        if (text.length > maxlength) {
                            var transformedInput = text.substring(0, maxlength);
                            ngModelCtrl.$setViewValue(transformedInput);
                            ngModelCtrl.$render();
                            return transformedInput;
                        } 
                        return text;
                    }
                    ngModelCtrl.$parsers.push(fromUser);
                }
            }; 
        })

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
            $scope.formControlStyle = '';
            $scope.savingButtonMessage = 'Save';

            // Save message
            $scope.createClientMessage = function () {
                $scope.savingButtonMessage = 'Saving';
                $scope.clientMessage.$save(function (responseMessage) {
                    $scope.formControlStyle = 'has-success';
                    $scope.clientMessage = new ClientMessage({});
                    $scope.savingButtonMessage = 'Save';

                    $scope.clientMessages.unshift(responseMessage);

                }, function (error) {
                    $scope.formControlStyle = 'has-error';
                    $scope.savingButtonMessage = 'Save';
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