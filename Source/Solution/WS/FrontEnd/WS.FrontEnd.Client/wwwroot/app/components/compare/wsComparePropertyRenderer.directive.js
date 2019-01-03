(function () {
    angular
        .module("ws.components")
        .directive("wsComparePropertyRenderer", wsComparePropertyRendererDirective);

    /*
        Directive used to display the compare action on products
    */

    wsComparePropertyRendererDirective.$inject = [];

    function wsComparePropertyRendererDirective() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/compare/wsComparePropertyRenderer.directive.html',
            scope: {
                property: "=",
                valueIndex: "="
            },
            controller: wsComparePropertyRendererController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsComparePropertyRendererController.$inject = ["$filter"];

    function wsComparePropertyRendererController($filter) {
        var vm = this;

        vm.RenderValue = renderValue;

        function renderValue() {

            var value = vm.property.CompareValues[vm.valueIndex].Value;

            if (vm.property.PropertyType === "currency") {
                return $filter("currency")(value);
            }

            return value;
        }
    }
})();