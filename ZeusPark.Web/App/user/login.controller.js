(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$scope','$http','$location','$cookies','$rootScope'];

    function LoginCtrl($scope,$http,$location,$cookies,$rootScope) {
        $scope.user = {};
        $scope.LoginFail = false;

        $scope.ValidateAccount = function () {
                $scope.LoginFail = false;
                $scope.displaywaiting = true;
                event.preventDefault();
                if (!$scope.validator.validate()) {
                    return;
                }
                $http({
                    method: 'POST',
                    url: '/api/UserLogin',
                    data: $scope.user,
                    header: {'Content-Type': 'application/json; charset=utf-8'}
                })
                .success(function (data, status) {
                    $scope.displaywaiting = false;
                    if (data == 'OK') {
                        var id = $cookies.get('userid');
                        var name = $cookies.get('username');
                        var type = $cookies.get('acctype');
                        $rootScope.LoginUser.Name = name;
                        $rootScope.LoginUser.Id = id;
                        $rootScope.LoginUser.Type = type;
                        $rootScope.LoginUser.Isvalid = true;
                        $location.path( "/home" );
                    }
                    else {
                        $scope.LoginFail = true;
                        $scope.user.UserName = "";
                        $scope.user.Type = 0;
                        $scope.user.Password = "";
                    }
                });
            };
    }
})();
