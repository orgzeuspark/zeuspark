(function () {
    'use strict';

    angular
        .module('zeusApp')
        .factory('prodDetailService', prodDetailService);

    prodDetailService.$inject = ['$http'];

    function prodDetailService($http) {
        var service = {
            getData: getData,
            getQuestion: getQuestion,
            getComment: getComment
        };

        return service;

        function getData(prodId) {
            return $http.get('/api/ProdDetail/' + prodId);
        };

        function getQuestion(prodId) {
            return $http.get('/api/Question/' + prodId);
        };

        function getComment(prodId) {
            return $http.get('/api/Comment/' + prodId);
        };
    }
})();