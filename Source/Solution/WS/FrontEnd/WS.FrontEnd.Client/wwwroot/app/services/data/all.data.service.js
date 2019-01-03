(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("data", DataServices);

    DataServices.$inject = [
        "common",
        "CategoryDataService",
        "FilesDataService",
        "ManufacturerDataService",
        "ProductDataService",
        "TagDataService"
    ];

    function DataServices(common, categoryDataService, filesDataService, manufacturerDataService, productDataService, tagDataService) {
        var factory = {};

        var primed = false;

        factory.categories = categoryDataService;
        factory.files = filesDataService;
        factory.manufacturers = manufacturerDataService;
        factory.products = productDataService;
        factory.tags = tagDataService;

        factory.prime = _primeDataService;

        return factory;

        function _primeDataService() {
            if (!primed) {
                var promises = [];

                for (var service in factory) {
                    if (factory.hasOwnProperty(service)) {
                        if (typeof (factory[service].prime) === "function") {
                            promises.push(factory[service].prime());
                        }
                    }
                }

                primed = true;
                return common.$q.all(promises);
            } else {
                return common.$q(function (resolve) {
                    resolve(true);
                });
            }
        }
    }
})();