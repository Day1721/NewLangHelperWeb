(function () {
    'use strict';

    angular
        .module('layout')
        .controller('RegisterCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', '$rootScope', 'http', '$location', '$cookies'];

    function registerCtrl($scope, $rootScope, http, $location, $cookies) {
        $scope.title = 'RegisterCtrl';
        $scope.error = undefined;

        $scope.register = function () {
            http.post('/rest-auth/registration/', {
                    'username': $scope.username,
                    'email': $scope.email,
                    'password1': $scope.password1,
                    'password2': $scope.password2
            }).then(
                successResponse => {
                    alert('You was successfully registred.\n' +
                        'Do not forget to activate your account, look in your mail.');
                    $rootScope.token = successResponse.data.key;
                    $cookies.put('token', $rootScope.token);
                    $cookies.put('username', $scope.username);
                    $rootScope.username = $scope.username;
                    $rootScope.isLogged = true;

                    $location.path('/home');
                    $location.replace();
                },
                function error(response) {
                    if (response.status === 400) {
                        $scope.error = response.data;
                    }
                });
        };
    }
})();
