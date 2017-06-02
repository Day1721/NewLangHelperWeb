(function() {
    'use strict';

    const dependencies = ['home', 'layout'];

    const app = angular.module('app', dependencies);
    
    app.run(runFunc);

    runFunc.$inject = ['$rootScope', '$cookies', 'loginPath'];

    function runFunc($rootScope, $cookies, loginPath) {
        $rootScope.username = $cookies.get('username');
        $rootScope.isLogged = $rootScope.username !== undefined;
        $rootScope.loginPath = loginPath;
    }
})();
