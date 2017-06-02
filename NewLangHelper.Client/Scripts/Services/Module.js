(function () {
    'use strict';

    const app = angular.module('services', ['config']);

    app.service('http', httpService);
    app.constant('serverUrl', 'http://localhost:8001');

    httpService.$inject = ['$http', 'serverUrl', 'debug'];

    function httpService($http, serverUrl, debug) {
        this.get = path => ({
            then: (success, error) => {
                $http.get(`${serverUrl}${path}`).then(
                    success, response => handler(error, response)
                );
            }
        });

        this.post = (path, body) => ({
            then: (success, error) => {
                $http.post(`${serverUrl}${path}`, body).then(
                    success,
                    response => handler(error, response)
                );
            }
        });

        function handler(error, responce) {
            if (debug) console.log(responce);
            error(responce);
        }
    }
})();