(function () {
    "use strict";

    angular
        .module("blocks.entities")
        .filter("entityNotDeletedFilter", entityStateFilter);

    entityStateFilter.$inject = ["EntityEditService"];

    function entityStateFilter(entityEditService) {
        return function (items) {
            var filtered = [];

            angular.forEach(items, function(item) {
                if (typeof item.State != "undefined" && item.State != null) {
                    if (item.State != entityEditService.entityState.Deleted) {
                        filtered.push(item);
                    }
                } else {
                    filtered.push(item);
                }
            });

            return filtered;
        };
    }
})();