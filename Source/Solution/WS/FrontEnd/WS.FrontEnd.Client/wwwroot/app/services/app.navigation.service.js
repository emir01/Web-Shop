(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("navigationService", navigationService);

    navigationService.$inject = ["common", "app.states"];

    function navigationService(common, appStates) {
        var cachedData = {
            searchParams: null,
            previousState: "",
            nextState: ""
        };

        var factory = {};

        factory.processStateChanges = _processStateChanges;

        factory.getPreviousState = _getPreviousState;
        factory.getNextState = _getNextState;

        factory.states = appStates;

        return factory;

        /*
         * Process a state change for any navigation related features.
         * 
         * Currently only looking at the case where we are coming from the
         * product search screen to save any filters if used.
         * 
         * Data contains:
         *     toState: toState,
         *     toParams: toParams,
         *     fromState: fromState,
         *     fromParams: fromParams
         * 
         */

        function _processStateChanges(data) {
            _processGeneralNavigationData(data);

            _processNavigatingFromProductSearch(data);
            _processNavigatingToProductSearch(data);
        }

        function _processGeneralNavigationData(data) {
            cachedData.previousState = data.fromState;
            cachedData.nextState = data.toState;
        }

        function _processNavigatingFromProductSearch(data) {
            if (data.fromState.name === appStates.products.search && data.toState.name !== appStates.products.search) {
                var searchParams = data.fromParams.params;
                cachedData.searchParams = searchParams;
            }
        }

        function _processNavigatingToProductSearch(data) {
            if (data.toState.name === appStates.products.search && _hasProductSearchParams()) {
                data.event.preventDefault();

                var params = _getProductSearchParams();

                if (data.toParams.params) {
                    params = data.toParams.params;
                }

                _clearProductSearchParams();

                common.$state.go(appStates.products.search, { params: params });
            }
        }

        /*
         * Specific cache data methods related to product search
         * ==============================================================
         */

        function _hasProductSearchParams() {
            if (cachedData.searchParams) {
                return true;
            }

            return false;
        }

        function _clearProductSearchParams() {
            cachedData.searchParams = null;
        }

        function _getProductSearchParams() {
            return cachedData.searchParams;
        }

        function _getPreviousState() {
            return cachedData.previousState;
        }

        function _getNextState() {
            return cachedData.nextState;
        }
    }
})();