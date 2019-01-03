(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductDescription", wsProductDetails);

    function wsProductDetails() {
        var directive = {
            scope: {
                product: "=",
                showLabel: "="
            },

            restrict: "E",

            templateUrl: 'app/components/products/details/productDescription.directive.html',

            controller: wsProductDescriptionController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function wsProductDescriptionController() {
        activate();
        
        function activate() {}
    }
})();