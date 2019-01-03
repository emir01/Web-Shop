(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("ProductCompareService", ProductCompareService);

    ProductCompareService.$inject = ["logger", "common", "localStorageService"];

    function ProductCompareService(logger, common, localStorageService) {

        var localStorageKey = "compare_products_local_storage_key";

        var factory = {};

        factory.selectedProducts = [];

        factory.SelectProductForCompare = selectProductForCompare;
        factory.ClearCompareProducts = clearCompareProducts;
        factory.RemoveProductFromCompare = removeProductFromCompare;

        initialize();

        return factory;

        // METHODS
        ////////////////////////////////////////////////

        function initialize() {
            readFromLocalStorage();
        }

        function selectProductForCompare(product) {
            if (factory.selectedProducts.length === 2) {
                logger.warning("You can only compare 2 products at a given time! Please remove one of the products if you want to add something else", null, "Product Compare");
                return;
            }

            if (!productSelectedForCompare(product)) {
                factory.selectedProducts.push(product);
                saveToLocalStorage();
            }
        }

        function clearCompareProducts() {
            for (var i = factory.selectedProducts.length - 1; i >= 0; i--) {
                removeProductFromCompare(factory.selectedProducts[i].Id);
            }
        }

        function removeProductFromCompare(id) {
            var index = common.lodash.findIndex(factory.selectedProducts, function (product) {
                return product.Id === id;
            });

            if (index !== -1) {
                factory.selectedProducts.splice(index, 1);
                saveToLocalStorage();
            }
        }

        // INTERNALS
        ////////////////////////////////////////////////

        function productSelectedForCompare(product) {
            return !common.lodash.isUndefined(getComparedProductWithId(product.Id));
        }

        function getComparedProductWithId(id) {
            return common.lodash.find(factory.selectedProducts, function (pr) {
                return pr.Id === id;
            });
        }

        function saveToLocalStorage() {
            var json = JSON.stringify(factory.selectedProducts);
            localStorageService.set(localStorageKey, json);
        }

        function readFromLocalStorage() {
            var json = localStorageService.get(localStorageKey);

            factory.selectedProducts = JSON.parse(json);
            if (!factory.selectedProducts) {
                factory.selectedProducts = [];
            }
        }
    }
})();