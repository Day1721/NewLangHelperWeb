(function () {
    'use strict';

    angular
        .module('home')
        .controller('AddWordCtrl', addWordCtrl);

    addWordCtrl.$inject = ['$scope', '$location', 'localStorageService', 'serverUrl', '$http'];

    function addWordCtrl($scope, $location, localStorage, serverUrl, $http) {
        $scope.title = 'AddWordCtrl';
        let data = localStorage.get('data');

        data.forEach(elem => elem.langs = `${elem.firstLanguage}|${elem.secondLanguage}`);

        $scope.data = {
            options: data
        }

        $scope.dataLength = Object.keys($scope.data).length || 0;

        $scope.words = [{}];

        $scope.extend = function () {
            $scope.words.push({});
        };

        $scope.submit = function () {
            $http({
                method: 'POST',
                url: `${serverUrl}/groups/${$scope.data.selected.pk}/add-card/`,
                data: $scope.words
            }).then(
                successResponce => {
                    $location.path('/home');
                },
                errorResponce => {
                    console.log(errorResponce);
                });
        };
    }
})();
