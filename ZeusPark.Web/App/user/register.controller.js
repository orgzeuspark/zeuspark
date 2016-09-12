(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('RegisterCtrl', RegisterCtrl);

    RegisterCtrl.$inject = ['$scope', '$http', '$state'];

    function RegisterCtrl($scope, $http, $state) {
        $scope.data = {};
        $scope.duplicate = false;

        $scope.notf1Options = {
            templates: [{
                type: "ngTemplate",
                template: $("#notificationTemplate").html()
            }]
        };

        $scope.submitForm = function (event) {
            event.preventDefault();
            if (!$scope.validator.validate()) {
                return;
            }
            else if ($scope.data.passwordvalue.length < 6 || $scope.data.passwordvalue != $scope.data.passwordconfirm) {
                alert('密码最少为6位，并请保持重复输入密码一致！');
                return;
            }

            var userAccount = {};
            userAccount.accountname = $scope.data.accountname;
            userAccount.mail = $scope.data.mail;
            userAccount.telephone = $scope.data.telephone;
            userAccount.username = $scope.data.username;
            userAccount.password = $scope.data.passwordvalue;

            $http.post('/api/UserRegister', userAccount).then(function (response) {
                if (response.data == false) {
                    $http.put('/api/UserRegister', userAccount).then(function (response) {
                        if (response.data == 'OK') {
                            $scope.notf1.show({}, "ngTemplate");
                            //$state.reload();
                            $state.go('login');
                        }
                    });
                }
                else {
                    $scope.duplicate = true;
                }

            });


        }
    }
})();
