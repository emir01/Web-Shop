(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("ManufacturerDataService", ManufacturerDataService);

    ManufacturerDataService.$inject = ["common", "DataUtils", "language", "error"];

    function ManufacturerDataService(common, dataUtils, language, error) {

        var factory = {};

        factory.manufacturers = [];

        factory.prime = _prime;

        factory.QueryManufacturers = queryManufacturers;

        factory.AddNewManufacturer = addNewManufacturer;

        factory.EditManufacturer = editManufacturer;

        factory.DeleteManufacturer = deleteManufacturer;

        return factory;

        function _prime() {
            return common.$q.all([queryManufacturers()]);
        }

        /*
            Return the manufacturers from the current system.
        */

        function queryManufacturers() {
            if (dataUtils.DefinedAndNotNullAndHasElements(factory.manufacturers)) {
                return common.$q(function (resolve) {
                    resolve(factory.manufacturers);
                });
            }

            var url = common.restConfig.manufacturers_base;

            return common.$http
                .get(url)
                .then(queryManufacturersComplete, queryManufacturersError);

            function queryManufacturersComplete(response) {
                factory.manufacturers = response.data;
                return response.data;
            }

            function queryManufacturersError(response) {
                return error
                  .build(language.manufacturer.messages.error.get, language.manufacturer.title)
                  .withResponse(response)
                  .show();
            }
        }

        function addNewManufacturer(data) {
            var url = common.restConfig.manufacturers_base;

            return common.$http
                .post(url, data)
                .then(addManufacturerComplete, addManufacturerError);

            function addManufacturerComplete(response) {
                return common.$q.resolve(response.data);
            }

            function addManufacturerError(response) {
                return error
                    .build(language.manufacturer.messages.error.create, language.manufacturer.title)
                    .withResponse(response)
                    .show();
            }
        }

        function editManufacturer(data) {
            var url = common.restConfig.manufacturers_base;

            return common.$http
                .put(url, data)
                .then(manufacturerUpdated, manufacturerUpdateError);

            function manufacturerUpdated(response) {
                return response.data;
            }

            function manufacturerUpdateError(response) {
                return error
                    .build(language.manufacturer.messages.error.update, language.manufacturer.title)
                    .withResponse(response)
                    .show();
            }
        }

        function deleteManufacturer(data) {
            var url = common.restConfig.manufacturers_base;

            return common.$http
                .delete(url, { params: { id: data.Id } })
                .then(manufacturerDeleted)
                .catch(manufacturerDeleteError);

            function manufacturerDeleted(response) {
                return response.data;
            }

            function manufacturerDeleteError(response) {
                return error
                     .build(language.manufacturer.messages.error.delete, language.manufacturer.title)
                     .withResponse(response)
                     .show();
            }
        }
    }
})();