'use strict';

app.controller('HomeController', ['$scope', '$http', '$cookies',
    function ($scope, $http, $cookies) {
        var token = $cookies.get('token');
        if (!token)
            window.location.assign('/#/auth');
        console.log(token);
        $http({
            method: 'GET',
            url: serverUrl + '/groups',
            headers: {
                'Authentication': 'Token ' + token
            }
        }).then(function success(response) {
            $scope.groups = response.data;
        },
        function error(response) {
            window.location.assign('/#/auth');
        });
}]);