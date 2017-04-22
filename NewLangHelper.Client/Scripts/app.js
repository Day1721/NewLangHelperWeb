'use strict';

var serverUrl = 'http://localhost:8001';

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
        .when('/home', {
            templateUrl: 'Templates/Home.html',
            controller: 'HomeController'
        })
        .when('/about', {
            templateUrl: 'Templates/About.html'
        })
        .when('/contact', {
            templateUrl: 'Templates/Contact.html'
        })
        .when('/auth', {
            templateUrl: 'Templates/Auth.html',
            controller: 'IndexController'
        })
        .otherwise({
            templateUrl: 'Templates/NotFound.html'
        });
}]);