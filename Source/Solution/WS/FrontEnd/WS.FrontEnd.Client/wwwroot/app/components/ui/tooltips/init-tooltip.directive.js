(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("initTooltip", actionWell);

    function actionWell() {
        var directive = {
            restrict: "A",
            scope: {
                initTooltip: '='
            },
            link: _link
        };

        return directive;
    }

    function _link($scope, $element, $attrs) {
        if ($scope.initTooltip) {
            $element.attr("title", $scope.initTooltip);
        }

        $element.tooltip({
            container: "body",
            delay: { "show": 700, "hide": 100 }
        });
    }
})();