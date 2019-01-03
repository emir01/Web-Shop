(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("categoryDropdown", categoryDropdown);

    function categoryDropdown() {
        var directive = {
            scope: {
                categories: "=",
                selectedCategory: "="
            },

            restrict: "EA",
            templateUrl: 'app/components/categories/category-dropdown.directive.html',

            controller: categoryDropdownController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }
    
    function categoryDropdownController() {
        var vm = this;

        vm.selections = [];

        vm.renderHirearchyDepthIndicator = _renderHirearchyDepthIndicator;

        activate();

        function activate() {}

        function _renderHirearchyDepthIndicator(category) {
            var depthIndicator = "";

            for (var i = 0; i < category.Depth; i++) {
                depthIndicator = depthIndicator + " - ";
            }

            return depthIndicator;
        }
    }
})();