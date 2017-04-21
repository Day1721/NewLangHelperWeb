'use strict';

var serverUrl = "http://localhost:8001";

var app = angular.module('langHelpApp', ['ngRoute', 'ngCookies']);

app.directive('loginDir', function () {
    return {
        templateUrl: 'Templates/LoginNavBarHelper.html'
    };
});

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider){
    $locationProvider.hashPrefix('');
    /*$locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });*/

    $routeProvider
        .when('/', {
            templateUrl: 'Templates/Index.html'
        })
        .when('/about', {
            templateUrl: 'Templates/About.html'
        })/*
        .when('/contact', {
            //TODO
        })*/
        .when('/auth', {
            templateUrl: 'Templates/Auth.html',
            controller: 'AuthController'
        })
        .otherwise({
            templateUrl: 'Templates/NotFound.html'
        });
}]);