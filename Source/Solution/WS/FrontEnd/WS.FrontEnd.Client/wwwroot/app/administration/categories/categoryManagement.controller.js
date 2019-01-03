(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("CategoryManagementController", CategoryManagement);

    CategoryManagement.$inject = ["common", "services", "categories"];

    function CategoryManagement(common, services, categories) {
        var vm = this;

        vm.categories = categories;

        vm.selectedCategory = services.data.categories.activeCategory;

        vm.selectCategoryHandler = selectCategoryHandler;

        vm.PersistCategories = persistCategories;
        vm.RefreshCategories = refreshCategories;

        activate();

        function activate() {
            common.$rootScope.$watch(function () { return services.data.categories.category; }, function () {
                vm.selectedCategory = services.data.categories.category;
                setStateToSelectedCategoryForId(vm.selectedCategory.Id);
            }, true);

            if (!common.$state.params.id) {
                redirectToChildSelectedState();
            }

            function redirectToChildSelectedState() {
                setStateToSelectedCategoryForId(vm.categories[0].Id);
            }

            function setStateToSelectedCategoryForId(id) {
                return common.$state.go("admin.categories.selected", { id: id });
            }
        }

        function selectCategoryHandler(category) {
            common.$state.go("admin.categories.selected", { id: category.Id });
        }

        function persistCategories() {
            // make a call to the data service
            services.data.categories.SaveCategories(vm.categories[0]).then(persistCategoriesSuccess, error);

            function persistCategoriesSuccess() {
                refreshCategories();
            }

            function error() {
                refreshCategories();
            }
        }

        function refreshCategories() {
            services.data.categories.enableRefresh();
            common.$state.go("admin.categories", null, { reload: true });
        }
    };
})();