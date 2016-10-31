
(function () {
    'use strict';
    
    angular.module('zeusApp').config(function ($stateProvider, $urlRouterProvider,$compileProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
        .state('home', {
            url: '/',
            templateUrl: 'App/layout/home.html',
            controller: 'HomeCtrl'
        })
        .state('login', {
            url: '/login',
            templateUrl: 'App/user/login.html',
            controller: 'LoginCtrl'
        })
        .state('prodList',{
            url: '/prodlist/{groupid}',
            templateUrl: 'App/product/list.html',
            controller: 'ProdListCtrl'
        })
        .state('prodDetail',{
            url: '/proddetail/{prodid}',
            templateUrl: 'App/product/detail.html',
            controller: 'ProdDetailCtrl'
        })
        .state('prodUpload',{
            url: '/produpload',
            templateUrl: 'App/product/upload.html',
            controller: 'ProdUploadCtrl'
        })
        .state('prodUploadSuccess',{
            url: '/produploadsuccess',
            templateUrl: 'App/product/upload-success.html'
        })
        .state('checkoutSuccess',{
            url: '/checkoutSuccess',
            templateUrl: 'App/product/checkout-success.html'
        })
        .state('register',{
            url: '/register',
            templateUrl: 'App/user/register.html',
            controller: 'RegisterCtrl'
        })
        .state('checkout',{
            url: '/checkout',
            templateUrl: 'App/product/checkout.html',
            controller: 'CheckoutCtrl'
        })
        .state('myorder',{
            url: '/myorder',
            templateUrl: 'App/user/myorder.html',
            controller: 'MyOrderCtrl'
        })
        .state('payorder',{
            url: '/payorder/{orderid}',
            templateUrl: 'App/product/payorder.html',
            controller: 'PayOrderCtrl'
        })
        .state('uploadshelf',{
            url: '/uploadshelf',
            templateUrl: 'App/product/upload-shelf.html',
            controller: 'UploadShelfCtrl'
        })
        .state('orderlist',{
            url: '/orderlist',
            templateUrl: 'App/admin/order-list.html',
            controller: 'OrderListCtrl'
        })
        .state('question',{
            url: '/question',
            templateUrl: 'App/user/question.html',
            controller: 'QuestionCtrl'
        })
        .state('aboutsend',{
            url: '/aboutsend',
            templateUrl: 'App/about/send.html'
        })
        .state('aboutpay',{
            url: '/aboutpay',
            templateUrl: 'App/about/pay.html'
        })
        .state('aboutservice',{
            url: '/aboutservice',
            templateUrl: 'App/about/service.html'
        });

    });
    
})();
