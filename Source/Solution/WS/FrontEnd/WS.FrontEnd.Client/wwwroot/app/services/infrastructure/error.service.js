(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("error", errorService);

    errorService.$inject = ["common"];

    function errorService(common) {
        var debugMode = false;

        var factory = {
        };

        factory.reject = _reject;

        factory.build = _build;

        return factory;

        function _reject(response) {
            if (!response) {
                response = "Error";
            }
            return common.$q.reject(response);
        }

        function _build(message, title, data) {
            return new wsError(message, title, data);
        }

        function _showError(message, title, data) {

            if (!debugMode) {
                data = {}
            }

            common.logger.error(message, data, title);
        }

        function shouldShowErrorMessage(wsError) {
            if (errorOveridesShowMessage(wsError)) {
                return true;
            }

            if (errorContainsUnatoriazedResponse(wsError)) {
                return false;
            } else {
                return true;
            }

            function errorContainsUnatoriazedResponse(wsError) {
                if (wsError.response && wsError.response.status && wsError.response.status === 401) {
                    return true;
                } else {
                    return false;
                }
            }

            function errorOveridesShowMessage(wsError) {
                return wsError.overideShow;
            }
        }

        function wsError(message, title, data) {
            var that = this;

            that.message = message;
            that.title = title;
            that.data = data;

            that.response = null;

            that.overideShow = false;

            that.redirectState = null;

            that.show = function () {
                if (that.response && typeof (that.response.data) == "string") {
                    that.message = that.response.data;
                }

                if (shouldShowErrorMessage(that)) {
                    _showError(that.message, that.title, that.data);
                }

                if (typeof (that.redirectState) === "string") {
                    common.$state.go(that.redirectState);
                }

                return _reject(that.response);
            };

            that.withResponse = function (response, overideShow) {
                that.response = response;

                if (typeof (overideShow) === "boolean") {
                    that.overideShow = overideShow;
                }

                return that;
            };

            that.withRedirect = function (state) {
                that.redirectState = state;
                return that;
            }
        }
    }
})();