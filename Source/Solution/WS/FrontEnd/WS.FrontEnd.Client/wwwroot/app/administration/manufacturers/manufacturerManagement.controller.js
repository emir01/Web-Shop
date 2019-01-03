(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("ManufacturerManagementController", ManufacturerManagementController);

    ManufacturerManagementController.$inject = ["common", "services", "manufacturers"];

    function ManufacturerManagementController(common, services, manufacturers) {
        var vm = this;

        vm.manufacturers = manufacturers;

        vm.AddNewManufacturer = addNewManufacturer;
        vm.EditManufacturer = editManufacturer;
        vm.DeleteManufacturerAfterConfirm = deleteManufacturerAfterConfirm;

        activate();

        function activate() { }

        function addNewManufacturer() {
            services.entity.setNewEntity();

            var modalInstance = common.$uibModal.open({
                templateUrl: "manufacturerModal.html",
                controller: 'EntityModalController as vm',
                resolve: {
                    header: function () { return "New Manufacturer"; },
                    data: {}
                }
            });

            modalInstance.result.then(addModalConfirmed);

            function addModalConfirmed(item) {
                return services.data.manufacturers
                    .AddNewManufacturer(item)
                    .then(manufacturerSavedOnServer);

                function manufacturerSavedOnServer(serverSavedManufacturer) {
                    common.logger.success(services.language.manufacturer.messages.success.create, serverSavedManufacturer, services.language.manufacturer.title);
                    vm.manufacturers.push(serverSavedManufacturer);
                }
            }
        }

        function editManufacturer(data) {
            services.entity.setEditEntity(data);

            var modalInstance = common.$uibModal.open(
            {
                controller: "EntityModalController as vm",
                templateUrl: "manufacturerModal.html",
                resolve: {
                    header: function () { return "Edit Manufacturer" },
                    data: {}
                }
            });

            modalInstance.result.then(editModalConfirmed);

            function editModalConfirmed(data) {
                return services.data.manufacturers
                    .EditManufacturer(data)
                    .then(manufacturerUpdatedOnServer, error);

                function manufacturerUpdatedOnServer(responseData) {
                    common.logger.success(services.language.manufacturer.message.success.update, responseData, services.language.manufacturer.title);
                    services.entity.applyEntityEdit();
                }

                function error(response) {
                    angular.merge(data, services.entity.undoChanges());
                }
            }
        }

        function deleteManufacturerAfterConfirm(data) {
            services.data.manufacturers
                .DeleteManufacturer(data)
                .then(manufacturerDeletedOnServer);

            function manufacturerDeletedOnServer() {
                common.logger.success(services.language.manufacturer.message.success.delete, data, services.language.manufacturer.title);

                var index = vm.manufacturers.indexOf(data);
                vm.manufacturers.splice(index, 1);
                data = null;
            }
        }
    };
})();