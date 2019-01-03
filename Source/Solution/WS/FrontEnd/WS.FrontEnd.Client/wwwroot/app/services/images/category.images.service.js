(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("CategoryImagesService", CategoryImagesService);

    CategoryImagesService.$inject = [];

    /*
        The product image service is supposed to handle the following structure for Category:
        Category
            AppImage
    */

    function CategoryImagesService() {

        var factory = {};

        factory.AddCategoryImage = addCategoryImage;

        return factory;

        /*
            When a new upload happens we need to create an Image Object and store it on the Category
        */

        function addCategoryImage(category, categoryImageUploadResult) {
            var id = 0;

            if (category.CategoryImage) {
                id = category.CategoryImage.Id;
            }

            // create a raw AppImage from the upload result
            // with an id of 0 as it is a new image uploaded for the Category
            var image = {
                Id: id,
                Name: categoryImageUploadResult.FileOriginalName,
                Uri: categoryImageUploadResult.VirtualLocation,
                Type: "CategoryImage"
            }

            // add the product image to the product
            category.CategoryImage = image;
        }
    }
})();