(function () {
    angular
        .module("ws.components")
        .directive("showIfLogged", isLoggedDirective);

    isLoggedDirective.$inject = ["common", "services"];

    function isLoggedDirective(common, services) {
        var directive = {
            restrict: "A",
            scope: {
                showIfLogged: "="
            },

            link: link
        };

        function link(scope, element) {
            var showIfLogged = scope.showIfLogged;

            common.$rootScope.$watch(services.auth.IsAuthorized, function (newValue) {
                element.hide();

                if (typeof showIfLogged === "boolean") {
                    if (showIfLogged) {
                        if (newValue) {
                            element.show();
                        }
                    } else {
                        if (!newValue) {
                            element.show();
                        }
                    }
                } else {
                    if (newValue) {
                        element.show();
                    }
                }
            });
        }

        return directive;
    }
})();