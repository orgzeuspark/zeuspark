(function () {
    'use strict';

    angular
        .module('zeusApp')
        .factory('prodlistService', prodlistService);

    prodlistService.$inject = ['$http'];

    function prodlistService($http) {
        var service = {
            getData: getData,
            getFilter: getFilter
        };

        return service;

        function getData(groupid) {


            var result = $http.get('/api/ProdList/'+groupid).success(function(data) {
                return data;
            });
            
            return result;
        };

        function getFilter(groupid) {
            var result = $http.get('/api/Search/'+groupid).success(function(data) {
                return data;
            });

            return result;
        };

    }
})();