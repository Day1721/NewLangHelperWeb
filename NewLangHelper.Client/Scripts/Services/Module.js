(function () {
    'use strict';

    const app = angular.module('services', ['config']);

    app.service('http', httpService);
    app.constant('serverUrl', 'http://localhost:8001');

    httpService.$inject = ['$http', 'serverUrl', 'debug', '$location'];


    function httpService($http, serverUrl, debug, $location) {
        this.get = path => ({
            then: (success, error) => {
                $http.get(`${serverUrl}${path}`).then(
                    success,
                    response => handler(error, response)
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

        this.delete = path => ({
            then: (success, error) => {
                $http({
                    method: 'DELETE',
                    url: `${serverUrl}${path}`
                }).then(
                    success,
                    response => handler(error, response)
                );
            }
        });

        function handler(error, response) {
            if (debug) console.log(response);
            if (response.status === 403) {
                $location.path('/login');
                $location.replace();
            }
            error(response);
        }
    }
})();