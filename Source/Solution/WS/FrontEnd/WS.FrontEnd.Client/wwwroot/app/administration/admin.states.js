(function () {
    'use strict';

    angular
        .module('ws.administration')
        .config(config);

    config.$inject = ["$stateProvider", "app.states"];

    function config($stateProvider, appStates) {
        appStates.admin = {
            base: "admin",
            tags: "admin.tags",
            manufacturers: "admin.manufacturers",
            categories: {
                base: "admin.categories",
                selected: "admin.categories.selected"
            },
            products: {
                base: "admin.products",
                list: "admin.products.list",
                edit: "admin.products.edit"
            },
            login: "admin.login"
        }

        $stateProvider
            .state(appStates.admin.base, {
                url: "admin",
                parent: "shell",
                abstract: true,
                template: "<div class='core-ui-view' ui-view></div>"
            })
            .state(appStates.admin.tags, {
                url: "/tags",
                templateUrl: "app/administration/tags/tagManagement.html",
                controller: "TagManagementController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name: "Tag Management"
                },
                resolve: {
                    categories: [
                        "services", "primedData", function (services) {
                            return services.data.categories.QueryCategoriesTree();
                        }
                    ]
                }
            })
            .state(appStates.admin.manufacturers, {
                url: "/manufacturers",
                templateUrl: "app/administration/manufacturers/manufacturerManagement.html",
                controller: "ManufacturerManagementController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name: "Manufacturer Management"
                },
                resolve: {
                    manufacturers: [
                        "services", "primedData", function (services) {
                            return services.data.manufacturers.QueryManufacturers().then(function (data) {
                                return data;
                            });
                        }
                    ]
                }
            })
            .state(appStates.admin.categories.base, {
                url: "/categories",
                templateUrl: "app/administration/categories/categoryManagement.html",
                controller: "CategoryManagementController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name: "Category Management"
                },
                resolve: {
                    categories: [
                        "services", "primedData", function (services) {
                            return services.data.categories.QueryCategoriesTree();
                        }
                    ]
                }
            })
            .state(appStates.admin.categories.selected, {
                url: "/:id",
                templateUrl: "app/administration/categories/templates/categoryManagement.edit.html",
                controller: "CategoryManagementEditController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name: "Category Management"
                }
            })
            .state(appStates.admin.products.base, {
                url: "/products",
                template: "<div class='core-ui-view' ui-view></div>",
                abstract: true

            })
            .state(appStates.admin.products.list, {
                url: "/list",
                templateUrl: "app/administration/products/productManagement.html",
                controller: "ProductManagementController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name: "Product Management"
                },
                resolve: {
                    products: [
                        "services", "primedData", function (services) {
                            return services.data.products.QueryProducts({ ignoreStatus: true });
                        }
                    ],
                    categories: [
                        "services", "primedData", function (services) {
                            return services.data.categories.QueryFlatCategories();
                        }
                    ],
                    manufacturers: [
                        "services", "primedData", function (services) {
                            return services.data.manufacturers.QueryManufacturers();
                        }
                    ]
                }
            })
            .state(appStates.admin.products.edit, {
                url: "/edit/:id",
                templateUrl: "app/administration/products/productEdit.html",
                controller: "ProductEditController",
                controllerAs: "vm",
                data: {
                    auth: true,
                    name:"Product Management"
                },
                resolve: {
                    categories: [
                        "services", "primedData", function (services) {
                            return services.data.categories.QueryFlatCategories();
                        }
                    ],
                    manufacturers: [
                        "services", "primedData", function (services) {
                            return services.data.manufacturers.QueryManufacturers();
                        }
                    ],
                    product: [
                        "services", "$stateParams", "primedData", function (services, $stateParams) {
                            var currentProductId = $stateParams.id;
                            return services.data.products.GetEditProduct(currentProductId);
                        }
                    ]
                },
                onEnter: [
                    "product", "common", function (product, common) {
                        if (typeof (product) !== "object") {
                            common.$state.go("admin.products.list");
                        }
                    }
                ]
            })
            .state(appStates.admin.login, {
                url: "/login?:previous",
                templateUrl: "app/administration/users/userLogin.html",
                controller: "LoginController",
                controllerAs: "vm"
            });
    }
})();
