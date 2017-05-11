﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$rootScope', '$http', '$location', '$cookies'];

    function loginCtrl($scope, $rootScope, $http, $location, $cookies) {
        if ($cookies.get('token') !== '' && $rootScope.isLogged) {
            //TRY to login twice
            $location.path('/home');
            $location.replace();
        }

        $rootScope.isLogged = false;
        $rootScope.username = '';

        $scope.title = 'LoginCtrl';

        $scope.login = function() {
            $http({
                method: 'POST',
                url: `${$rootScope.serverUrl}/rest-auth/login/`,
                data: {
                    'username': $scope.username,
                    'password': $scope.password
                }
            }).then(
                function success(response) {
                    alert(`OK, token = ${response.data.key}`);
                    $rootScope.token = response.data.key;
                    $cookies.put('token', $rootScope.token);
                    $cookies.put('username', $scope.username); 
                    $rootScope.username = $scope.username;
                    $rootScope.isLogged = true;

                    $location.path('/home');
                    $location.replace();
                },
                function error(response) {
                    alert(`ERROR, code = ${response.status}`);
                    console.log(response.headers());
                });
        };
    }
})();
