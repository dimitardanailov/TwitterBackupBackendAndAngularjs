﻿(function () {
    'use strict';

    configuration.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('TwitterBackup', ['ngResource', 'ngRoute'])
        .config(configuration)

        .directive('myMaxlength', function () {
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

        // Source and Idea: http://blog.revolunet.com/blog/2014/02/14/angularjs-services-inheritance/
        .factory('BaseFactoryMessage', ['$resource', '$rootScope', function ($resource, $rootScope) {
            var messages = []; // Pointer to $scope.clientMessages
            var formControlStyle = '';
            var buttonTextMessage = '';
            var dbReference = '';
            var message = null;

            var BaseFactoryMessage = function (URI, messages, formControlStyle, buttonTextMessage) {
                this.messages = messages;
                this.formControlStyle = formControlStyle;
                this.buttonTextMessage = buttonTextMessage;
                this.dbReference = $resource(URI, { id: '@id.clean' });
                this.message = new this.dbReference({});
            };

            BaseFactoryMessage.prototype.setMessages = function (messages) {
                this.messages = messages;
            };

            BaseFactoryMessage.prototype.saveRecordToDabase = function () {
                var defaultMessage = this.buttonTextMessage;
                this.buttonTextMessage = 'Saving';
                var _this = this;

                this.message.$save(function (responseMessage) {
                    _this.formControlStyle = 'has-success';
                    _this.message = new _this.dbReference({});
                    _this.buttonTextMessage = defaultMessage;

                    console.log(responseMessage);

                    if (_this.messages instanceof Array) {
                        _this.messages.unshift(responseMessage);
                    }
                }, function (error) {
                    _this.formControlStyle = 'has-error';
                    _this.buttonTextMessage = defaultMessage;
                });
            };

            return BaseFactoryMessage;
        }])

        .factory('ClientMessage', ['BaseFactoryMessage', function (BaseFactoryMessage) {
            // create our new custom object that reuse the original object constructor
            var ClientMessage = function () {
                BaseFactoryMessage.apply(this, arguments);
            };
            
            // reuse the original object prototype
            BaseFactoryMessage.prototype = new BaseFactoryMessage();

            ClientMessage.prototype.save = function () {
                BaseFactoryMessage.prototype.saveRecordToDabase.call(this);
            };

            ClientMessage.prototype.setMessages = function (messages) {
                BaseFactoryMessage.prototype.setMessages.call(this, messages);
            };

            return ClientMessage;
        }])

        .factory('MongoDbMessage', ['BaseFactoryMessage', function (BaseFactoryMessage) {
            // create our new custom object that reuse the original object constructor
            var MongoDbMessage = function () {
                BaseFactoryMessage.apply(this, arguments);
            };

            // reuse the original object prototype
            BaseFactoryMessage.prototype = new BaseFactoryMessage();

            MongoDbMessage.prototype.save = function () {
                BaseFactoryMessage.prototype.saveRecordToDabase.call(this);
            };

            MongoDbMessage.prototype.setMessages = function (messages) {
                BaseFactoryMessage.prototype.setMessages.call(this, messages);
            };

            return MongoDbMessage;
        }])

        /**
         * @ngdoc overview
         * @name TwitterBackup:controller:TwitterBackupCtrl
         * @description
         * 
         * Main controller of the application.
         */
        .controller('TwitterBackupHomePageCtrl', ['$scope', '$http', '$resource', 'ClientMessage', 'MongoDbMessage',
            function ($scope, $http, $resource, ClientMessage, MongoDbMessage) {
                
                $scope.clientMessages = [];

                // Create an empty MongoDbMessage object
                $scope.mongoDbMessage = new MongoDbMessage('/api/MongoDbMessage/:id', $scope.clientMessages, '', 'Save to MongoDB');

                // Create an empty ClientMessage object
                $scope.clientMessage = new ClientMessage('/api/ClientMessage/:id', $scope.clientMessages, '', 'Save to MSSQL');

                $http.get("/api/ClientMessage").success(function (data, status, headers, config) {
                    $scope.clientMessages = data;

                    // Update reference.
                    $scope.mongoDbMessage.setMessages($scope.clientMessages);
                    $scope.clientMessage.setMessages($scope.clientMessages);

                }).error(function (data, status, headers, config) {
                    alert("Please try again later.");
                });

                // Post message on twitter
                $scope.postMessageOnTwitter = function (clientMessage) {

                    // Capitalize first letter
                    clientMessage = Object.withCapitalizeKeys(clientMessage);

                    $http.post("/api/Tweet", clientMessage).success(function (data, status, headers, config) {
                        if (data.hasOwnProperty('id')) {
                            alert('Your twitter messages is: ' + data.id);
                        }
                    }).error(function (error, status, headers, config) {
                        var errorMessage = 'Please try again later. If you problem is still able, please re-sign up.';

                        if (error.hasOwnProperty('message')) {
                            errorMessage = error.message;
                        }

                        alert(errorMessage);
                    });
                };

            }]) // END TwitterBackupHomePageCtrl

            /**
             * @ngdoc overview
             * @name TwitterBackup:controller:TwitterBackupCtrl
             * @description
             * 
             * Main controller of the application.
             */
            .controller('TwitterWallCtrl', ['$scope', '$http', function ($scope, $http) {

                $scope.tweets = [];

                $http.get("/api/TwitterWall/GetTweets").success(function (data, status, headers, config) {
                    $scope.tweets = data;

                    console.log(data);

                }).error(function (data, status, headers, config) {
                    alert("Please try again later. If you problem is still able, please re-sign up.");
                });
                
            }]);
    
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

            // View your twitter newsletters
            .when('/angular/twitterwall', {
                controller: 'TwitterWallCtrl',
                templateUrl: '/AngularTemplates/twitter-wall.html'
            })

            // View a client message.
            .when('/ClientMessage/:clientmessage_id', {
                controller: 'TwitterBackupClientMessageCtrl',
                templateUrl: '/AngularTemplates/client-messages-details.html'
            })

            // Load information for home page of twitter
            .when('/angular/homepage', {
                controller: 'TwitterBackupHomePageCtrl',
                templateUrl: '/AngularTemplates/home.html'
            });

        // use the HTML5 History API
        $locationProvider.html5Mode(true);
    };

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
    };

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    };
})();
