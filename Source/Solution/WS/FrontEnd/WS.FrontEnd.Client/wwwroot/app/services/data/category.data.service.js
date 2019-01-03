(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("CategoryDataService", CategoryDataService);

    CategoryDataService.$inject = ["common", "DataUtils", "error", "language"];

    function CategoryDataService(common, dataUtils, error, language) {
        var factory = {};

        var enableRefresh = false;

        factory.categoryTree = [];

        factory.flatCategories = [];

        factory.category = {};

        factory.prime = _prime;

        factory.QueryCategoriesTree = queryCategoriesTree;

        factory.QueryFlatCategories = queryFlatCategories;

        factory.GetCategoryForId = getCategoryForId;

        factory.SaveCategories = saveCategories;

        factory.CategoriesEqual = categoriesEqual;

        factory.CanDelete = canDelete;

        factory.FindNodeParentInHirearchy = findNodeParentInHirearchy;

        factory.FindNodeInHirearchyForId = findNodeInHirearchyForId;

        factory.enableRefresh = _enableRefresh;

        return factory;

        /*
         * Query categories with a tree format
         */

        function _prime() {
            // return promises that need to be resolved to prime the category data
            return common.$q.all([queryCategoriesTree(), queryFlatCategories()]);
        }

        function _enableRefresh() {
            enableRefresh = true;
        }

        function queryCategoriesTree() {
            if (dataUtils.DefinedAndNotNullAndHasElements(factory.categoryTree) && !enableRefresh) {
                return common.$q(function (resolve) {
                    resolve(factory.categoryTree);
                });
            }
            enableRefresh = false;
            var url = common.restConfig.categories_base;

            return common.$http
                    .get(url)
                    .then(queryCategoriesComplete)
                    .catch(queryCategoriesError);

            function queryCategoriesComplete(response) {
                factory.categoryTree = response.data;
                return response.data;
            }

            function queryCategoriesError(response) {
                return error
                    .build(language.category.messages.error.get, language.category.title)
                    .withResponse(response)
                    .show();
            }
        }

        /*
         * Query a flat list of categories
         */

        function queryFlatCategories() {
            if (dataUtils.DefinedAndNotNullAndHasElements(factory.flatCategories) && !enableRefresh) {
                return common.$q(function (resolve) {
                    resolve(factory.flatCategories);
                });
            }

            enableRefresh = false;

            var url = common.restConfig.categories_base + "/all";

            return common.$http
                    .get(url)
                    .then(queryAllCategoriesComplete)
                    .catch(queryAllCategoriesError);

            function queryAllCategoriesComplete(response) {
                factory.flatCategories = response.data;
                return response.data;
            }

            function queryAllCategoriesError(response) {
                return error
                    .build(language.category.messages.error.get, language.category.title)
                    .withResponse(response)
                    .show();
            }
        }

        function getCategoryForId(id) {
            return common.$q(function (resolve, reject) {
                var category = findNodeInHirearchyForId(factory.categoryTree[0], id);

                if (category) {
                    return resolve(category);
                } else {
                    return reject(null);
                }
            });
        }

        /*
            Persist the category hirearch given with the category root
        */
        function saveCategories(categoryRoot) {
            var url = common.restConfig.categories_base;

            return common.$http
                .post(url, categoryRoot)
                .then(postCategoryRootComplete, postCategoryRootError);

            function postCategoryRootComplete(response) {
                return response.data;
            }

            function postCategoryRootError(response) {
                return error
                   .build(language.category.messages.error.save, language.category.title)
                   .withResponse(response)
                   .show();
            }
        }

        /*
            Check if the category can be deleted. 
            Currently only check if it is the root category
         */
        function canDelete(category) {
            if (category.Id === factory.flatCategories[0].Id) {
                return false;
            }

            return true;
        }

        /*
            Assert if two categories are equal based on the Id of the category
            or if the id is not present assert on a temporary id field defined 
            by the application.
        */
        function categoriesEqual(category, categoryToCheck) {
            if (typeof category.Id != "undefined" && category.Id != null) {
                return category.Id === categoryToCheck.Id;
            } else {
                return category.TmpId === categoryToCheck.TmpId;
            }
        }

        /*
             Search the category hirearchy starting from root and find the 
             node with the given id
         */

        function findNodeInHirearchyForId(root, id) {
            var node = root;

            // if the root is the actual element we are looking for
            if (node.Id == id) {
                return node;
            }

            if (!dataUtils.DefinedAndNotNullAndHasElements(node.ChildCategories)) {
                return null;
            }

            // traverse children of root and check if the given node is in the result set
            for (var i = 0; i < node.ChildCategories.length; i++) {
                var childCategory = root.ChildCategories[i];

                var insideResut = findNodeInHirearchyForId(childCategory, id);

                if (insideResut != null) {
                    node = insideResut;
                    return node;
                }
            }

            return null;
        }

        /*
            Search the category hirearchy starting from root
            and find the parent of the category passed in as node.
        */

        function findNodeParentInHirearchy(root, node) {
            var parent = null;

            if (!dataUtils.DataIsDefinedAndNotNull(root.ChildCategories)) {
                return null;
            }

            // traverse children of root and check if the given node is in the result set
            for (var i = 0; i < root.ChildCategories.length; i++) {
                var childCategory = root.ChildCategories[i];
                var insideResut = findNodeParentInHirearchy(childCategory, node);

                if (insideResut != null) {
                    parent = insideResut;
                    break;
                } else {
                    if (categoriesEqual(node, childCategory)) {
                        parent = root;
                        break;
                    }
                }
            }

            return parent;
        }
    }
})();