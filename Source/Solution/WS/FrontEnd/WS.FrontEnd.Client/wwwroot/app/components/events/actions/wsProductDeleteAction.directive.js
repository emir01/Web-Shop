(function () {
    angular
        .module("ws.components")
        .directive("wsProductDeleteAction", wsProductDeleteAction);

    function wsProductDeleteAction() {
        var directive = {
            restrict: "E",
            templateUrl: 'app/components/events/actions/wsProductDeleteAction.directive.html',
            scope: {
                product: "=",
                showLabel: "=?"
            },

            link: link,

            controller: wsProductDeleteActionController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;

        function link(scope, element, attrs) {
            if (!attrs.showLabel) {
                scope.showLabel = true;
            }
        }

        wsProductDeleteActionController.$inject = ["common"];

        function wsProductDeleteActionController(common) {
            var vm = this;

            vm.delete = _delete;

            function _delete() {
                common.$rootScope.$broadcast(common.commands.product.delete, vm.product);
            }
        }
    }
})();