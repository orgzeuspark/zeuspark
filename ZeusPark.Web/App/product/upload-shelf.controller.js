(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('UploadShelfCtrl', UploadShelfCtrl);

    UploadShelfCtrl.$inject = ['$scope', '$http', '$state'];

    function UploadShelfCtrl($scope, $http, $state) {

        $scope.onshelf = function (id) {
            var shelf = {};
            shelf.productId = id;
            shelf.isOnShelf = true;
            $http.post('/api/ProdShelf/', shelf).then(function (response) {
                $state.reload();
            });

        };

        $scope.offshelf = function (id) {
            var shelf = {};
            shelf.productId = id;
            shelf.isOnShelf = false;
            $http.post('/api/ProdShelf/', shelf).then(function (response) {
                $state.reload();
            });
        };

        $scope.deleteprod = function (id) {
            if (confirm("确定删除此产品？ 删除后产品数据将不能回复")) {
                $http.delete('/api/ProdShelf/' + id).then(function (response) {
                    $state.reload();
                });
            }

        };

        // Column sytles
        var headerStyle = "text-align: right; font-size: 13px; font-weight:bold";
        var contentStyle = "text-align: right; font-size: 12px";
        $scope.mainGridOptions = {
            dataSource: {
                transport: {
                    read: "/api/ProdShelf",
                    dataType: "json"
                },
                pageSize: 20,
                serverPaging: false,
                serverFiltering: false
            },
            sortable: {
                mode: "single",
                allowUnsort: false
            },
            filterable: {
            },
            pageable: {
                refresh: true,
                pageSizes: ["all",10, 20, 50]
            },
            columns: [
                {
                    title: "产品ID",
                    field: "ProductID"
                },
                {
                    title: "产品名称",
                    field: "Name"
                },
                {
                    title: "状态",
                    field: "DisplayStatus"
                },
                {
                    title: "操作",
                    template: "<kendo-button ng-click='onshelf(#=ProductID#)'> 上架 </kendo-button> <kendo-button ng-click='offshelf(#=ProductID#)'> 下架 </kendo-button> <kendo-button ng-click='deleteprod(#=ProductID#)'> 删除 </kendo-button>"
                }
                
            ]
        };
    }

})();