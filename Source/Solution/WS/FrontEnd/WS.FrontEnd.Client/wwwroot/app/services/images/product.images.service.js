(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("ProductImagesService", ProductImagesService);

    ProductImagesService.$inject = [];

    /*
        The product image service is supposed to handle the following structure for Products:
        Product
            ProductImages
                AppImage
    */

    function ProductImagesService() {

        var factory = {};

        factory.AddProductImage = addProductImage;

        return factory;

        /*
            When a new upload happens we need to create an Image Object and store it on the 
            array of Product Images on the Product
        */

        function addProductImage(product, uploadResult) {
            if (typeof product.ProductImages == "undefined" || product.ProductImages == null) {
                product.ProductImages = [];
            }

            // create a raw AppImage from the upload result
            // with an id of 0 as it is a new image uploaded for the Product
            var image = {
                Id: 0,
                Name: uploadResult.FileOriginalName,
                Uri: uploadResult.VirtualLocation,
                Type: "ProductImage"
            }

            // create a ProductImage objects that wraps the AppImage
            var productImage = {
                Id: 0,
                IsPrimary: false,
                DateUploaded: uploadResult.DateUploaded,
                Image: image,
                ProductId: product.Id
            }

            if (product.ProductImages.length === 0) {
                productImage.IsPrimary = true;
            }

            // add the product image to the product
            product.ProductImages.push(productImage);
        }
    }
})();