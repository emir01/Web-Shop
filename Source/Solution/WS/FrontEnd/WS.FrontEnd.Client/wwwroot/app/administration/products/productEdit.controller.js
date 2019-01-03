(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("ProductEditController", ProductEditController);

    ProductEditController.$inject = ["common", "services", "categories", "manufacturers", "product"];

    function ProductEditController(common, services, categories, manufacturers, product) {
        var vm = this;

        vm.product = product;
        vm.selectedImage = {};

        vm.categories = categories;
        vm.manufacturers = manufacturers;

        vm.UndoChanges = undoChanges;
        vm.Save = save;
        vm.Upload = upload;
        vm.deleteProduct = deleteProduct;
        vm.activateProduct = activateProduct;

        vm.createSubtitle = _createSubtitle;

        activate();

        function activate() {
            services.entity.setEditEntity(vm.product);
        }

        /////////////////////////////////////////////
        // EVENTS

        function undoChanges() {
            vm.product = services.entity.undoChanges();
        }

        function save() {
            // make the call to the service and apply current edit
            // only if the server resoinds with true
            return services.data.products.UpdateProduct(vm.product).then(productUpdateSuccess);

            function productUpdateSuccess(result) {
                common.logger.info("Product updated");

                // apply the edit
                services.entity.applyEntityEdit();

                // set the returned updated product as the new vm product and reset entity edit.
                vm.product = result;
                services.entity.setEditEntity(vm.product);
            }
        }

        function upload(file) {
            if (typeof file == "undefined" || file == null) {
                return;
            }

            var data = {
                Type: "Product",
                Id: vm.product.Id
            };

            services.data.files.ImageUpload(file, data, fileUploadSuccess, fileFailedUploadCallback);

            function fileUploadSuccess(response) {
                common.logger.info(services.language.file.messages.success.upload, response, services.language.file.title);
                services.images.product.AddProductImage(vm.product, response.data);
            }

            function fileFailedUploadCallback(response) {
                return services.error
                       .build(services.language.file.messages.error.upload, services.language.file.title)
                       .withResponse(response)
                       .show();
            }
        };

        function deleteProduct(productId) {
            services.data.products.deleteProduct(productId).then(function (product) {
                vm.product.Status = product.State;
            });
        }

        function activateProduct(productId) {
            services.data.products.activateProduct(productId).then(function (product) {
                vm.product.Status = product.State;
            });
        }

        function _createSubtitle() {
            if (vm.product.Status) {
                return "";
            }

            return "(This product is deactivated. Activate by clicking the Activate button)";
        }
    }
})();