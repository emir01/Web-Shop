(function () {
    angular
        .module("ws.components")
        .directive("wsConfirmClickAction", wsConfirmClickActionDirective)
        .controller("wsConfirmClickActionModalController", wsConfirmClickActionModalController);

    wsConfirmClickActionDirective.$inject = ["common"];

    function wsConfirmClickActionDirective(common) {
        var directive = {
            restrict: "A",
            scope: {
                wsConfirmActionConfig: "=",
                wsConfirmClickActionMethod: "&"
            },

            link: link
        };

        function link(scope, element) {
            element.bind('click', function () {
                var modalInstance = common.$uibModal.open({
                    templateUrl: "app/components/events/actions/wsConfirmClickActionModal.directive.html",
                    controller: 'wsConfirmClickActionModalController as vm',

                    resolve: {
                        message: function () {
                            // pass the message from the wsConfirmActionConfig object
                            // which also should include the data action assosiated with the confimation action
                            return scope.wsConfirmActionConfig.message;
                        }
                    }
                });

                // setup the modal instance result execution -- here we can call the function to be executed on confirm
                modalInstance.result.then(function () {
                    // confirm the action passing back the data object if any which was transfered
                    // from the directive configuration
                    scope.wsConfirmClickActionMethod()(scope.wsConfirmActionConfig.data);

                }, function (reason) { });
            });
        }

        return directive;
    }

    wsConfirmClickActionModalController.$inject = ["$scope", "$uibModalInstance", "message"];

    function wsConfirmClickActionModalController($scope, $uibModalInstance, message) {
        var vm = this;

        // set the message that will be displayed on the modal
        vm.Message = message || "Are you sure you want to proceed?";

        vm.yes = yes;
        vm.cancel = cancel;

        // run any activation logic regardint the confirmation modal
        activate();

        function activate() { }

        function yes() {
            $uibModalInstance.close();
        }

        function cancel() {
            $uibModalInstance.dismiss("action_canceled");
        }
    }
})();