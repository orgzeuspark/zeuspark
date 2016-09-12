(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('MyOrderCtrl', MyOrderCtrl);

    MyOrderCtrl.$inject = ['$scope', '$http','$cookies'];

    function MyOrderCtrl($scope, $http, $cookies) {

        var userid = $cookies.get('userid');
        $http.get("/api/MyOrder/" + userid).success(function (response) {
            $scope.orders = response;
        });

    }
})();
