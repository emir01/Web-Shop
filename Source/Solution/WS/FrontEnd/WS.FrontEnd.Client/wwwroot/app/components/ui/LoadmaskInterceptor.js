(function () {
    "use strict";

    angular
        .module("ws.components")
        .factory("LoadmaskInterceptor", loadmaskInterceptor);

    loadmaskInterceptor.$inject = ["$injector", "$q"];

    function loadmaskInterceptor($injector, $q) {
        var factory = {};

        factory.request = request;
        factory.requestError = requestError;

        factory.response = response;
        factory.responseError = responseError;

        return factory;

        function request(config) {
            if (isAnApiCall(config.url)) {
                startMask();
            }
            return config;
        }

        function requestError(rejectReason) {
            stopMask(true);
            return $q.reject(rejectReason);
        }

        function response(response) {
            if (isAnApiCall(response.config.url)) {
                stopMask(false);
            }

            return response;
        }

        function responseError(response) {
            stopMask(true);

            return $q.reject(response);
        }

        function isAnApiCall(url) {
            return url.indexOf("api") >= 0;
        }

        function startMask() {
            var common = $injector.get("common");
            common.$loading.start("view");
        }

        function stopMask(error) {
            var common = $injector.get("common");
            if (!common.$rootScope.transitioning || error) {
                common.$loading.finish("view");
            }
        }
    }
})();