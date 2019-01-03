(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsImage", wsImage);

    function wsImage() {
        var directive = {
            scope: {
                wsImageUrl: "=",

                wsImagesData: "=",

                wsActiveFlagName: "@",
                wsActiveImageUri: "@",

                wsAltImage: "="
            },

            restrict: "E",
            templateUrl: 'app/components/images/wsImage/wsImage.directive.html',

            controller: wsImageController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsImageController.$inject = ["common", "services"];

    function wsImageController(common, services) {
        var vm = this;

        // get image source is the primary and one of the more
        // important public methods for the image directive
        vm.GetImageSource = getImageSource;

        /*  
            Based on the provided parameters and data to the directive
            determines the url to be used on the single image element in the template.
        */

        function getImageSource() {
            if (singleImageUrlIsProvided()) {
                return composeSingleImageUrl();
            } else if (imagesDataAndActiveImageNameProvided()) {
                return composeImageDataUrlBasedOnActiveImage();
            } else {
                return composeDefualtImage();
            }
        }

        /*
            Composition functions that compose vm and configuration values 
            for generating image urls
        */

        function composeImageDataUrlBasedOnActiveImage() {
            var activeImage = null;
            for (var i = 0; i < vm.wsImagesData.length; i++) {
                var imageDataObject = vm.wsImagesData[i];

                if (isActiveImage(imageDataObject)) {
                    activeImage = imageDataObject;
                    break;
                }
            }

            if (activeImage == null) {
                activeImage = vm.wsImagesData[0];
            }

            var uri = services.utils.data.GetDescendantProp(activeImage, vm.wsActiveImageUri);
            return common.restConfig.filesGetUrl + uri;
        }

        function composeSingleImageUrl() {
            return common.restConfig.filesGetUrl + vm.wsImageUrl;
        }

        function composeDefualtImage() {
            // if the category has an image defined 
            if (services.utils.data.DataIsDefinedAndNotNull(vm.wsAltImage)) {
                var url = common.restConfig.filesGetUrl + vm.wsAltImage;
                return url;
            }

            // return the default image
            return common.restConfig.filesGetUrl + common.defaults.ProductDefaultImageUrl;
        }

        /*
            Validations and checks for data provided to the Directive
        */
        function imagesDataAndActiveImageNameProvided() {
            if (services.utils.data.DataIsDefinedAndNotNull(vm.wsImagesData)
                && services.utils.data.DataIsDefinedAndNotNull(vm.wsImagesData.length)
                && vm.wsImagesData.length > 0
                && services.utils.data.DataIsDefinedAndNotNull(vm.wsActiveFlagName)
                && services.utils.data.DataIsDefinedAndNotNull(vm.wsActiveImageUri)) {
                return true;
            } else {
                return false;
            }
        }

        function singleImageUrlIsProvided() {
            return services.utils.data.DataIsDefinedAndNotNull(vm.wsImageUrl);
        }

        function isActiveImage(imageData) {
            return services.utils.data.IsTrue(services.utils.data.GetDescendantProp(imageData, vm.wsActiveFlagName));
        }
    }
})();