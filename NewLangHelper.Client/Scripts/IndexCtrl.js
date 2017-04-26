'use strict';

app.controller('IndexController', ['$scope', '$http', '$cookies',
    function ($scope, $http, $cookies) {
        $scope.AppTitle = 'New Lang Helper';
    
        $scope.username = '';
        $scope.isLogged = false;

        IndexAuth($scope, $http, $cookies);
    }]);