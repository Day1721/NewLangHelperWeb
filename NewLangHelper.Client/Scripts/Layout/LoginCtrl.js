(function () {
    'use strict';

    angular
        .module('layout')
        .controller('LoginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$rootScope', 'http', '$location', '$cookies'];

    function loginCtrl($scope, $rootScope, http, $location, $cookies) {
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
            http.post('/rest-auth/login/', {
                    'username': $scope.username,
                    'password': $scope.password
            }).then(
                successResponse => {
                    $rootScope.token = successResponse.data.key;
                    $cookies.put('token', $rootScope.token);
                    $cookies.put('username', $scope.username); 
                    $rootScope.username = $scope.username;
                    $rootScope.isLogged = true;

                    $location.path('/home');
                    $location.replace();
                },
                errorResponse => {
                    if (errorResponse.status === 400) {
                        alert('Incorrect login or password');
                    }
                    console.log(errorResponse);
                });
        };
    }
})();
