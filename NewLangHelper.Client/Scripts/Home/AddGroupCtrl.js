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
                    $location.path('/home');
                },
                errorResponce => {
                    if (errorResponce.status === 400)
                        alert(errorResponce.data.error);
                    else alert('Error occurred, try again later');
                });
        };
    }
})();
