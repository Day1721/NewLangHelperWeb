(function () {
    'use strict';

    angular
        .module('home')
        .controller('HomeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$http', '$location', 'localStorageService', 'serverUrl'];

    function homeCtrl($scope, $http, $location, localStorage, serverUrl) {
        if (!$scope.isLogged) {
            $location.path('/login');
            return;
        }

        $scope.title = 'HomeCtrl';
        $scope.partLoading = [];
        $scope.wordListShow = [];

        $scope.showGroupInfo =
            id => $scope.wordListShow[id] = !$scope.wordListShow[id];

        let data = localStorage.get('data') || [];

        $scope.data = groupby(data);

        $scope.isLoading = true;


        $http({
            method: 'GET',
            url: `${serverUrl}/groups/`  //maybe TODO
        }).then(
            function success(response) {
                response.data.forEach(elem => {
                    elem.id = elem.pk;
                });

                localStorage.set('data', response.data);

                $scope.data = groupby(response.data);
                $scope.dataLength = Object.keys($scope.data).length || 0;

                $scope.isLoading = false;
                console.log(data);
            }, toLoginIf403);

        function toLoginIf403(response) {
            if (response.status === 403) {
                $location.path('/login');
                $location.replace();
            }
        }

        function groupby(list) {
            return list.reduce((prev, elem) => {
                const fst = elem.firstLanguage;
                const snd = elem.secondLanguage;
                const key = `${fst}|${snd}`;
                (prev[key] = prev[key] || []).push(elem);
                return prev;
            }, {});
        }
    }
})();
