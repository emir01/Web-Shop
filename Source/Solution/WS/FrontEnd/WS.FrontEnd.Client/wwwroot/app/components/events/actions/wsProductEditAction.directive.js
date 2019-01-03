(function () {
    angular
        .module("ws.components")
        .directive("wsProductEditAction", wsProductEditAction);

    function wsProductEditAction() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/events/actions/wsProductEditAction.directive.html',
            scope: {
                product: "=",
                showLabel: "=?"
            },

            link: link
        };

        return directive;

        function link(scope, element, attrs) {
            if (!attrs.showLabel) {
                scope.showLabel = true;
            }
        }
    }
})();