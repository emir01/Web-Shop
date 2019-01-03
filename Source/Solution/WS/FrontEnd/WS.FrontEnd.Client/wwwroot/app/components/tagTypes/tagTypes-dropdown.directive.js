(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("tagTypesDropdown", tagTypesDropdown);

    function tagTypesDropdown() {
        var directive = {
            scope: {
                tagTypes: "=",
                selectedTagType: "=",
                selectedTagTypeFilters: "="
            },

            restrict: "EA",
            templateUrl: 'app/components/tagTypes/tagTypes-dropdown.directive.html',

            controller: tagTypesDropdownController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function tagTypesDropdownController() {
    }
})();