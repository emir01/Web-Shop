(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("FilesDataService", FilesDataService);

    FilesDataService.$inject = ["Upload", "common", "error"];

    function FilesDataService(Upload, common, error) {

        var factory = {};

        factory.ImageUpload = imageUpload;

        return factory;

        /*
         * Parameters: 
         * File - The uploaded file
         * Metadata - 
         *          Type: Product
         *          Id: ProductId/CategoryId
         */
        function imageUpload(file, metadata, fileUploadedCallback, fileFailedUploadFallback) {
            var uploadUrl = common.restConfig.files_base;

            var data = {
                file: file
            };

            for (var property in metadata) {
                if (metadata.hasOwnProperty(property)) {
                    data[property] = metadata[property];
                }
            }

            Upload.upload({
                url: uploadUrl,
                data: data
            }).then(success, error, notify);

            function success(resp) {
                fileUploadedCallback(resp);
            }

            function error(resp) {
                if (fileFailedUploadFallback) {
                    fileFailedUploadFallback(resp);
                }

                return common.$q(function (response, reject) {
                    reject(resp);
                });
            }

            function notify(evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            }
        }
    }
})();