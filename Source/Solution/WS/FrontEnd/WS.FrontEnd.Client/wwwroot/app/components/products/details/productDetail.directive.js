(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsProductDetails", wsProductDetails);

    function wsProductDetails() {
        var directive = {
            scope: {
                product: "=",
                showImage: "=",
                showTags: "="
            },

            restrict: "E",

            templateUrl: 'app/components/products/details/productDetail.directive.html',

            controller: wsProductDetailsController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsProductDetailsController.$inject = ["common"];

    function wsProductDetailsController(common) {
        var vm = this;

        vm.currentPriceSet = _currentPriceSet;
        vm.regularPriceSet = _regularPriceSet;

        vm.activePrice = _getActivePrice;
        vm.calculateDiscount = _calculateDiscount;

        activate();

        function activate() { }

        function _getActivePrice() {
            var product = vm.product;

            if (_currentPriceSet(product)) {
                return product.PriceCurrent;
            } else {
                return product.PriceRegular;
            }
        }

        function _currentPriceSet(product) {
            var currentPriceIsSet = product.PriceCurrent && product.PriceCurrent !== 0 && product.PriceCurrent !== product.PriceRegular;
            return currentPriceIsSet;
        }

        function _regularPriceSet(product) {
            var price = common.lodash.parseInt(product.PriceRegular);

            var regularPriceIsSet = price !== 0;

            return regularPriceIsSet;
        }

        function _calculateDiscount(regularPrice, currentPrice) {
            var regPriceInt = common.lodash.parseInt(regularPrice);
            var currPriceInt = common.lodash.parseInt(currentPrice);

            var percentage = 1 - (currPriceInt / regPriceInt);

            return percentage;
        }
    }
})();