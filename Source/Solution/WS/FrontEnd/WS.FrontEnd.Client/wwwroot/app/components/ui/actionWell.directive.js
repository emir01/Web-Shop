(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("actionWell", actionWell);

    function actionWell() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/ui/actionWell.directive.html',
            transclude: true
        };

        return directive;
    }
})();