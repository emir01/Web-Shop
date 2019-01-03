(function () {
    'use strict';

    angular
        .module('ws.core')
        .config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider", "app.states"];

    function config($stateProvider, $urlRouterProvider, appStates) {
        $urlRouterProvider.otherwise("products/search");
        appStates.core = {
            shell: "shell",
            about: "about"
        };

        $stateProvider
            .state(appStates.core.shell, {
                url: "/",
                abstract: true,
                views: {
                    '': {
                        templateUrl: "app/layout/shell.html"
                    },
                    'nav@shell': {
                        templateUrl: "app/layout/nav.html",
                        controller: "NavController",
                        controllerAs: "vm"
                    },
                    'footer@shell': {
                        templateUrl: "app/layout/footer.html"
                    }
                },
                resolve: {
                    primedData: [
                        "services", function (services) {
                            return services.data.prime().then(function (values) {
                                return values;
                            });
                        }
                    ]
                }
            })
            .state(appStates.core.about, {
                url: "about",
                parent: appStates.core.shell,
                template: "<h1> About </h1>"
            });
    }
})();
