(function () {
    'use strict';

    angular
        .module('home')
        .controller('HomeCtrl', homeCtrl);

    homeCtrl.$inject =
        ['$scope', 'http', '$location', 'localStorageService', '$route'];

    function homeCtrl($scope, http, $location, localStorage, $route) {
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
            http.delete(`/groups/${id}/`).then(
                successResponce => {
                    alert('Group deleted successfully.');
                    $route.reload();
                },
                toLoginIf403
            );
        };

        $scope.deleteCard = (groupId, id) => {
            http.delete(`/groups/${groupId}/word/${id}/`).then(
                sucessResponce => {
                    alert('Card deleted successfully.');
                    $route.reload();
                },
                toLoginIf403
            );
        };

        http.get('/groups').then(
            successResponce => {
                successResponce.data.forEach(elem => {
                    elem.id = elem.pk;
                });

                localStorage.set('data', successResponce.data);

                $scope.data = groupby(successResponce.data);
                $scope.dataLength = Object.keys($scope.data).length || 0;

                $scope.isLoading = false;
                console.log(data);
            },
            toLoginIf403
        );



        function toLoginIf403(response) {
            alert('Error occurred, try again later');
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
