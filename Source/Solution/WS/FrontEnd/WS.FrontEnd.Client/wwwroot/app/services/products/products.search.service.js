(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("productSearchService", produectSearchService);

    produectSearchService.$inject = ["common", "ProductDataService"];

    function produectSearchService(common, productsDataService) {
        var factory = {};

        factory.getSearchParams = productsDataService.getDefaultSearchParameters;

        factory.addCategoryId = _addCategoryId;
        factory.addManufacturerId = _addManufacturerId;

        factory.go = _go;

        return factory;

        function _addCategoryId(searchParams, catId) {
            searchParams.categoryId = catId;
        }

        function _addManufacturerId(searchParams, manufacturerId) {
            searchParams.manufacturerId = manufacturerId;
        }

        function _go(searchParams) {
            common.$state.go("products.search", { params: searchParams, test: true }, { reload: true });
        }
    }
})();