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
                function groupby(list) {
                    return list.reduce((prev, elem) => {
                        let fst = elem.first_language;
                        let snd = elem.second_language;
                        let key = `${fst}|${snd}`;
                        (prev[key] = prev[key] || []).push(elem);
                        return prev;
                    }, {});
                };

                $scope.data = groupby(response.data);
                $scope.dataLength = Object.keys($scope.data).length || 0;
            },
            function error(response) {
                $location.path('/login');
                $location.replace();
            });
    }
})();
