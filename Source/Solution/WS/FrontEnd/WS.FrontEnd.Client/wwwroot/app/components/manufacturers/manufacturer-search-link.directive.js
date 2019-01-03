(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("manufacturerSearchLink", manufacturerSearchLink);

    function manufacturerSearchLink() {
        var directive = {
            scope: {
                manufacturerId: "=?",
                manufacturerName: "=?"
            },

            restrict: "E",
            templateUrl: 'app/components/manufacturers/manufacturer-search-link.directive.html',

            controller: manufacturerSearchLinkController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    manufacturerSearchLinkController.$inject = ["services"];

    function manufacturerSearchLinkController(services) {
        var vm = this;

        vm.search = _search;

        function _search($event, manufacturerId) {
            $event.preventDefault();

            var searchParams = services.search.getSearchParams();
            services.search.addManufacturerId(searchParams, manufacturerId);
            services.search.go(searchParams);
        }
    }
})();