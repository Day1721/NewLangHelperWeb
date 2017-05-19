(function () {
    'use strict';

    angular
        .module('home')
        .controller('HomeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$http', '$location', 'serverUrl'];

    function homeCtrl($scope, $http, $location, serverUrl) {
        $scope.title = 'HomeCtrl';
        $scope.partLoading = [];
        $scope.wordListShow = [];

        $scope.showGroupInfo =
            id => $scope.wordListShow[id] = !$scope.wordListShow[id];

        $scope.isLoading = true;
        $http({
            method: 'GET',
            url: `${serverUrl}/groups/`  //maybe TODO
        }).then(
            function success(response) {
                response.data.map((elem) => {
                    elem.id = elem.url.split('/').slice(-1)[0];
                    $scope.partLoading[elem.id] = true;
                    $http({
                        method: 'GET',
                        url: elem.url
                    }).then(
                        successResponse => {
                            elem.words = successResponse.data.words;
                            $scope.partLoading[elem.id] = false;
                        }, toLoginIf403);
                }, []);

                function groupby(list) {
                    return list.reduce((prev, elem) => {
                        const fst = elem.first_language;
                        const snd = elem.second_language;
                        const key = `${fst}|${snd}`;
                        (prev[key] = prev[key] || []).push(elem);
                        return prev;
                    }, {});
                }

                $scope.data = groupby(response.data);
                $scope.dataLength = Object.keys($scope.data).length || 0;

                $scope.isLoading = false;
            }, toLoginIf403);

        function toLoginIf403(response) {
            if (response.status === 403) {
                $location.path('/login');
                $location.replace();
            }
        }
    }
})();
