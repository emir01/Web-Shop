(function () {
    angular
        .module("ws.components")
        .directive("wsCategoryEditAction", wsCategoryEditAction);

    function wsCategoryEditAction() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/events/actions/wsCategoryEditAction.directive.html',
            scope: {
                category: "=",
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