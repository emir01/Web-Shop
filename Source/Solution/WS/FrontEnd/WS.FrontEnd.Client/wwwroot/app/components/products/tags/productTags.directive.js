(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductTags", wsProductTags);

    function wsProductTags() {
        var directive = {
            scope: {
                tags: "=",
                filterOnValue: "="
            },

            restrict: "E",

            templateUrl: 'app/components/products/tags/productTags.directive.html',

            controller: wsProductTagsController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsProductTagsController.$inject = ["common", "services"];

    function wsProductTagsController(common, services) {
        var vm = this;

        vm.filteredTags = [];

        activate();

        function activate() {
            common.$rootScope.$watchCollection(function () {
                return vm.tags;
            }, function () {
                common.lodash.forEach(vm.tags, function (tag) {
                    if (services.utils.data.StringIsDefinedAndNotEmpty(tag.Value) || !vm.filterOnValue) {
                        vm.filteredTags.push(tag);
                    }
                });
            });
        }
    }
})();