(function () {
    'use strict';

    angular
        .module('layout')
        .controller('RegisterCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', '$rootScope', '$http', '$location', '$cookies', 'serverUrl'];

    function registerCtrl($scope, $rootScope, $http, $location, $cookies, serverUrl) {
        $scope.title = 'RegisterCtrl';
        $scope.error = undefined;

        $scope.register = function () {
            $http({
                method: 'POST',
                url: `${serverUrl}/rest-auth/registration/`,
                data: {
                    'username': $scope.username,
                    'email': $scope.email,
                    'password1': $scope.password1,
                    'password2': $scope.password2
                }
            }).then(
                function success(response) {
                    //alert(`OK, token = ${response.data.key}`); //DEBUGGING
                    alert('You was successfully registred.\n' +
                        'Do not forget to activate your account, look in your mail.');
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
                    if (response.status === 400) {
                        $scope.error = response.data;
                    }
                });
        };
    }
})();
