(function () {
    "use strict";

    angular
        .module("ws.core")
        .constant("rest_config", restConfig())
        .constant("defaults", defaults())
        .constant("events", events())
        .constant("commands", commands())
        .constant("keys", keys())
        .constant("app.states", {})
        .config(configure)
        .run(loadingConfiguration)
        .run(stateConfiguration)
        .run(stateTransitionConfiguration);

    loadingConfiguration.$inject = ["common"];

    function loadingConfiguration(common) {
        common.$loading.setDefaultOptions({
            text: "LOADING",
            spinnerOptions: {
                color: "#007196"
            }
        });
    }

    stateConfiguration.$inject = ["stateAuthorizationConfiguration"];

    function stateConfiguration(stateAuthorizationConfiguration) {
        stateAuthorizationConfiguration.initialize();
    }

    stateTransitionConfiguration.$inject = ["common", "services"];

    function stateTransitionConfiguration(common, services) {
        common.$rootScope.$on('$stateChangeStart',
            function (event, toState, toParams, fromState, fromParams, options) {
                services.navigation.processStateChanges({
                    event: event,
                    toState: toState,
                    toParams: toParams,
                    fromState: fromState,
                    fromParams: fromParams,
                    options: options
                });

                common.$rootScope.transitioning = true;
                common.$loading.start("view");
            });

        common.$rootScope.$on('$stateChangeSuccess',
            function (event, toState, toParams, fromState, fromParams, options) {
                common.$rootScope.transitioning = false;
                common.$loading.finish("view");
            });

        common.$rootScope.$on('$stateChangeError',
           function (event, toState, toParams, fromState, fromParams, error) {
               common.logger.log(error, event, "State Change Error");
           });
    }

    function defaults() {
        var defaultConfig = {};

        defaultConfig.state = "products.search";
        defaultConfig.ProductDefaultImageUrl = "/content/images/product.png";

        return defaultConfig;
    }

    function events() {
        var eventConfig = {
            keydown: "keydown"
        };

        return eventConfig;
    }

    function commands() {
        var commands = {
            product: {
                "delete": " delete-product"
            }
        }

        return commands;
    }

    function keys() {
        var keyconfig = {
            escape: 27
        };

        return keyconfig;
    }

    function restConfig() {
        var config = {};
        config.filesGetUrl = "http://localhost:9555";
        config.baseUrl = "http://localhost:9555";

        config.apiUrl = config.baseUrl + "/api";
        config.tokenUrl = config.baseUrl + "/token";

        config.account_base = config.apiUrl + "/account/";
        config.products_base = config.apiUrl + "/products/";
        config.categories_base = config.apiUrl + "/categories/";
        config.tags_base = config.apiUrl + "/tags/";
        config.manufacturers_base = config.apiUrl + "/manufacturers/";
        config.files_base = config.apiUrl + "/files/";

        return config;
    }

    configure.$inject = ["$logProvider", "$httpProvider", "localStorageServiceProvider"];

    function configure($logProvider, $httpProvider, localStorageServiceProvider) {

        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }

        localStorageServiceProvider.setPrefix("ws");

        $httpProvider.interceptors.push("AuthorizationInterceptor");
        $httpProvider.interceptors.push("LoadmaskInterceptor");
    }
})();