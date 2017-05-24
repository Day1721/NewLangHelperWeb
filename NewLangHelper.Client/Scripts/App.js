(function() {
    'use strict';

    const dependencies = ['home', 'layout'];

    const app = angular.module('app', dependencies);
    
    app.run(runFunc);

    runFunc.$inject = ['$rootScope', '$cookies'];

    function runFunc($rootScope, $cookies) {
        $rootScope.username = $cookies.get('username');
        $rootScope.isLogged = $rootScope.username !== undefined;
    }
})();
