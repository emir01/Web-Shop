(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("keydownBroadcaster", keydownBroadcaster);

    keydownBroadcaster.$inject = ["common"];

    function keydownBroadcaster(common) {
        var directive = {
            restrict: "A",
            link: function ($scope, $element, $attrs) {
                $element.bind("keydown", function ($event) {
                    var keyCodesToBroadcast = $scope.$eval($attrs.keydownBroadcaster);
                    var keycode = $event.which || $event.keyCode;
                    var ctrlUsed = $event.ctrlKey;
                    var shiftUsed = $event.shiftKey;

                    var keycodeConfig = common.lodash.find(keyCodesToBroadcast, function (code) { return code.key === keycode });
                    if (keycodeConfig) {
                        if (!keycodeConfig.withControl && !keycodeConfig.withShift) {
                            common.$rootScope.$broadcast(common.events.keydown, keycode);
                            return;
                        }
                        if (keycodeConfig.withControl && keycodeConfig.withShift) {
                            if (ctrlUsed && shiftUsed) {
                                common.$rootScope.$broadcast(common.events.keydown, keycode);
                            }
                            return;
                        }

                        if (keycodeConfig.withControl) {
                            if (ctrlUsed) {
                                common.$rootScope.$broadcast(common.events.keydown, keycode);
                            }
                            return;
                        }

                        if (keycodeConfig.withShift) {
                            if (shiftUsed) {
                                common.$rootScope.$broadcast(common.events.keydown, keycode);
                            }
                            return;
                        }
                    }
                });
            }
        }

        return directive;
    }
})();