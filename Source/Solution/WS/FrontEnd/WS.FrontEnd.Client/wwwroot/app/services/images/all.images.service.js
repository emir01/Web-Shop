(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("images", allImageServices);

    allImageServices.$inject = ["CategoryImagesService", "ProductImagesService"];

    function allImageServices(categoryImagesService, productImagesService) {

        var factory = {};

        factory.category = categoryImagesService;
        factory.product = productImagesService;

        return factory;
    }
})();