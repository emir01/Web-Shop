(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductName", wsProductDetails);

    function wsProductDetails() {
        var directive = {
            scope: {
                product: "=",
                showManufacturer: "=?"
            },
            restrict: "E",
            templateUrl: 'app/components/products/details/productName.directive.html',
            link: link
        };
        
        return directive;

        function link(scope, element, attrs) {
            if (!attrs.showManufacturer) {
                scope.showManufacturer = true;
            }
        }
    }
})();