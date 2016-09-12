
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
        .state('uploadshelf',{
            url: '/uploadshelf',
            templateUrl: 'App/product/upload-shelf.html',
            controller: 'UploadShelfCtrl'
        });

    });
    
})();
