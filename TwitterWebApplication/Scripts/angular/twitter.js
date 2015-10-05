(function () {
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

            // Post message on twitter
            $scope.postMessageOnTwitter = function (clientMessage) {

                // Capitalize first letter
                clientMessage = Object.withCapitalizeKeys(clientMessage);

                $http.post("/api/Tweet",  clientMessage).success(function (data, status, headers, config) {
                    
                }).error(function (error, status, headers, config) {
                    var errorMessage = "Please try again later.";

                    if (error.hasOwnProperty('message')) {
                        errorMessage = error.message;
                    }

                    alert(errorMessage);
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
            // View a client message.
            .when('/ClientMessage/:clientmessage_id', {
                controller: 'TwitterBackupClientMessageCtrl',
                templateUrl: '/AngularTemplates/client-messages-details.html'
            })

            // Load information for home page of twitter
            .when('/Home/TwitterBackupHomePage', {
                controller: 'TwitterBackupHomePageCtrl',
                templateUrl: '/AngularTemplates/home.html'
            });

        // use the HTML5 History API
        $locationProvider.html5Mode(true);
    }

    /**
     * Uppercase keys object keys.
     */
    Object.withCapitalizeKeys = function withCapitalizeKeys(object) {
        // this solution ignores inherited properties
        var newObject = {}, capitalizeKey = null;
        for (var iterator in object) {
            if (typeof iterator === 'string') {
                capitalizeKey = capitalizeFirstLetter(iterator);
                newObject[capitalizeKey] = object[iterator];
            }
        }

        return newObject;
    }

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

})();