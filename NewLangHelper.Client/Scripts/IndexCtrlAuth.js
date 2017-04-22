'use strict';

app.controller('IndexController', ['$scope', '$http', '$cookies', '$window',
    function ($scope, $http, $cookies, $window) {
        $scope.login = function () {
            console.log($scope);
            var username = $scope.loginModel.username;
            $http.post(serverUrl + '/rest-auth/login/ ', {
                        username: $scope.loginModel.username,
                        password: $scope.loginModel.password
                    })
                .then(function success(response) {
                        console.log(response.data.key);
                        $cookies.put('token', response.data.key);
                        $scope.username = username;
                        $scope.isLogged = true;
                        $window.location.url = '/';
                    },
                    function error(response) {
                        console.log(response);
                        alert(response);
                    });

        };

        $scope.register = function () {
            $http.post(serverUrl + '/rest-auth/registration/ ', {
                        username: $scope.registerModel.username,
                        email: $scope.registerModel.email,
                        password1: $scope.registerModel.password,
                        password2 : $scope.registerModel.password2

                    })
                .then(function success(response) {
                        console.log(response);
                        $cookies.put('token', response.data.key);
                        $scope.username = $scope.loginModel.username;
                    },
                    function error(response) {
                        console.log(response);
                        alert(response);
                    });
        };
}]);