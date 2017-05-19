(function () {
    'use strict';

    const app = angular.module('layout', [
        'ngRoute', 'ngCookies',     // Angular modules 
        'config'                    // Custom modules 
    ]);

    app.config(config);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'Views/Index.html',
                controller: 'IndexCtrl'
            })
            .when('/contact', {
                templateUrl: 'Views/Contact.html',
                controller: 'IndexCtrl'
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
                templateUrl: 'Views/Index.html',
                controller: 'LogoutCtrl'
            })
            .otherwise({
                templateUrl: 'Views/NotFound.html',
                controller: 'IndexCtrl'
            });
    }
})();