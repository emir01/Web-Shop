(function () {
    "use stirct";

    angular
        .module("ws.components")
        .filter("percentage", percentageFilter);

    percentageFilter.$inject = ["$filter"];

    function percentageFilter($filter) {
        return function (input, decimals) {
            return $filter('number')(input * 100, decimals) + '%';
        };
    }
})();