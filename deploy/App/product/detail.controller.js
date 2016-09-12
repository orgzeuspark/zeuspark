(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('ProdDetailCtrl', ProdDetailCtrl);

    ProdDetailCtrl.$inject = ['$scope','$timeout','$stateParams','$cookies','$http','prodDetailService'];

    function ProdDetailCtrl($scope, $timeout,$stateParams,$cookies,$http,prodDetailService) {

        $scope.switchStyle = function(index) {
            $scope.price = $scope.prodData.data.SkuInfoList[index].Price /100;
            $scope.orgprice = $scope.prodData.data.SkuInfoList[index].OrgPrice /100;
            $scope.quantity = $scope.prodData.data.SkuInfoList[index].Quantity;
            $scope.style = response.data.SkuInfoList[index].Size + " " + response.data.SkuInfoList[index].Color;
            $scope.infoID = response.data.SkuInfoList[index].infoID;
        };

        $scope.descriptionTab = function() {
            $scope.tab = 1;
        };

        $scope.commentTab = function() {
            $scope.tab = 2;
            prodDetailService.getComment($stateParams.prodid).then(function(response){  
                $scope.commentlist = response.data;
            });
        };

        $scope.questionTab = function() {
            $scope.tab = 3;
            prodDetailService.getQuestion($stateParams.prodid).then(function(response){  
                $scope.questionlist = response.data;
            });
        };

        $scope.submitQuestion = function() {
            var question  = {};
            question.Question = $scope.newquestion;
            question.ProductID = $stateParams.prodid;
            question.SubmitBy = $cookies.get('username');

            $http.post('/api/Question', question).then(function (response) {
                prodDetailService.getQuestion($stateParams.prodid).then(function(result){  
                    $scope.questionlist = result.data;
                });
                $scope.newquestion = "";
                alert('问题已经成功提交，我们会为你及时解答。');
            });

        };

        $scope.$on('$viewContentLoaded', OwlCarousel.initOwlCarousel);
        $scope.$on('$viewContentLoaded', StyleSwitcher.initStyleSwitcher);
        $scope.$on('$viewContentLoaded', function(event) {
            prodDetailService.getData($stateParams.prodid).then(function(response){           
                $scope.prodData = response;
                $scope.price = response.data.SkuInfoList[0].Price /100;
                $scope.orgprice = response.data.SkuInfoList[0].OrgPrice /100;
                $scope.quantity = response.data.SkuInfoList[0].Quantity;
                $scope.infoID = response.data.SkuInfoList[0].InfoID;
                $scope.style = response.data.SkuInfoList[0].Size + " " + response.data.SkuInfoList[0].Color;

                $timeout(function() {
                    MasterSliderShowcase2.initMasterSliderShowcase2();
                },0);
                
            });

        });

    }
})();
