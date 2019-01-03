(function () {
    'use strict';

    angular
        .module('ws.products')
        .config(config);

    config.$inject = ["$stateProvider", "app.states"];

    function config($stateProvider, appStates) {
        appStates.products = {
            base: "products",
            search: "products.search",
            details: "products.details",
            compare: "products.compare"
        }

        $stateProvider
            .state(appStates.products.base, {
                parent: "shell",
                url: "products",
                template: "<div class='core-ui-view' ui-view></div>",
                abstract: true
            })
            .state(appStates.products.search, {
                url: "/search?{params:json}",
                templateUrl: "app/products/search/productSearch.html",
                controller: "ProductSearchController",
                controllerAs: "vm",
                resolve: {
                    categories: ["services", "primedData", function (services) {
                        return services.data.categories.QueryFlatCategories();
                    }],
                    manufacturers: ["services", "primedData", function (services) {
                        return services.data.manufacturers.QueryManufacturers();
                    }],
                    tagTypes: ["services", "primedData", function (services) {
                        return services.data.tags.GetAllTags();
                    }]
                }
            })
            .state(appStates.products.details, {
                url: "/details/:id",
                templateUrl: "app/products/details/productDetails.html",
                controller: "ProductDetailsController",
                controllerAs: "vm"
            })
            .state(appStates.products.compare, {
                url: "/compare",
                templateUrl: "app/products/compare/productsCompare.html",
                controller: "ProductsCompareController",
                controllerAs: "vm"
            });
    }
})();
