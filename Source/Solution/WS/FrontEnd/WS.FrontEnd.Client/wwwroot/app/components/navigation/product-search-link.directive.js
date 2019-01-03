(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("productSearchLink", productSearchLink);

    function productSearchLink() {
        var directive = {
            restrict: "AE",
            scope: {
                linkText: '@'
            },
            templateUrl: 'app/components/navigation/product-search-link.directive.html',
            controller: productSearchLinkController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function productSearchLinkController() {
        var vm = this;

        activate();

        function activate() {}
    }
})();