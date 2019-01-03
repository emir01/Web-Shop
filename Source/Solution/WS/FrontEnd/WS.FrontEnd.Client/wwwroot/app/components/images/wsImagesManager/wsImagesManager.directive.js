(function () {
    "use stirct";

    angular
        .module("ws.components")
        .directive("wsImagesManager", wsImagesManager);

    function wsImagesManager() {
        var directive = {
            scope: {
                wsImageData: "=",
                wsAltImageUrl: "=",
                wsIsSingleImage: "=",
                wsImageUriPropName: "@",
                wsImageRemovedPropValue: "@",
                wsImageRemovedPropName: "@",
                wsActiveFlagName: "@",
                wsDisplayOnly: "="
            },

            restrict: "E",
            templateUrl: 'app/components/images/wsImagesManager/wsImagesManager.directive.html',

            controller: wsImagesManagerController,
            controllerAs: "vm",
            bindToController: true
        };

        return directive;
    }

    wsImagesManagerController.$inject = ["$scope", "$filter", "common", "services"];

    function wsImagesManagerController($scope, $filter, common, services) {
        var vm = this;

        vm.selectedImage = null;

        vm.SelectImage = selectImage;

        vm.SetSelectedImageAsRemoved = setSelectedImageAsRemoved;

        vm.GetImageUrl = getImageUrl;

        vm.ImageIsSelected = imageIsSelected;

        vm.IsDisplayOnly = _isDisplayOnly;

        vm.IsSingleImage = _isSingleImage;

        activate();

        function activate() {
            if (_isSingleImage()) {
                // watch for changes on the image object
                $scope.$watch("vm.wsImageData", function () {
                    // if the image object passed from the client is defined we select
                    // the wsImageData object as the active
                    if (services.utils.data.DataIsDefinedAndNotNull(vm.wsImageData)) {
                        setImageDataAsActive();
                    }
                }, true);
            }
            else {
                // we are watching the collection of image data
                // and on change we are selecting the active image
                $scope.$watchCollection("vm.wsImageData", function () {
                    if (services.utils.data.DataIsDefinedAndNotNull(vm.wsImageData)) {
                        selectImageFromCollection();
                    } else {
                        selectImage(null);
                    }
                });
            }
        }

        ////////////////////////////////////////////////////////////////////
        // ACTIVATION
        ////////////////////////////////////////////////////////////////////

        /*
         * Triggers when something gets changed in the collection
         */
        function selectImageFromCollection() {
            if (vm.wsImageData.length === 0) {
                selectImage(null);
                return;
            }

            for (var i = 0; i < vm.wsImageData.length; i++) {
                // find the first active image in the collection
                if (_imageIsActive(vm.wsImageData[i])) {
                    // set the directives internal tracking selected image to the active image
                    selectImage(vm.wsImageData[i]);
                    break;
                }
            }

            // if there was no active image in the collection
            if (vm.selectedImage == null) {
                // an image was not selected based on the active flag name
                // so try and select one either way from the ones not marked as deleted.
                var nonDeletedImages = $filter("entityNotDeletedFilter")(vm.wsImageData);

                if (services.utils.data.DefinedAndNotNullAndHasElements(nonDeletedImages)) {
                    selectImage(nonDeletedImages[0]);
                } else {
                    selectImage(null);
                }
            }
        }

        /*
         * ACTIVATION SINGLE
         * 
         * Process the main data object bound to the directive as a single image. 
         * 
         * If the image is not deleted it is selected as the internally tracked image, otherwise
         * the internally tracked image is set as null.
         */
        function setImageDataAsActive() {
            // check that the change that triggered the image update is not caused by deleting the image.
            if (_imageNotDeleted(vm.wsImageData)) {
                // set the directive selected image to the image passed from client code
                selectImage(vm.wsImageData);
            } else {
                // otherwise there is no selected image
                selectImage(null);
            }
        }

        ////////////////////////////////////////////////////////////////////
        // EVENTS
        ////////////////////////////////////////////////////////////////////

        /*
            EVENT
            Select the image. If working with multiple iamges set the image as the active one!
            ex. ActiveImage in a Product Image collection would refer to the IsPrimary flag (configurable) and indicates
                which image is dispalyed on search results for the given product
        */

        function selectImage(image) {
            if (image == null && vm.wsDisplayOnly) {
                image = {}
                services.utils.data.SetDescendantProp(image, vm.wsImageUriPropName, vm.wsAltImageUrl || common.defaults.ProductDefaultImageUrl);
            }

            vm.selectedImage = image;

            // make the selected image primary in the case of a collection
            if (!_isSingleImage() && !vm.wsDisplayOnly) {
                _makeSelectedImagePrimaryInCollection(image, vm.wsImageData);
            }
        }

        /*
            EVENT
            Set the removed flag name to the currently selected image
            to the defined removed value.
    
            Also if dealing with multiple images preselect the first non-deleted image
        */

        function setSelectedImageAsRemoved() {
            // mark the current image as deleted
            vm.selectedImage[vm.wsImageRemovedPropName] = vm.wsImageRemovedPropValue;

            if (_isSingleImage()) {
                // if dealing with a single image set the current active image as null
                selectImage(null);
            } else {
                selectImage(firstNonDeletedImageInCollection(vm.wsImageData));
            }
        }

        ////////////////////////////////////////////////////////////////////
        // UTILITIES
        ////////////////////////////////////////////////////////////////////

        /*
        * UTILITY MULTIPLE
        * Return the first non deleted image in a collection. 
        */
        function firstNonDeletedImageInCollection(collection) {
            for (var i = 0; i < collection.length; i++) {
                if (_imageNotDeleted(collection[i])) {
                    return collection[i];
                }
            }

            return null;
        }

        /*
         * UTILITY MULTIPLE 
         * Reset the active flag (ex. IsPrimary) on all the images in the collection and set it to true
         * on the provided image. 
         * 
         * Ex. This can be used to set the image as currently Primary for a collection of Product images
         *     - which means that that image is the one dispalyed on image search.
         */

        function _makeSelectedImagePrimaryInCollection(image, imageCollection) {
            angular.forEach(imageCollection, function (image) {
                image[vm.wsActiveFlagName] = false;
            });

            if (image) {
                image[vm.wsActiveFlagName] = true;
            }
        }

        /*
         * UTILITY GENERAL
         * 
         * A utility which builds the url for the provided image.
         */
        function getImageUrl(image) {
            if (image == null) {
                return "";
            }

            var baseUrl = common.restConfig.filesGetUrl;
            var imageUri = services.utils.data.GetDescendantProp(image, vm.wsImageUriPropName);

            var url = baseUrl + imageUri;

            return url;
        }

        /*
         * UTILITY GENERAL
         * 
         * Utility checking if there is a currently internally tracked image.
         */

        function imageIsSelected() {
            return vm.selectedImage != null;
        }

        /*
         *  UTILITY GENERAL
         * 
         *  Return the state of the isDisplayOnly property which controls if the Remove Image button is shown.
         */

        function _isDisplayOnly() {
            if (services.utils.data.DataIsDefinedAndNotNull(vm.wsDisplayOnly) && vm.wsDisplayOnly) {
                return true;
            } else {
                return false;
            }
        }

        /*
         * UTILITY GENERAL
         * 
         * Return the flag indicating if the image manager is used to work only with a single image
         */
        function _isSingleImage() {
            if (services.utils.data.DataIsDefinedAndNotNull(vm.wsIsSingleImage) && vm.wsIsSingleImage) {
                return true;
            } else {
                return false;
            }
        }

        /*
         * UTILITY GENERAL
         * 
         * Check if the image is not deleted. 
         * ex. Based on configuration this can check if the State property is not set to 4 (where 4 is Deleted on the server side)
         */

        function _imageNotDeleted(image) {
            return image != null && image[vm.wsImageRemovedPropName] != vm.wsImageRemovedPropValue;
        }

        /*
         * UTILITY GENERAL
         * 
         * Check if the image is considered an active in a collection. 
         * ex. IsPrimary - for Product Images indicating which Image in the collection is primarly shown.
         */
        function _imageIsActive(image) {
            if (services.utils.data.DataIsDefinedAndNotNull(image[vm.wsActiveFlagName])) {
                return image[vm.wsActiveFlagName];
            } else {
                return true;
            }
        }
    }
})();