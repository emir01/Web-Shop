(function () {
    "use strict";

    angular
        .module("ws.administration")
        .controller("ProductManagementController", ProductManagementController);

    ProductManagementController.$inject = ["common", "services", "uiGridConstants", "products", "categories", "manufacturers"];

    function ProductManagementController(common, services, uiGridConstants, products, categories, manufacturers) {
        var vm = this;

        vm.products = products;
        vm.categories = categories;
        vm.manufacturers = manufacturers;

        vm.selectedProduct = null;
        vm.gridOptions = {};

        vm.NewProductHandler = addNewProductHandler;
        vm.DeleteProductHandler = deleteProductHandler;
        vm.ActivateProductHandler = activateProductHandler;



        activate();

        function activate() {
            initGridOptions();
        }

        function initGridOptions() {
            vm.gridOptions = {
                data: vm.products,
                enableFullRowSelection: true,
                enableSelectAll: false,
                enableFiltering: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                multiSelect: false,
                showGridFooter: true,
                paginationPageSizes: [25, 50, 75],
                paginationPageSize: 25,
                rowTemplate: '<div ng-dblclick="grid.appScope.doubleClickHandler(row)" ng-repeat="col in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ui-grid-cell></div>',
                appScopeProvider: {
                    doubleClickHandler: _doubleClickHandler,
                    mapStateToValue: _mapStateToValue
                },
                columnDefs: [
                    { field: 'Id', displayName: 'Id', enableColumnMenu: false },
                    { field: 'Name', displayName: 'Name', enableColumnMenu: false },
                    { field: 'Category', displayName: 'Category', enableColumnMenu: false },
                    { field: 'Manufacturer', displayName: 'Manufacturer', enableColumnMenu: false },
                    {
                        field: 'State',
                        displayName: 'State',
                        enableColumnMenu: false,
                        cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.appScope.mapStateToValue(grid.getCellValue(row, col))}}</div>',
                        filter: {
                            selectOptions: [
                                { value: 1, label: services.language.status.active },
                                { value: 0, label: services.language.status.inactive }],
                            type: uiGridConstants.filter.SELECT
                        }
                    }
                ]
            };

            function _mapStateToValue(value) {
                if (value === 1) {
                    return services.language.status.active;
                } else {
                    return services.language.status.inactive;
                }
            }

            vm.gridOptions.onRegisterApi = onGridRegister;

            function onGridRegister(gridApi) {
                vm.gridApi = gridApi;

                vm.gridApi.selection.on.rowSelectionChanged(null, function (row) {
                    if (row.isSelected) {
                        vm.selectedProduct = row.entity;
                    } else {
                        vm.selectedProduct = null;
                    }
                });
            }
        }

        function activateProductHandler() {
            var rows = vm.gridApi.selection.getSelectedRows();
            if (rows.length && rows.length > 0) {
                var entity = rows[0];

                if (entity) {
                    services.data.products.activateProduct(entity.Id).then(function (product) {
                        var productInArray = common.lodash.find(vm.products, function (pr) {
                            return product.Id === pr.Id;
                        });

                        if (productInArray) {
                            productInArray.State = 1;
                        }
                    });
                }
            }
        }

        function deleteProductHandler() {
            var rows = vm.gridApi.selection.getSelectedRows();
            if (rows.length && rows.length > 0) {
                var entity = rows[0];

                if (entity) {
                    services.data.products.deleteProduct(entity.Id).then(function (product) {
                        var productInArray = common.lodash.find(vm.products, function (pr) {
                            return product.Id === pr.Id;
                        });

                        if (productInArray) {
                            productInArray.State = 0;
                        }
                    });
                }
            }
        }

        function _doubleClickHandler(row) {
            common.$state.go("admin.products.edit", { id: row.entity.Id });
        }

        function addNewProductHandler() {
            // open the modal dialog for adding a new product

            services.entity.setNewEntity();

            var modalInstance = common.$uibModal.open({
                templateUrl: "productModal.html",
                controller: "EntityModalController as vm",
                resolve: {
                    // define the header for this instance of the modal
                    header: function () { return "New Product"; },

                    // define any additional data to be resolved
                    data: resolveAddProductLookupData
                }
            });

            modalInstance.result.then(addProductModalConfirmed);

            // resolve the lookup information for creating products
            function resolveAddProductLookupData() {
                return {
                    categories: vm.categories,
                    manufacturers: vm.manufacturers
                }
            }

            function addProductModalConfirmed(item) {
                return services.data.products
                    .CreateProduct(item)
                    .then(productCreatedOnServer);

                function productCreatedOnServer(product) {
                    common.logger.success("New product saved!", product, "Product Management");
                    vm.products.push(product);
                    return product;
                }
            }
        }
    }
})();