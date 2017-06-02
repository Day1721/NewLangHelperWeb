﻿(function () {
    'use strict';

    angular
        .module('home')
        .controller('AddWordCtrl', addWordCtrl);

    addWordCtrl.$inject = ['$scope', '$location', 'localStorageService', 'serverUrl', 'http'];

    function addWordCtrl($scope, $location, localStorage, serverUrl, http) {
        $scope.title = 'AddWordCtrl';
        const data = localStorage.get('data');

        data.forEach(elem => elem.langs = `${elem.firstLanguage}|${elem.secondLanguage}`);

        $scope.data = {
            options: data
        };

        $scope.dataLength = Object.keys($scope.data).length || 0;

        $scope.words = [{}];

        $scope.extend = () => $scope.words.push({});

        $scope.submit = () => 
            http.post(`/groups/${$scope.data.selected.pk}/add-cards/`, $scope.words).then(
                successResponce => {
                    $location.path('/home');
                },
                errorResponce => {
                    console.log(errorResponce);
                });
    }
})();
