(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$rootScope', '$http', '$location', '$cookies'];

    function homeCtrl($scope, $rootScope, $http, $location, $cookies) {
        $scope.title = 'HomeCtrl';

        const token = $cookies.get('token');
        $http({
            method: 'GET',
            url: `${$rootScope.serverUrl}/groups/`, //maybe TODO
            headers: {
                Authorization: token
            }
        }).then(
            function success(response) {

            },
            function error(response) {

            });
    }
})();
