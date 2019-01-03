(function () {
    "use strict";

    angular.module("blocks.entities")
        .controller("EntityModalController", entityModalController);

    entityModalController.$inject = ["$uibModalInstance", "$scope", "data", "header", "logger", "EntityEditService"];

    function entityModalController($uibModalInstance, $scope, data, header, logger, entityEditService) {
        // State flag used to determine if we are processing key events
        // Set shortly after the modal is rendered and prevents the modal
        // from processing outise key events
        var processingKeyEvents = false;

        var vm = this;

        vm.Rendered = false;

        vm.ok = confirmAction;

        vm.cancel = cancelAction;

        vm.entity = {};
        
        // The data object is used for any lookup collections
        // so it is important we keep it
        vm.Data = data;
        
        // set the modal header for the modal
        vm.ModalHeader = header || "Edit Entity";
        
        vm.CheckKey = checkKey;

        activate();

        function activate() {
            vm.entity = entityEditService.getEntity();

            $uibModalInstance.rendered.then(function () {
                modalRenderedStateTrue();
            });
        }

        ////////////////////////////
        // VM Events

        function checkKey($event) {
            // check if the enter key has been pressed
            if (processingKeyEvents) {
                if ($event.keyCode === 13) {
                    confirmAction();
                }
            }
        }

        function confirmAction() {
            modalRenderedStateFalse();
            var updatedEntity = entityEditService.getEntity();
            $uibModalInstance.close(updatedEntity);
        }

        function cancelAction() {
            modalRenderedStateFalse();
            $uibModalInstance.dismiss("cancel");
        }

        ////////////////////////////
        // INTERNALS

        /*
            Setting the state of the Modal
        */

        function modalRenderedStateTrue() {
            vm.Rendered = true;

            setTimeout(function () {
                processingKeyEvents = true;
            }, 100);
        }

        function modalRenderedStateFalse() {
            vm.Rendered = false;
            processingKeyEvents = false;
        }
    }
})();