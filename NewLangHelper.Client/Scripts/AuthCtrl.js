'use strict';

app.controller('AuthController', ['$scope', '$http', function ($scope, $http) {
    $scope.login = function () {
        $http.post(ServerUrl + '/login',
                {
                    username: $scope.loginModel.username,
                    password: $scope.loginModel.password
                })
            .then(function success(response) {
                    //TODO
                },
                function error(response) {
                    //TODO
                });

    };
}]);