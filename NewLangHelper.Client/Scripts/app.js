(function() {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'ngCookies']);

    app.config(configFunc);
    app.run(runFunc);
//    app.service('http', httpService);

    runFunc.$inject = ['$rootScope', '$cookies'];

    function runFunc($rootScope, $cookies) {
        $rootScope.serverUrl = 'http://localhost:8001';
        $rootScope.jsonType = 'application/json';

        $rootScope.username = $cookies.get('username');
        $rootScope.isLogged = $rootScope.username != undefined; 
    }
    /*
    function httpService() {
        this
    }*/

    configFunc.$inject = ['$routeProvider', '$locationProvider', '$httpProvider'];

    function configFunc($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.hashPrefix('');

        $httpProvider.defaults.xsrfCookieName = 'csrftoken';
        $httpProvider.defaults.xsrfHeaderName = 'X-CSRFToken';
        $httpProvider.defaults.withCredentials = true;
        $httpProvider.interceptors.push(function ($cookies) {
            return {
                'request': function (config) {
                    config.headers['X-CSRFToken'] = $cookies.get('csrftoken');
                    return config;
                }
            };
        });

        $routeProvider
            .when('/', {
                templateUrl: 'Views/Index.html'
            })
            .when('/home', {
                templateUrl: 'Views/Home.html',
                controller: 'HomeCtrl'
            })
            .when('/contact', {
                templateUrl: 'Views/Contact.html'
            })
            .when('/login', {
                templateUrl: 'Views/Login.html',
                controller: 'LoginCtrl'
            })
            .when('/register', {
                templateUrl: 'Views/Register.html',
                controller: 'RegisterCtrl'
            })
            .when('/logout', {
                controller: 'LogoutCtrl'
            })
            .otherwise({
                templateUrl: 'Views/NotFound.html'
            });
    }


})();