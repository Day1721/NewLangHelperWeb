(function () {
    'use strict';

    angular
        .module('layout')
        .controller('LogoutCtrl', logoutCtrl);

    logoutCtrl.$inject = ['$scope', '$rootScope', '$cookies', '$location'];

    function logoutCtrl($scope, $rootScope, $cookies, $location) {
        /*$scope.title = 'LogoutCtrl';*/

        $rootScope.isLogged = false;
        $rootScope.username = '';

        $cookies.remove('token');
        $cookies.remove('username');
        $cookies.remove('sessionid');

        $location.path('/');
        $location.replace();
    }
})();
