(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('LayoutCtrl', LayoutCtrl);

    LayoutCtrl.$inject = ['$scope','$rootScope','$cookies'];

    function LayoutCtrl($scope,$rootScope,$cookies) {

        if ($rootScope.LoginUser == undefined){
            var id = $cookies.get('userid');
            var name = $cookies.get('username');
            var type = $cookies.get('acctype');
            $rootScope.LoginUser = {
                Name:"",
                Id:"",
                Type:0,
                Isvalid:false };
                
            if (id != undefined && id > 0) {
                $rootScope.LoginUser.Name = name;
                $rootScope.LoginUser.Id = id;
                $rootScope.LoginUser.Type = type;
                $rootScope.LoginUser.Isvalid = true;
            }
        }



    }
})();
