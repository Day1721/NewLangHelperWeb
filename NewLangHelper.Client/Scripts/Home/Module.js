﻿(function () {
    'use strict';

    const app = angular.module('home', ['config']);

    app.config(config);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when('/home', {
                templateUrl: 'Views/Home/Home.html',
                controller: 'HomeCtrl'
            })
            .when('/add/card/', {
                templateUrl: 'Views/Home/AddWord.html',
                controller: 'AddWordCtrl'
            })
            .when('/add/group/', {
                templateUrl: 'Views/Home/AddGroup.html',
                controller: 'AddGroupCtrl'
            })
            .when('/share', {
                templateUrl: 'Views/Home/Share.html',
                controller: 'ShareCtrl'
            });
    }
})();