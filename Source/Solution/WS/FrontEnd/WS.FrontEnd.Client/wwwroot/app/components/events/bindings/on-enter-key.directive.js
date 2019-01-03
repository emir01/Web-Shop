(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("onEnterKey", onEnterKeyDirective);

    function onEnterKeyDirective() {
        var directive = {
            restrict: "A",
            scope: {
                handler:"=onEnterKey"
            },
            link: function ($scope, $element, $attrs) {
                $element.bind("keypress", function ($event) {
                    var keycode = $event.which || $event.keyCode;

                    if (keycode === 13) {
                        $scope.handler();
                        $event.preventDefault();
                    }
                });
            }
        }

        return directive;
    }
})();