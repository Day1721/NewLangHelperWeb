(function () {
    'use strict';

    angular
        .module('home')
        .controller('AddGroupCtrl', addGroupCtrl);

    addGroupCtrl.$inject = ['$scope', 'localStorageService', 'http', '$location'];

    function addGroupCtrl($scope, localStorage, http, $location) {
        $scope.title = 'AddGroupCtrl';

        $scope.submit = function() {
            const groups = Object.keys(localStorage.get('data'));
            console.log(groups);
            if ($scope.groupName in groups) return;

            http.post('/groups/', {
                    name: $scope.groupName,
                    firstLanguage: $scope.firstLanguage,
                    secondLanguage: $scope.secondLanguage,
                    words: []
            }).then(
                successResponce => {
                    console.log(successResponce);
                    $location.path('/home');
                },
                errorResponce => {
                    console.log(errorResponce);
                });
        };
    }
})();
