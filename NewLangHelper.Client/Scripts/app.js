'use strict';

var app = angular.module('langHelpApp', ['ngRoute']);

app.controller('IndexController', ['$scope', function ($scope) {
    $scope.AppTitle = 'New Lang Helper';

    $scope.Login = '';
}]);
app.directive('loginDir', function () {
    return {
        templateUrl: 'Templates/LoginNavBarHelper.html'
    }
});

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider){
    $locationProvider.hashPrefix('');

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