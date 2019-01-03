(function () {
    angular
        .module("ws.components")
        .directive("wsCompareAction", wsCompareActionDirective);

    /*
        Directive used to display the compare action on products
    */

    wsCompareActionDirective.$inject = [];

    function wsCompareActionDirective() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/events/actions/wsCompareAction.directive.html',
            scope: {
                wsCompareActionProduct: "="
            },

            controller: wsCompareActionController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsCompareActionController.$inject = ["services"];

    function wsCompareActionController(services) {
        var vm = this;

        vm.compareActionClick = handleCompareActionClick;

        function handleCompareActionClick() {
            services.compare.products.SelectProductForCompare(vm.wsCompareActionProduct);
        }
    }
})();