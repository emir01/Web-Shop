(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("compare", compare);

    compare.$inject = ["ProductCompareService", "TagCompareService"];

    function compare(productCompareService, tagCompareService) {

        var factory = {};

        factory.products = productCompareService;

        factory.tags = tagCompareService;

        return factory;
    }
})();