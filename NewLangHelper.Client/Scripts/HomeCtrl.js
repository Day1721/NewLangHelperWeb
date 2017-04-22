'use strict';

app.controller('HomeController', ['$scope', '$http', '$cookies', function($scope, $http, $cookies) {
    $http({
        method: 'GET',
        url: serverUrl + '/groups',
        headers: {
            'Authentication': 'Token ' + $cookies.get('token')
        }
    }).then(function success(response) {
        console.log(response);
        $scope.groups = response.data;
    },
    function error(response) {
        console.log(response);
    });
}]);