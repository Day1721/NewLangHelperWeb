(function () {
    'use strict';

    angular
        .module('home')
        .controller('AddGroupCtrl', addGroupCtrl);

    addGroupCtrl.$inject = ['$scope', 'localStorageService', '$http', 'serverUrl', '$location'];

    function addGroupCtrl($scope, localStorage, $http, serverUrl, $location) {
        $scope.title = 'AddGroupCtrl';

        $scope.submit = function() {
            const groups = Object.keys(localStorage.get('data'));
            console.log(groups);
            if ($scope.groupName in groups) return;

            $http({
                method: 'POST',
                url: `${serverUrl}/groups/`,
                data: {
                    name: $scope.groupName,
                    firstLanguage: $scope.firstLanguage,
                    secondLanguage: $scope.secondLanguage,
                    words: []
                }
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
