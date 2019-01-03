(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("TagManagementController", TagManagement);

    TagManagement.$inject = ["common", "services", "categories"];

    function TagManagement(common, services, categories) {
        var vm = this;

        vm.categories = [];
        vm.selectedCategory = {};
        vm.tagsForCategory = [];

        vm.addNewTag = addNewTag;
        vm.editTag = editTag;
        vm.deleteTagAfterConfirm = deleteTagAfterConfirm;

        // used as an event delegate to the directive that renders the category tree
        vm.selectCategoryHandler = selectCategoryHandler;

        activate();

        function activate() {
            vm.categories = categories;
            selectCategoryHandler(vm.categories[0]);
        }

        /*
            Event handler we use to recieve information from the category hirearchy that a 
            category has been selected allowing us to load tags for the category.
        */

        function selectCategoryHandler(category) {
            vm.selectedCategory = category;
            getTagsForActiveCategory(category.Id);
        }

        /*
            Add new tag handler which shows the modal for adding new tags
        */

        function addNewTag() {
            // before opening the modal for editing we are going to set the 
            // tag we are editing on the tag edit service
            services.entity.setNewEntity();

            var modalInstance = common.$uibModal.open({
                templateUrl: "tagModal.html",
                controller: 'EntityModalController as vm',
                resolve: {
                    header: function () { return "New Tag"; },
                    data: {}
                }
            });

            modalInstance.result.then(addModalConfirmed);

            function addModalConfirmed(item) {
                // set the category for which the tag is created
                item.CategoryId = vm.selectedCategory.Id;

                return services.data.tags
                    .AddNewTagForCategory(item)
                    .then(tagSavedOnServer);

                function tagSavedOnServer(serverSavedTag) {
                    common.logger.info(services.language.tag.messages.success.create, serverSavedTag, services.language.tag.title);
                    vm.tagsForCategory.push(serverSavedTag);
                }
            }
        }

        /*
            Edit handler method which shows the modal dialog for tag editing
        */

        function editTag(tag) {
            services.entity.setEditEntity(tag);

            var modalInstance = common.$uibModal.open({
                templateUrl: 'tagModal.html',
                controller: 'EntityModalController as vm',
                resolve: {
                    header: function () { return "Edit Tag"; },
                    data: {}
                }
            });

            modalInstance.result.then(editModalConfirmed);

            function editModalConfirmed(item) {
                return services.data.tags
                    .UpdateTag(item)
                    .then(tagUpdatedOnServer, error);

                function tagUpdatedOnServer(serverPersistedTag) {
                    common.logger.info(services.language.tag.messages.success.update, serverPersistedTag, services.language.tag.title);
                    services.entity.applyEntityEdit();
                }

                function error() {
                    angular.merge(tag, services.entity.undoChanges());
                }
            }
        }

        /*
          
          The handler for deleting tags after confirmation message.
    
            After the tag deletion is confirmed by the user try and delete the tag on the server
            after which we are deleting the tag on the client.
        */

        function deleteTagAfterConfirm(tag) {

            return services.data.tags
                .DeleteTag(tag)
                .then(tagDeletedOnServer);

            function tagDeletedOnServer() {
                common.logger.success("Tag Deleted");
                // splice/remove the tag from the current collection of tags
                var index = vm.tagsForCategory.indexOf(tag);
                vm.tagsForCategory.splice(index, 1);
            }
        }

        /*  
            Return all the tags for a given selected category
        */

        function getTagsForActiveCategory(categoryId) {
            if (categoryId != undefined && categoryId !== "") {
                return services.data.tags
                        .GetTagsForCategory(categoryId)
                        .then(tagsForCategoryRetrieved);
            } else {
                return [];
            }

            /*
                Executed when we get tags for a given category
            */
            function tagsForCategoryRetrieved(data) {
                vm.tagsForCategory = data;
                return vm.tagsForCategory;
            }
        }
    };
})();