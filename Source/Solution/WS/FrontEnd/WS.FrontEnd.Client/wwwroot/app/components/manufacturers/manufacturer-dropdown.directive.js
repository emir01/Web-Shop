(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("manufacturerDropdown", manufacturerDropdown);

    function manufacturerDropdown() {
        var directive = {
            scope: {
                manufacturers: "=",
                selectedManufacturer: "="
            },

            restrict: "EA",
            templateUrl: 'app/components/manufacturers/manufacturer-dropdown.directive.html',

            controller: manufacturerDropdownController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    function manufacturerDropdownController() {
    }
})();