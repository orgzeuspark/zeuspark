(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('CheckoutCtrl', CheckoutCtrl)
        .directive('wizard', [function() {
            return {
            restrict: 'EA',
            scope: {
                stepChanging: '=',
                finished: '='
            },
            compile: function(element, attr) {
                element.steps({
                    headerTag: ".header-tags",
                    bodyTag: attr.bodyTag,
                    transitionEffect: "fade"
                });
                
                return {
                    //pre-link
                    pre:function() {},
                    //post-link
                    post: function(scope, element) {
                        element.on('stepChanging', scope.stepChanging);
                        element.on('finished', scope.finished);
                    }
                }
            }
            };
        }]);

    CheckoutCtrl.$inject = ['$scope','$http','$state','$cookies','ngCart'];

    function CheckoutCtrl($scope,$http,$state,$cookies,ngCart) {

        $scope.ngCart = ngCart;
        $scope.discount = 0;
        $scope.orderid = generateOrderID();

        function _arrayBufferToBase64( buffer ) {
            var binary = '';
            var bytes = new Uint8Array( buffer );
            var len = bytes.byteLength;
            for (var i = 0; i < len; i++) {
                binary += String.fromCharCode( bytes[ i ] );
            }
            return window.btoa( binary );
        };

        function generateOrderID() {
            function S4() {  
                return (((1+Math.random())*0x10000)|0).toString(16).substring(1);  
            }  
            return (S4() + S4() + S4() + S4() + S4()).toLowerCase(); 
        };

        $scope.stepChanging = function(){
        };

        $scope.finished = function() {
            ngCart.empty(true);
            $state.go('checkoutSuccess');
        };

        $scope.paywx = function() {
            var total = ngCart.totalCost() - $scope.discount;
            
            var url = "/api/WXPay?id=" + $scope.orderid + "&total=" + total;
            $http.get(url,{responseType: "arraybuffer"}).then(function(response) { 
                $scope.image= _arrayBufferToBase64(response.data); 
            });

            var order = createOrder();

            $http.post('/api/Order', order).then(function(response){
                $scope.displaywxorder = true;
            });

        };

        function createOrder() {
            var total = ngCart.totalCost() - $scope.discount;
            var order = {};
            order.OrderUnique = $scope.orderid;
            order.OrderUserId = $cookies.get('userid');
            order.OrderAmount = total;
            order.DiscountAmount = $scope.discount;
            order.DiscountCode = $scope.code;
            order.OrderDetail = '';
            order.ProdIds = '';
            var items = ngCart.getItems();
            for (var i = 0; i < items.length; i++) {
                var itemdetail = "产品: " + items[i].getName() + " & 数量: " + items[i].getQuantity() + " & 价格：" + items[i].getPrice() + " ## ";
                order.OrderDetail += itemdetail;
                order.ProdIds += items[i].getId() + ";";
            };
            if (items.length > 0) {
                order.OrderImg = items[0].getData().MainImageUrl;
            }

            order.Contactor = $scope.contactor;
            order.Mobile = $scope.mobile;
            order.Email = $scope.email;
            order.Telephone = $scope.telephone;
            order.City = $scope.city;
            order.PostCode = $scope.postcode;
            order.Address = $scope.address;
            order.Note = $scope.note;

            return order;
        }

        $scope.payali = function() {
            var total = ngCart.totalCost() - $scope.discount;
 
            var order = createOrder();

            $http.post('/api/Order', order).then(function(response){
                $scope.displayaliorder = true;
            });

        };

        $scope.verifyDiscount = function() {
            var total = ngCart.totalCost();
            var url = "/api/Discount?codeid=" + $scope.code + "&total=" + total;
            $http.get(url).then(function(response){
                if (response.data.IsValid) {
                    $scope.discount = response.data.DiscountNum;;
                    $scope.discountTitle = '有效优惠码:' + response.data.Title;
                }
                else {
                    $scope.discount = 0;
                    $scope.discountTitle = '无有效优惠码:' + response.data.Message;
                }
                
            });
        }
        //$scope.$on('$viewContentLoaded', StepWizard.initStepWizard);
        //StepWizard.initStepWizard();
    }
})();
