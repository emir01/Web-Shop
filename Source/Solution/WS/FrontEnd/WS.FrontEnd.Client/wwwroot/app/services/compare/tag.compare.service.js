(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("TagCompareService", TagCompareService);

    TagCompareService.$inject = ["common"];

    function TagCompareService(common) {

        var factory = {};

        factory.ProcessCompareTags = processCompareTags;

        initialize();

        return factory;

        // METHODS
        ////////////////////////////////////////////////

        function initialize() {

        }

        /*
            Given two tag collections creates a single collection which matches tag types from both 
            collections and returns a single renderable collection with the values of both
        */
        function processCompareTags(firstTags, secondTags) {
            var longerCollection = getCollectionBasedOnLength(firstTags, secondTags, true);

            var shorterCollection = getCollectionBasedOnLength(firstTags, secondTags, false);

            var compareTagsOutput = [];

            // process all common tags and all tags from the first 
            // collection
            for (var i = 0; i < longerCollection.length; i++) {
                var firstTag = longerCollection[i];

                var secondTag = findTagInCollectionForPropertyTypeId(shorterCollection, firstTag.PropertyTypeId);

                if (secondTag) {
                    compareTagsOutput.push(new CompareTag(firstTag.PropertyTypeId, firstTag.Name, firstTag.Value, secondTag.Value));
                } else {
                    compareTagsOutput.push(new CompareTag(firstTag.PropertyTypeId, firstTag.Name, firstTag.Value, ""));
                }
            }

            // push all tags exclusive to the second collection
            var shorterCollectionExclusiveTags = common.lodash.filter(shorterCollection, function (shorterCollectionTag) {
                return common.lodash.filter(compareTagsOutput, function (compareTag) {
                    return compareTag.id === shorterCollectionTag.PropertyTypeId;
                }).length === 0;
            });

            for (var j = 0; j < shorterCollectionExclusiveTags.length; j++) {
                var secondCollectioniExclusigeTag = shorterCollectionExclusiveTags[j];

                compareTagsOutput.push(new CompareTag(secondCollectioniExclusigeTag.PropertyTypeId, secondCollectioniExclusigeTag.Name, "", secondCollectioniExclusigeTag.Value));
            }

            return compareTagsOutput;
        }

        // INTERNALS
        ////////////////////////////////////////////////

        function getCollectionBasedOnLength(firstTags, secondTags, getLonger) {
            if (firstTags.length === secondTags.length) {
                if (getLonger) {
                    return firstTags;
                } else {
                    return secondTags;
                }
            }

            if (firstTags.length >= secondTags.length) {
                if (getLonger) {
                    return firstTags;
                } else {
                    return secondTags;
                }
            } else {
                if (getLonger) {
                    return secondTags;
                } else {
                    return firstTags;
                }
            }
        }

        function findTagInCollectionForPropertyTypeId(collection, propertyTypeId) {
            return common.lodash.find(collection, function (tag) {
                return tag.PropertyTypeId === propertyTypeId;
            });
        }

        // INTERNALS CLASS
        ////////////////////////////////////////////////

        function CompareTag(id, name, firstValue, secondValue) {
            this.id = id;
            this.name = name;
            this.firstValue = firstValue;
            this.secondValue = secondValue;

            return this;
        }
    }
})();