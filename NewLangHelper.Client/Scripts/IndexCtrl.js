'use strict';

app.controller('IndexController', ['$scope', function ($scope) {
    $scope.AppTitle = 'New Lang Helper';

    $scope.username = '';
    $scope.isLogged = false;
}]);