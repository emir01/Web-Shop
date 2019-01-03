(function () {
    angular
        .module("ws.components")
        .directive("wsCompareSidebar", wsCompareSidebarDirective);

    /*
        Directive used to display the compare action on products
    */

    wsCompareSidebarDirective.$inject = [];

    function wsCompareSidebarDirective() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/compare/wsCompareSidebar.directive.html',
            scope: {
            },
            controller: wsCompareSidebarController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsCompareSidebarController.$inject = ["common", "services"];

    function wsCompareSidebarController(common, services) {
        var vm = this;

        vm.NavigateToCompare = _navigateToCompare;

        vm.SelectedCompareProducts = services.compare.products.selectedProducts;

        vm.RemoveFromCompare = services.compare.products.RemoveProductFromCompare;

        vm.ClearCompareProducts = services.compare.products.ClearCompareProducts;

        function _navigateToCompare(event) {
            if (services.compare.products.selectedProducts.length <= 1) {
                common.logger.warning("Cannot compare less than 2 selected products");
                event.preventDefault();
                return false;
            } else {
                common.$state.go("products.compare");
            }
        }
    }
})();