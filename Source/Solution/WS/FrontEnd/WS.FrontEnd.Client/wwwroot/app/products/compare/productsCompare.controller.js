(function () {
    "use strict";

    angular.module("ws.products")
		.controller("ProductsCompareController", ProductsCompareController);

    ProductsCompareController.$inject = ["common", "services"];

    function ProductsCompareController(common, services) {
        var vm = this;

        vm.EntitiesCompare = null;

        vm.CompareProducts = [];

        vm.FirstProduct = null;

        vm.SecondProduct = null;

        vm.CompareTags = null;

        activate();

        function activate() {
            return getCompareProducts().then(function () {
                common.logger.log("[Product Details] Activated Product Compare Controller");

                vm.FirstProduct = vm.CompareProducts[0];
                vm.SecondProduct = vm.CompareProducts[1];

                var compareTagsCollection = services.compare.tags.ProcessCompareTags(vm.FirstProduct.Properties, vm.SecondProduct.Properties);
                vm.CompareTags = compareTagsCollection;
            });
        }

        function getCompareProducts() {
            var productsToCompare = services.compare.products.selectedProducts;

            var productCompareIdValues = common.lodash.map(productsToCompare, function (product) {
                return product.Id;
            });

            return services.data.products.QueryProductsForCompareByIds(productCompareIdValues).then(function (data) {
                vm.EntitiesCompare = data;
                vm.CompareProducts = data.Entities;
                return data;
            });
        }
    }
})();
