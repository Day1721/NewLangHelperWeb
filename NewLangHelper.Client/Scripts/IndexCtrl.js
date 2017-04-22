'use strict';

app.controller('IndexController', ['$scope', '$http', '$cookies', '$window',
    function ($scope, $http, $cookies, $window) {
        $scope.AppTitle = 'New Lang Helper';
    
        $scope.username = '';
        $scope.isLogged = false;

        IndexAuth($scope, $http, $cookies, $window);
    }]);