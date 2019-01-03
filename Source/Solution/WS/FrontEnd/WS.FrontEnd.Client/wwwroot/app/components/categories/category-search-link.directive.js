(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("categorySearchLink", categorySearchLink);

    function categorySearchLink() {
        var directive = {
            scope: {
                categoryId: "=",
                categoryName: "="
            },

            restrict: "E",
            templateUrl: 'app/components/categories/category-search-link.directive.html',

            controller: categorySearchLinkController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    categorySearchLinkController.$inject = ["services"];

    function categorySearchLinkController(services) {
        var vm = this;

        vm.search = _search;

        activate();

        function activate() {
        }

        function _search($event, catId) {
            $event.preventDefault();

            var searchParams = services.search.getSearchParams();
            services.search.addCategoryId(searchParams, catId);
            services.search.go(searchParams);
        }
    }
})();