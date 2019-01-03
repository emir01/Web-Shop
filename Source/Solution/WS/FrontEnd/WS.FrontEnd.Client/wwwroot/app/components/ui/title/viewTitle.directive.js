(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("viewTitle", actionWell);

    function actionWell() {
        var directive = {
            restrict: "A",
            templateUrl: 'app/components/ui/title/viewTitle.directive.html',
            transclude: true,
            scope: {
                "viewTitle": "@",
                "subTitle": "@"
            }
        };

        return directive;
    }
})();