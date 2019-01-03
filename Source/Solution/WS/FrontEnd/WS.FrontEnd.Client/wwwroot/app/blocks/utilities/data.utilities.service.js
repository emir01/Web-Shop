(function () {
    "use strict";

    angular
        .module("blocks.entities")
        .factory("DataUtils", dataUtils);

    dataUtils.$inject = ["common"];

    function dataUtils(common) {
        var factory = {};

        factory.DataIsDefinedAndNotNull = dataIsDefinedAndNotNull;

        factory.DefinedAndNotNullAndHasElements = definedAndNotNullAndHasElements;

        factory.StringIsDefinedAndNotEmpty = _stringIsDefinedAndNotEmpty;

        factory.IsTrue = isTrue;

        factory.GetDescendantProp = getDescendantProp;

        factory.SetDescendantProp = setDescendantProp;

        factory.revertObjectInCollection = _revertObjectInCollection;

        function _revertObjectInCollection(collection, object, revert, key) {
            var objectInCollection = common.lodash.find(collection, function (item) {
                return object[key] === item[key];
            });

            if (objectInCollection) {
                angular.merge(objectInCollection, revert);
            }
        }

        function dataIsDefinedAndNotNull(data) {
            return (typeof data != "undefined" && data != null);
        }

        function definedAndNotNullAndHasElements(data) {
            return dataIsDefinedAndNotNull(data)
                && dataIsDefinedAndNotNull(data.length)
                && data.length > 0;
        }

        function _stringIsDefinedAndNotEmpty(stringValue) {
            return dataIsDefinedAndNotNull(stringValue) && typeof (stringValue) == "string" && stringValue.trim() !== "";
        }

        function isTrue(property) {
            if (property === true) {
                return true;
            }

            if (property.toString().toLowerCase() === "true") {
                return true;
            }

            return false;
        }

        function getDescendantProp(obj, desc) {
            var arr = desc.split(".");
            while (arr.length && (obj = obj[arr.shift()]));
            return obj;
        }

        function setDescendantProp(obj, desc, value) {
            var arr = desc.split(".");
            var currentLevel = obj;
            var nextTerm;

            for (var i = 0; i < arr.length; i++) {
                nextTerm = arr[i];
                if (i === arr.length - 1) {
                    currentLevel[nextTerm] = value;
                } else {
                    currentLevel = currentLevel[nextTerm] = currentLevel[nextTerm] || {};
                }
            }
        }

        return factory;
    };
})();