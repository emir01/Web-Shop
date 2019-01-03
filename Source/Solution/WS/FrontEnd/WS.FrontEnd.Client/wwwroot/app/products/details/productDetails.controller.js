(function () {
    "use strict";

    angular.module("ws.products")
		.controller("ProductDetailsController", ProductDetailsController);

    ProductDetailsController.$inject = ["common", "services"];

    function ProductDetailsController(common, services) {
        var vm = this;

        vm.product = {};
        vm.relatedProducts = [];

        activate();

        function activate() {
            return common.$q.all([getProduct(), getRelatedProducts()]).then(function () {
                common.logger.log("Activated Product Details View");
            });
        }

        function getProduct() {
            var currentProductId = common.$state.params.id;

            return services.data.products.GetProduct(currentProductId).then(function (data) {
                vm.product = data;
                return vm.product;
            });
        }

        function getRelatedProducts() {
            var currentProductId = common.$state.params.id;

            return services.data.products.fetchSimilarProducts(currentProductId).then(function (data) {
                vm.relatedProducts = data;
                return vm.relatedProducts;
            });
        }
    }
})();
