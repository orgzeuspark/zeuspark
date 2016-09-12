(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('ProdListCtrl', ProdListCtrl);

    ProdListCtrl.$inject = ['$scope', '$stateParams', '$timeout', 'prodlistService'];

    function ProdListCtrl($scope, $stateParams, $timeout, prodlistService) {
        prodlistService.getData($stateParams.groupid).then(function (response) {
            $scope.prodData = response;
        });

        prodlistService.getFilter($stateParams.groupid).then(function (response) {
            $scope.filterlist = [];
            $scope.query = '';
            for (var i = 0; i < response.data.length; i++) {
                    var f = {};
                    f.value = response.data[i];
                    f.checked = false;
                    $scope.filterlist.push(f);

            }
        });

        $scope.updateFilter = function (filter) {
            $scope.query = '';
            for (var i = 0; i < $scope.filterlist.length; i++) {
                var item = $scope.filterlist[i];
                if (item.checked) {
                    $scope.query += (item.value + ';');
                }

            };
            $scope.query = $scope.query.substring(0,$scope.query.length - 1);
        };

        $scope.clearFilter = function() {
            $scope.query = '';
            for (var i = 0; i < $scope.filterlist.length; i++) {
                var item = $scope.filterlist[i];
                item.checked = false;

            };
        };

        /*$scope.$on('$viewContentLoaded', function(event) {
            prodlistService.getData($stateParams.groupid).then(function(response){           
                $scope.prodData = response;
                $timeout(function() {
                    $('body').ctshop({
                        currency: '¥',
                        paypal: {
                            currency_code: 'RMB'
                        },
                        empty_text: '你的购物车是空的！',
                    });
                },0);
                
            });

        });*/

    }
})();
