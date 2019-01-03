(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("ProductDataService", ProductDataService);

    ProductDataService.$inject = ["common", "language", "error"];

    function ProductDataService(common, language, error) {
        var factory = {};

        factory.products = [];
        factory.currentProducts = [];

        factory.product = {};

        factory.getDefaultSearchParameters = _getDefaultSearchParameters;
        factory.sortOptions = getSortOptions();

        factory.QueryProducts = queryProducts;
        factory.QueryProductsForCompareByIds = queryProductsForCompareByIds;
        factory.GetProduct = getProduct;

        factory.fetchSimilarProducts = fetchSimilarProducts;

        factory.GetEditProduct = getEditProduct;
        factory.UpdateProduct = updateProduct;
        factory.deleteProduct = deleteProduct;
        factory.activateProduct = activateProduct;

        factory.CreateProduct = createProduct;

        return factory;

        /////////////////////////////////////////////////
        ////////////////// PRODUCT QUERIES //////////////
        /////////////////////////////////////////////////

        function _getDefaultSearchParameters() {
            return {
                name: "",
                categoryId: null,
                manufacturerId: null,
                minPrice: 0,
                maxPrice: 500000,
                tagFilters: [],
                sort: factory.sortOptions[0].value,
                onSale: false
            };
        }

        function getSortOptions() {
            return [
                { label: "Name Asceding", value: 1, icon: "fa-sort-alpha-asc" },
                { label: "Name Descending", value: 2, icon: "fa-sort-alpha-desc" },
                { label: "Price Ascending", value: 3, icon: "fa-sort-amount-asc" },
                { label: "Price Descending", value: 4, icon: "fa-sort-amount-desc" }
            ];
        }

        /*
         * Query all the products in the system for the given parameters.
         * Returns a list of product read DTO objects for simple product display on a search screen.
         */
        function queryProducts(params) {
            var url = common.restConfig.products_base;

            return common.$http
                .get(url, {
                    params: params
                })
                .then(queryProductsComplete, queryProductsError);

            function queryProductsComplete(response) {
                factory.products = response.data;
                factory.currentProducts = response.data;
                return response.data;
            }

            function queryProductsError(error) {
                return error
                    .build(language.product.messages.error.get, language.product.title)
                    .withResponse(response)
                    .show();
            }
        }

        /*
       * Query all the products in the system for the given parameters.
       * Returns a list of product read DTO objects for simple product display on a search screen.
       */
        function fetchSimilarProducts(productId) {
            var url = common.restConfig.products_base + "/related/" + productId;

            return common.$http
                .get(url)
                .then(querySimilarProductsComplete, querySimilarProductsError);

            function querySimilarProductsComplete(response) {
                return response.data;
            }

            function querySimilarProductsError(response) {
                return error
                    .build(language.product.messages.error.gettingSimilar, language.product.title)
                    .withResponse(response)
                    .show();
            }
        }

        function queryProductsForCompareByIds(ids) {
            var url = common.restConfig.products_base + "compare";

            return common.$http
                .post(url, ids)
                .then(queryProductsByIdsComplete, queryProductsByIdsError);

            function queryProductsByIdsComplete(response) {
                return response.data;
            }

            function queryProductsByIdsError(response) {
                return error
                    .build(language.product.messages.error.gettingCompare, language.product.title)
                    .withResponse(response)
                    .show();
            }
        }

        /*
         * Query a single product. The information returned is for read purposes only
         */

        function getProduct(id) {
            var url = common.restConfig.products_base + id;

            return common.$http
                .get(url)
                .then(getProductComplete, getProductError);

            function getProductComplete(response) {
                factory.product = response.data;
                return response.data;
            }

            function getProductError(response) {
                return error
                    .build(language.product.messages.error.get, language.product.title, id)
                    .withResponse(response)
                    .show();
            }
        }

        /*
         * Query a single product. The information returned is for editing purposes and it includes
           extra information about the properties/tags for the product which are not visible if not added for the regular
           get product call
         */

        function getEditProduct(id) {
            if (!id) {
                return false;
            }

            // use the edit suffix to get the product including all the tags
            // even those for which there is no value set.
            var url = common.restConfig.products_base + "edit/" + id;

            return common.$http
                .get(url)
                .then(getProductComplete, getProductError);

            function getProductComplete(response) {
                factory.product = response.data;
                return response.data;
            }

            function getProductError(response) {
                return error
                    .build(language.product.messages.error.get, language.product.title, id)
                    .withResponse(response)
                    .show();
            }
        }

        /////////////////////////////////////////////////
        ////////////////// PRODUCT ACTIONS //////////////
        /////////////////////////////////////////////////

        /*
            Update the product information for the specific product
        */

        function updateProduct(product) {
            var url = common.restConfig.products_base;

            return common.$http
                .put(url, product)
                .then(updateProductComplete, updateProductError);

            function updateProductComplete(response) {
                factory.product = response.data;
                return response.data;
            }

            function updateProductError(response) {
                return error
                    .build(language.product.messages.error.update, language.product.title, product)
                    .withResponse(response)
                    .show();
            }
        }

        function deleteProduct(productId) {
            var url = common.restConfig.products_base + productId;

            return common.$http
                .delete(url, productId)
                .then(deleteProductComplete, deleteProductError);

            function deleteProductComplete(response) {
                return response.data;
            }

            function deleteProductError(response) {
                return error
                    .build(language.product.messages.error.delete, language.product.title, productId)
                    .withResponse(response)
                    .show();
            }
        }

        function activateProduct(productId) {
            var url = common.restConfig.products_base + "activate";

            return common.$http
                .put(url, productId)
                .then(activateProductComplete, activateProductError);

            function activateProductComplete(response) {
                return response.data;
            }

            function activateProductError(response) {
                return error
                    .build(language.product.messages.error.activate, language.product.title, productId)
                    .withResponse(response)
                    .show();
            }
        }

        function createProduct(product) {
            var url = common.restConfig.products_base;

            return common.$http
                .post(url, product)
                .then(createProductSuccess, createProductError);

            function createProductSuccess(result) {
                return result.data;
            }

            function createProductError(response) {
                return error
                    .build(language.product.messages.error.create, language.product.title, product)
                    .withResponse(response)
                    .show();
            }
        }
    }
})();