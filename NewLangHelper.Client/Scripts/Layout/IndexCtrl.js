(function () {
    'use strict';

    angular
        .module('layout')
        .controller('IndexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope'];

    function indexCtrl($scope) {
        $scope.title = 'IndexCtrl';

        $scope.loginPath = 'Views/LoginPartial.html';
    }
})();
