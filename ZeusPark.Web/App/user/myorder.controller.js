(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('MyOrderCtrl', MyOrderCtrl);

    MyOrderCtrl.$inject = ['$scope', '$http','$cookies','$location','$state'];

    function MyOrderCtrl($scope, $http, $cookies,$location,$state) {

        var userid = $cookies.get('userid');
        $http.get("/api/MyOrder/" + userid).success(function (response) {
            $scope.orders = response;
        });

        $scope.payorder = function (orderid) {
            $location.path('/payorder/'+ orderid);
        };

        $scope.cancelorder = function(orderid) {
            $http.put("/api/MyOrder/" + orderid, 0).success( function (response) {
                $state.reload();
            });
        };

    }
})();
