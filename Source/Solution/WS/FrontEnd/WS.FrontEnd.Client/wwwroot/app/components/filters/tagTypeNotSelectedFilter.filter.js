(function () {
    "use stirct";

    angular
        .module("ws.components")
        .filter("tagTypeNotSelected", tagTypeNotSelected);

    tagTypeNotSelected.$inject = ["$filter", "common"];

    function tagTypeNotSelected($filter, common) {
        return function (input, alreadySelectedTagTypes) {
            var out = [];

            angular.forEach(input, function (tagType) {
                if (common.lodash.every(alreadySelectedTagTypes, function (selectedTagType) { return !selectedTagType.tagType || selectedTagType.tagType.Id !== tagType.Id })) {
                    out.push(tagType);
                }
            });

            return out;
        };
    }
})();