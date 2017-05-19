(function () {
    'use strict';

    const app = angular.module('home', ['ngRoute', 'config']);

    app.config(config);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider.when('/home', {
                templateUrl: 'Views/Home.html',
                controller: 'HomeCtrl'
            });
    }
})();