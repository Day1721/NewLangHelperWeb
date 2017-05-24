(function () {
    'use strict';

    const app = angular.module('layout', ['config']);

    app.config(config);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'Views/Layout/Index.html',
                controller: 'IndexCtrl'
            })
            .when('/contact', {
                templateUrl: 'Views/Contact.html',
                controller: 'IndexCtrl'
            })
            .when('/login', {
                templateUrl: 'Views/Layout/Login.html',
                controller: 'LoginCtrl'
            })
            .when('/register', {
                templateUrl: 'Views/Layout/Register.html',
                controller: 'RegisterCtrl'
            })
            .when('/logout', {
                templateUrl: 'Views/Layout/Index.html',
                controller: 'LogoutCtrl'
            })
            .otherwise({
                templateUrl: 'Views/Layout/NotFound.html',
                controller: 'IndexCtrl'
            });
    }
})();