/*
 * Serves as a wrapper for all the services in the application
 * 
 * - data
 *      - category
 *      - files
 *      - manufacturers
 *      - products
 *      - tags
 * - compare
 *      - products
 *      - tags
 * - images,
 *      - category
 *      - product
 * - entity
 * - auth
 * - language
 * - utils
 *      -data
 */
(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("services", services);

    services.$inject = [
        "data",
        "compare",
        "images",
        "EntityEditService",
        "UserAuthorization",
        "DataUtils",
        "navigationService",
        "productSearchService",
        "language",
        "error"
    ];

    function services(
            data,
            compare,
            images,
            entityEditService,
            userAuthorization,
            dataUtils,
            navigationService,
            productSearchService,
            language,
            error
        ) {

        var factory = {};

        factory.data = data;

        factory.compare = compare;

        factory.images = images;

        factory.entity = entityEditService;

        factory.auth = userAuthorization;

        factory.navigation = navigationService;

        factory.search = productSearchService;

        factory.language = language;

        factory.error = error;

        factory.utils = {
            data: dataUtils
        };

        return factory;
    }
})();