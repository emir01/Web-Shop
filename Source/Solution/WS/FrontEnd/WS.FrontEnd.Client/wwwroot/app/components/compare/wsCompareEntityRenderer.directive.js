(function () {
    angular
        .module("ws.components")
        .directive("wsCompareEntityRenderer", wsCompareEntityRendererDirective);
    /*
        Directive used to display the compare action on products
    */

    wsCompareEntityRendererDirective.$inject = [];

    function wsCompareEntityRendererDirective() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/compare/wsCompareEntityRenderer.directive.html',
            scope: {
                entitiesCompareObject: "="
            },
            controller: wsCompareEntityDirectiveController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function wsCompareEntityDirectiveController() {
        var vm = this;
    }
})();