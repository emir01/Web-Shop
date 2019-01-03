(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductCategory", wsProductDetails);

    function wsProductDetails() {
        var directive = {
            scope: {
                product: "="
            },

            restrict: "E",
            templateUrl: 'app/components/products/details/productCategory.directive.html',
            controller: wsProductCategoryController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function wsProductCategoryController() {
        activate();
        
        function activate() {}
    }
})();