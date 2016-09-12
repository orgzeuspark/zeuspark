(function () {
    'use strict';

    angular
        .module('zeusApp')
        .controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = ['$scope'];

    function HomeCtrl($scope) {
        $scope.$on('$viewContentLoaded', RevolutionSlider.initRSfullWidth);
        $scope.$on('$viewContentLoaded', StyleSwitcher.initStyleSwitcher);
        $scope.$on('$viewContentLoaded', OwlCarousel.initOwlCarousel);
              
        
    }
})();
