(function () {
    'use strict';

    angular
        .module('home')
        .controller('ShareCtrl', shareCtrl);

    shareCtrl.$inject = ['$scope', 'localStorageService', 'http', '$location'];

    function shareCtrl($scope, localStorage, http, $location) {
        $scope.title = 'ShareCtrl';
        const data = localStorage.get('data');

        data.forEach(elem => elem.langs = `${elem.firstLanguage}|${elem.secondLanguage}`);

        $scope.data = {
            options: data
        };

        $scope.useCode = () => http.get(`/invite/${$scope.code}/`).then(
            successResponce => {
                alert('Group was successfully imported');
                $location.path('/home');
            },
            errorResponce => {
                if (errorResponce.status === 404)
                    alert('Wrong share code, please try again.');
                else alert('Error occured, please try again later');
            }
        );
    }
})();
