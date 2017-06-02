(function () {
    'use strict';

    angular
        .module('home')
        .controller('HomeCtrl', homeCtrl);

    homeCtrl.$inject =
        ['$scope', '$http', '$location', 'localStorageService', 'serverUrl', '$route'];

    function homeCtrl($scope, $http, $location, localStorage, serverUrl, $route) {
        if (!$scope.isLogged) {
            $location.path('/login');
            return;
        }

        $scope.title = 'HomeCtrl';
        $scope.partLoading = [];
        $scope.wordListShow = [];

        $scope.showGroupInfo =
            id => $scope.wordListShow[id] = !$scope.wordListShow[id];

        const data = localStorage.get('data') || [];

        $scope.data = groupby(data);
        $scope.dataLength = Object.keys($scope.data).length || 0;

        $scope.isLoading = true;

        $scope.deleteGroup = id => {
            $http({
                method: 'DELETE',
                url: `${serverUrl}/groups/${id}/`
            }).then(
                successResponce => {
                    alert('Group deleted successfully.');
                    $route.reload();
                }, console.log);
        };

        $scope.deleteCard = (groupId, id) => {
            $http({
                method: 'DELETE',
                url: `${serverUrl}/groups/${groupId}/word/${id}/`
            }).then(
                sucessResponce => {
                    alert('Card deleted successfully.');
                    $route.reload();
                },
                console.log);
        };

        $http({
            method: 'GET',
            url: `${serverUrl}/groups/`  //maybe TODO
        }).then(
            successResponce => {
                successResponce.data.forEach(elem => {
                    elem.id = elem.pk;
                });

                localStorage.set('data', successResponce.data);

                $scope.data = groupby(successResponce.data);
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
