(function () {
    "use strict";

    angular
        .module("ws.core")
        .factory("language", languageService);

    function languageService() {
        var factory = getLanguage();
        return factory;

        function getLanguage() {
            return {
                status: {
                    active: "Active",
                    inactive: "Inactive"
                },
                general: {
                    title: "Web Shop Application",
                    messages: {
                        error: {
                            unathorized: "You are not autorized. Please login to access that page"
                        }
                    }
                },
                product: {
                    title: "Products",
                    messages: {
                        error: {
                            get: "Error getting product/s",
                            gettingCompare: "Error getting products for compare",
                            gettingSimilar: "Error getting similar products",
                            create: "Error creating product",
                            update: "Error updating product",
                            "delete": "Error deleting product",
                            activate: "Error activating product"
                        }
                    }
                },
                tag: {
                    title: "Tags",
                    messages: {
                        error: {
                            getTags: "Error getting tags",
                            create: "Error creating tag",
                            edit: "Error updating tag",
                            "delete": "Error deleting tag"
                        },
                        success: {
                            create: "Tag created",
                            update: "Tag updated"
                        }
                    }
                },
                manufacturer: {
                    title: "Manufacturers",
                    messages: {
                        error: {
                            get: "Error getting manufacturers",
                            create: "Error creating manufacturer",
                            edit: "Error updating manufacturer",
                            "delete": "Error deleting manufacturer"
                        },
                        success: {
                            create: "Manufacturer created",
                            update: "Manufacturer updated",
                            "delete": "Manufacturer updated"
                        }
                    }
                },
                category: {
                    title: "Categories",
                    messages: {
                        error: {
                            get: "Error getting categories",
                            save: "Error saving category changes"
                        }
                    }
                },
                file: {
                    title: "Files",
                    messages: {
                        error: {
                            upload: "Image upload failed"
                        },
                        success: {
                            upload: "Image uploaded successfuly"
                        }
                    }
                }
            }
        }
    }
})();