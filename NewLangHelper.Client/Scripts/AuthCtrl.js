'use strict';

app.controller('AuthController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
    $scope.login = function() {
        $http.post(serverUrl + '/rest-auth/login/ ',
                {
                    username: $scope.loginModel.username,
                    password: $scope.loginModel.password,
                    email: $scope.loginModel.email

                })
            .then(function success(response) {
                    console.log(response)
                    //maybe $cookies.put('token', response);
                },
                function error(response) {
                    console.log(response)
                    alert(response)
                });

    };

    $scope.register = function () {
        //TODO check if password == password2

        $http.post(serverUrl + '/rest-auth/registration/ ',
                {
                    username: $scope.registerModel.username,
                    email: $scope.registerModel.email,
                    password1: $scope.registerModel.password,
                    password2 : $scope.registerModel.password2

                })
            .then(function success(response) {
                    console.log(response)
                },
                function error(response) {
                    console.log(response)
                    alert(response)
                });
    };
}]);