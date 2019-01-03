(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("focusOnKey", onEnterKeyDirective);

    onEnterKeyDirective.$inject = ["common"];

    function onEnterKeyDirective(common) {
        var directive = {
            restrict: "A",
            link: function ($scope, $element, $attrs) {
                $scope.$on(common.events.keydown, function (event, data) {
                    if (parseInt(data) === parseInt($attrs.focusOnKey)) {
                        $element.focus();
                        $element.select();
                    }
                });
            }
        }

        return directive;
    }
})();