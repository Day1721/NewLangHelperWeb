var IndexAuth = function ($scope, $http, $cookies) {
    $scope.login = function () {
        var username = $scope.loginModel.username;
        $http.post(serverUrl + '/rest-auth/login/ ', {
                username: $scope.loginModel.username,
                password: $scope.loginModel.password
            })
            .then(function success(response) {
                    $cookies.put('token', response.data.key);
                    $scope.username = username;
                    $scope.isLogged = true;
                    isLogged = true;
                    window.location.assign('/#/home');
                }, function error(response) {

                });

    };

    $scope.register = function () {
        $http.post(serverUrl + '/rest-auth/registration/ ', {
                username: $scope.registerModel.username,
                email: $scope.registerModel.email,
                password1: $scope.registerModel.password,
                password2: $scope.registerModel.password2

            })
            .then(function success(response) {
                    $cookies.put('token', response.data.key);
                    $scope.username = $scope.loginModel.username;
                },
                function error(response) {

                });
    };
};