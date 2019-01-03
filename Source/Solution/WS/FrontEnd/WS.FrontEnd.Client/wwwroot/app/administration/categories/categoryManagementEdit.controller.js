(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("CategoryManagementEditController", categoryManagementEditController);

    categoryManagementEditController.$inject = ["common", "services"];

    function categoryManagementEditController(common, services) {
        var vm = this;

        vm.category = {};

        vm.selectedChild = {};

        vm.selectChildCategory = selectChildCategory;
        vm.isChildSelected = isChildSelected;

        vm.addChildCategory = addChildCategory;
        vm.editChildCategory = editChildCategory;

        vm.deleteChildCategory = deleteChildCategory;

        vm.removeSelectedCategory = removeSelectedCategory;

        vm.Upload = upload;

        activate();

        function activate() {
            return fetchActiveCategory();
        }

        function fetchActiveCategory() {
            var id = common.$state.params.id;

            return services.data.categories.GetCategoryForId(id).then(success, error);

            function success(data) {
                setCurrentCategory(data);
                return data;
            }

            function error() {
                return common.$state.go("admin.categories", null, { reload: true });
            }
        }

        function selectChildCategory(child) {
            vm.selectedChild = child;
        }

        function isChildSelected(child) {
            if ((typeof vm.selectedChild != "undefined" && vm.selectedChild != null) && child.Id === vm.selectedChild.Id) {
                return "selected";
            }
            return "";
        }

        function addChildCategory() {
            services.entity.setNewEntity();

            var modalInstance = common.$uibModal.open({
                templateUrl: "categoryModal.html",
                controller: 'EntityModalController as vm',
                resolve: {
                    header: function () { return "New Category"; },
                    data: {}
                }
            });

            modalInstance.result.then(addModalConfirmed);

            function addModalConfirmed(item) {
                item.Id = services.entity.getEntityTemporaryId();

                services.entity.StateSetAdded(item);

                // set the category for which the tag is created
                addToChildCollection(vm.category, item);
            }
        }

        function editChildCategory() {
            if (vm.selectedChild == null || !vm.selectedChild.Id) {
                common.logger.warning("No child category selected. Please select a child category before editing");
                return;
            }

            services.entity.setEditEntity(vm.selectedChild);

            common.$uibModal.open({
                templateUrl: 'categoryModal.html',
                controller: 'EntityModalController as vm',
                resolve: {
                    header: function () { return "Edit Category"; },
                    data: {}
                }
            });
        }

        function addToChildCollection(parent, item) {
            if (typeof parent.ChildCategories == "undefined" || parent.ChildCategories == null) {
                parent.ChildCategories = [];
            }

            item.ParentId = parent.Id;
            parent.ChildCategories.push(item);
        }

        function deleteChildCategory() {
            if (vm.selectedChild != null) {
                services.entity.StateSetDeleted(vm.selectedChild);
                vm.selectedChild = null;
            } else {
                common.logger.warning("No child category selected. Please select a child category before trying to delete");
            }
        }

        function removeSelectedCategory() {
            if (vm.category) {
                if (!services.data.categories.CanDelete(vm.category)) {
                    common.logger.warning("Cannot delete the selected category");
                    return;
                }

                services.entity.StateSetDeleted(vm.category);

                var nodeParent = services.data.categories.FindNodeParentInHirearchy(services.data.categories.categoryTree[0], vm.category);

                setCurrentCategory(nodeParent);
            }
        }

        function upload(file) {
            if (typeof file == "undefined" || file == null) {
                return;
            }

            var imageUploadData = {
                Type: "Category",
                Id: vm.category.Id
            };

            services.data.files.ImageUpload(file, imageUploadData, fileUploadSuccess, fileUploadFailedCallback);

            function fileUploadSuccess(response) {
                common.logger.info(services.language.file.messages.success.upload, response, services.language.file.title);
                services.images.category.AddCategoryImage(vm.category, response.data);
            }

            function fileUploadFailedCallback(response) {
                return services.error
                      .build(services.language.file.messages.error.upload, services.language.file.title)
                      .withResponse(response)
                      .show();
            }
        };

        function setCurrentCategory(category) {
            vm.category = category;
            services.data.categories.category = category;
        }
    };
})();