(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("TagDataService", TagDataService);

    TagDataService.$inject = ["common", "DataUtils", "error", "language"];

    function TagDataService(common, dataUtils, error, language) {
        var factory = {};

        factory.tagsForCategory = [];

        factory.allTags = [];

        factory.GetTagsForCategory = getTagsForCategory;

        factory.GetAllTags = getAllTags;

        factory.AddNewTagForCategory = addTagForCategory;

        factory.UpdateTag = updateTag;

        factory.DeleteTag = deleteTag;

        factory.prime = _prime;

        return factory;

        function _prime() {
            return common.$q.all([getAllTags()]);
        }

        /*
         * Return the system tags for the category with the given category id.
         */

        function getTagsForCategory(categoryId) {
            var url = common.restConfig.tags_base;

            return common.$http
                .get(url, { params: { categoryId: categoryId } })
                .then(getTagsComplete)
                .catch(getTagsError);

            function getTagsComplete(response) {
                factory.tagsForCategory = response.data;
                return response.data;
            }

            function getTagsError(response) {
                common.logger.error(language.tag.messages.error.getTags, categoryId, language.tag.title);

                return common.$q(function (resolve, reject) {
                    reject(response);
                });
            }
        }

        /*
         * Return all the tags in the system
         */

        function getAllTags() {
            if (dataUtils.DefinedAndNotNullAndHasElements(factory.allTags)) {
                return common.$q(function (resolve) {
                    resolve(factory.allTags);
                });
            }

            var url = common.restConfig.tags_base;

            return common.$http
                .get(url, {})
                .then(getTagsComplete)
                .catch(getTagsError);

            function getTagsComplete(response) {
                factory.tagsForCategory = response.data;
                return response.data;
            }

            function getTagsError(response) {
                common.logger.error(language.tag.messages.error.getTags, null, language.tag.title);

                return common.$q(function (resolve, reject) {
                    reject(response);
                });
            }
        }

        /*
            Add a new tag in the system for the specified category
        */

        function addTagForCategory(tag) {
            var url = common.restConfig.tags_base;

            return common.$http
                .post(url, tag)
                .then(addTagComplete, addTagError);

            function addTagComplete(response) {
                return response.data;
            }

            function addTagError(response) {
                return error
                  .build(language.tag.messages.error.create, language.tag.title)
                  .withResponse(response)
                  .show();
            }
        }

        /*
            Update the specified tag for the given specifiy category
        */

        function updateTag(tag) {
            var url = common.restConfig.tags_base;

            return common.$http
                .put(url, tag)
                .then(putTagComplete, putTagError);


            function putTagComplete(response) {
                return response.data;
            }

            function putTagError(response) {
                return error
                  .build(language.tag.messages.error.update, language.tag.title)
                  .withResponse(response)
                  .show();
            }
        }

        /*
            Delete the given tag from the category it belongs to
        */

        function deleteTag(tag) {
            var url = common.restConfig.tags_base;

            return common.$http
                .delete(url, { params: { id: tag.Id } })
                .then(deleteTagComplete)
                .catch(deleteTagError);

            function deleteTagComplete(response) {
                return response.data;
            }

            function deleteTagError(response) {
                return error
                  .build(language.tag.messages.error.delete, language.tag.title)
                  .withResponse(response)
                  .show();
            }
        }
    }
})();