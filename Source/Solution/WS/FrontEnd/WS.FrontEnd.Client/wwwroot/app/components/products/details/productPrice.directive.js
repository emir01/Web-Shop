(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductPrice", wsProductPrice);

    function wsProductPrice() {
        var directive = {
            scope: {
                price: "=",
                label: "@"
            },

            restrict: "E",

            templateUrl: 'app/components/products/details/productPrice.directive.html',

            controller: wsProductPriceController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function wsProductPriceController() {
        activate();

        function activate() { }
    }
})();