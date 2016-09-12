(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('ProdUploadCtrl', ProdUploadCtrl);

    ProdUploadCtrl.$inject = ['$scope', '$http', '$state','$cookies'];

    function ProdUploadCtrl($scope, $http, $state,$cookies) {

        $scope.displaywaiting = false;

        var datasource = new kendo.data.DataSource({
            pageSize: 20,
            schema: {
                model: {
                    fields: {
                        sizegrid: { validation: { required: false } },
                        colorgrid: { validation: { required: false } },
                        pricegrid: {
                            type: "number",
                            nullable: true,
                            validation: { min: 0, required: true }
                        },
                        orgpricegrid: {
                            type: "number",
                            nullable: true,
                            validation: { min: 0, required: false }
                        },
                        quantitygrid: {
                            type: "number",
                            nullable: true,
                            validation: { min: 0, required: true }
                        }
                    }
                }
            }
        });

        $scope.multiStyleGridOptions = {
            toolbar: [{ name: "create", text: "添加新规格" }],
            editable: true,
            dataSource: datasource,
            columns: [{
                field: "sizegrid",
                title: "尺寸*"
            },
                {
                    field: "colorgrid",
                    title: "颜色"
                },
                {
                    field: "pricegrid",
                    title: "售价*"
                },
                {
                    field: "orgpricegrid",
                    title: "标价"
                },
                {
                    field: "quantitygrid",
                    title: "库存*"
                },
                {
                    command: { name: "destroy", text: "删除" },
                    title: "操作"
                }]
        };

        $scope.isOneStyle = true;

        $scope.onestyle = function () {
            $scope.isOneStyle = true;
        };

        $scope.multistyle = function () {
            $scope.isOneStyle = false;
        };

        $scope.addMorePostParameters = function (e) {
            var id = e.files[0].uid;
            e.data = {
                picid: id
            };
        };

        $scope.onMainSelect = function (e) {
            if (e.sender.wrapper.find('.k-file').length >= 3) {
                e.preventDefault();
                alert("只能上传3张产品主图！");
            }
        }

        $scope.onOtherSelect = function (e) {
            if (e.sender.wrapper.find('.k-file').length >= 20) {
                e.preventDefault();
                alert("只能上传20张产品主图！");
            }
        }

        $scope.submitForm = function (event) {

            event.preventDefault();
            if (!$scope.validator.validate()) {
                //$location.path('/produpload');
                return;
            }

            $scope.displaywaiting = true;
            var newProduct = {};
            newProduct.prodName = $scope.prodName;
            newProduct.simpleDesc = $scope.simpleDesc;
            newProduct.userid = $cookies.get('userid');
            newProduct.priceDetail = [];
            if ($scope.isOneStyle) {
                var pdetail = {};
                pdetail.price = $scope.price;
                //newProduct.price = $scope.price;
                if ($scope.orgprice == "") {
                    //newProduct.orgprice = $scope.price;
                    pdetail.orgprice = $scope.price;
                }
                else {
                    pdetail.orgprice = $scope.orgprice;
                }
                pdetail.quantity = $scope.quantity;
                newProduct.priceDetail.push(pdetail);
            }
            else {
                for (var i = 0; i < $scope.multiStyleGrid._data.length; i++) {
                    var data = $scope.multiStyleGrid._data[i];
                    var pdetail = {};
                    pdetail.price = data.pricegrid;
                    pdetail.orgprice = data.orgpricegrid;
                    if (data.orgpricegrid == "") {
                        pdetail.orgprice = data.pricegrid;
                    }
                    else {
                        pdetail.orgprice = data.orgpricegrid;
                    }
                    pdetail.quantity = data.quantitygrid;
                    pdetail.color = data.colorgrid;
                    pdetail.size = data.sizegrid;
                    newProduct.priceDetail.push(pdetail);
                }
            }



            newProduct.keyword = $scope.keyword;
            newProduct.detailDesc = $scope.detailDesc;
            newProduct.groupid = $scope.selectedgroup;

            newProduct.mainfileids = [];
            var mainf = $scope.mainfiles.wrapper.find('.k-file');
            for (var index = 0; index < mainf.length; ++index) {
                newProduct.mainfileids.push(mainf[index].attributes['data-uid'].value);
            }
            newProduct.otherfileids = [];
            var otherf = $scope.otherfiles.wrapper.find('.k-file');
            for (var index = 0; index < otherf.length; ++index) {
                newProduct.otherfileids.push(otherf[index].attributes['data-uid'].value);
            }

            $http.post('/api/Upload', newProduct).then(function (response) {
                $scope.displaywaiting = false;
                //$scope.notf1.show({
                //    }, "ngTemplate");
                //$state.reload();
                $state.go('prodUploadSuccess');
            });
        };

    }
})();
