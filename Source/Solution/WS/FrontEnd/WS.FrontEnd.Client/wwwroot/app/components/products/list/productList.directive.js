(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductList", wsProductList);

    function wsProductList() {
        var directive = {
            scope: {
                products: "=",
                productCardClass: "@",
                productNavRoot: "@"
            },

            restrict: "EA",
            templateUrl: 'app/components/products/list/productList.directive.html',

            controller: wsProductListController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsProductListController.$inject = ["common", "services"];

    function wsProductListController(common, services) {
        var vm = this;
        var defaultCardClass = "col-xs-12 col-sm-6 col-md-6 col-lg-4";

        vm.processedNavRoot = vm.productNavRoot;
        vm.ProcessProductImage = processProductImage;

        activate();

        function activate() {
            parseProductNavRoot();
            setupProductCardClass();
        }

        function parseProductNavRoot() {
            var lastIndex = vm.productNavRoot.length - 1;
            if (vm.productNavRoot[lastIndex - 1] === "\\" || vm.productNavRoot[lastIndex] === "/") {
                vm.processedNavRoot = vm.productNavRoot.substr(0, lastIndex);
            }
        }

        function setupProductCardClass() {
            if (!services.utils.data.StringIsDefinedAndNotEmpty(vm.productCardClass)) {
                vm.productCardClass = defaultCardClass;
            }
        }

        function processProductImage(product) {
            if (product.ImageUrl) {
                return product.ImageUrl;
            } else {
                return product.CategoryImage;
            }
        }
    }
})();