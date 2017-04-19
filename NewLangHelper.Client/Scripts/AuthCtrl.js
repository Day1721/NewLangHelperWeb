'use strict';

app.controller('AuthController', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
    $scope.login = function() {
        $http.post(ServerUrl + '/login',
                {
                    username: $scope.loginModel.username,
                    password: $scope.loginModel.password
                })
            .then(function success(response) {
                    //TODO
                    //maybe $cookies.put('token', response);
                },
                function error(response) {
                    //TODO
                });

    };

    $scope.register = function () {
        //TODO check if password == password2

        $http.post(ServerUrl + '/register',
                {
                    username: $scope.registerModel.username,
                    email: $scope.registerModel.email,
                    password: $scope.registerModel.password
                })
            .then(function success(response) {
                    //TODO
                },
                function error(response) {
                    //TODO
                });
    };
}]);