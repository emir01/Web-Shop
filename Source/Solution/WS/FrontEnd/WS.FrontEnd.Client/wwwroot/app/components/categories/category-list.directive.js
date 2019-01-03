(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("categoryList", categoryDropdown);

    function categoryDropdown() {
        var directive = {
            scope: {
                categories: "="
            },

            restrict: "EA",
            templateUrl: 'app/components/categories/category-list.directive.html',

            controller: categoryListController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }
    
    function categoryListController() {
        var vm = this;

        vm.isParent = _isParent;
        vm.isFinalCategory = _isFinalCategory;

        activate();

        function activate() {
        }

        function _isParent(category) {
            if (vm.categories.indexOf(category) === vm.categories.length - 1) {
                return false;
            }
            return true;
        }

        function _isFinalCategory(category) {
            if (vm.categories.indexOf(category) === vm.categories.length - 1) {
                return true;
            }
            return false;
        }
    }
})();