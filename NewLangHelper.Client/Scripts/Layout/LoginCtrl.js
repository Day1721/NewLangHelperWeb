(function () {
    'use strict';

    angular
        .module('layout')
        .controller('LoginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$rootScope', '$http', '$location', '$cookies', 'serverUrl'];

    function loginCtrl($scope, $rootScope, $http, $location, $cookies, serverUrl) {
        /*if ($cookies.get('token') !== '' && $rootScope.isLogged) {
            //TRY to go home, if redirect back, isLogged will be false
            $location.path('/home');
            $location.replace();
        }*/

        $rootScope.isLogged = false;
        $rootScope.username = '';
        $cookies.remove('token');
        $cookies.remove('username');

        $scope.title = 'LoginCtrl';

        $scope.login = function() {
            $http({
                method: 'POST',
                url: `${serverUrl}/rest-auth/login/`,
                data: {
                    'username': $scope.username,
                    'password': $scope.password
                }
            }).then(
                function success(response) {
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
