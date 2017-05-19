(function () {
    'use strict';

    const app = angular.module('config', ['ngCookies']);

    app.config(config);
    app.constant('serverUrl', 'http://localhost:8001');

    config.$inject = ['$httpProvider', '$locationProvider'];

    function config($httpProvider, $locationProvider) {
        $locationProvider.hashPrefix('');

        $httpProvider.defaults.xsrfCookieName = 'csrftoken';
        $httpProvider.defaults.xsrfHeaderName = 'X-CSRFToken';
        $httpProvider.defaults.withCredentials = true;
        $httpProvider.interceptors.push(['$cookies', function ($cookies) {
            return {
                'request': function (conf) {
                    conf.headers['X-CSRFToken'] = $cookies.get('csrftoken');
                    return conf;
                }
            };
        }]);
    }

})();