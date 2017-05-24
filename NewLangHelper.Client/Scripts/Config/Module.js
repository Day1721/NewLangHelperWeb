(function () {
    'use strict';

    const app = angular.module('config', ['ngRoute', 'ngCookies', 'LocalStorageModule']);

    app.config(config);
    app.constant('serverUrl', 'http://localhost:8001');

    config.$inject = ['$httpProvider', '$locationProvider', 'localStorageServiceProvider'];

    function config($httpProvider, $locationProvider, localStorageProvider) {
        localStorageProvider.setPrefix('NLH');

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